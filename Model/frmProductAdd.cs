using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp12.View;
using static WindowsFormsApp12.LoginForm;

namespace WindowsFormsApp12.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }
        public int Id = 0;
        public int cID = 0;

        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            // Kategorileri ComboBox'a yüklemek için CBFill metodunu çağırıyoruz
            string qry = "SELECT Id, Name AS CategoryName FROM Category"; // Category tablosundan kategori adlarını çekeriz
            DatabaseHelper.CBFill(qry, cbCategory); // cbCategory ComboBox'ınız


        }

        string filePath;
        Byte[] ImageByteArray;
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txt_Image.Image = new Bitmap(filePath) ;
            }
        }


        public override void btn_Save_Click(object sender, EventArgs e)
        {
            // Giriş kontrolleri
            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("Lütfen ürünün ismini giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Price.Text))
            {
                MessageBox.Show("Lütfen ürünün fiyatını giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbCategory.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir category seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string qry = "";
            Hashtable ht = new Hashtable();

            if (Id == 0) // Insert işlemi
            {
                qry = "INSERT INTO Products (Name, Price, CategoryID, Image) VALUES (@Name, @Price, @CategoryID, @Image)";
                ht.Add("@Name", txt_Name.Text);
                ht.Add("@Price", txt_Price.Text);
                ht.Add("@CategoryID", Convert.ToInt32(cbCategory.SelectedValue));
                ht.Add("@Image", ImageByteArray);
            }
            else // Update işlemi
            {
                qry = "UPDATE Products SET Name = @Name, Price = @Price, CategoryID = @CategoryID, Image = @Image WHERE Id = @Id";
                ht.Add("@Id", Id);
                ht.Add("@Name", txt_Name.Text);
                ht.Add("@Price", txt_Price.Text);
                ht.Add("@CategoryID", Convert.ToInt32(cbCategory.SelectedValue));
                ht.Add("@Image", ImageByteArray);
            }

            if (txt_Image.Image == null)
            {
                MessageBox.Show("Lütfen bir resim seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Image temp = new Bitmap(txt_Image.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);  
            ImageByteArray = ms.ToArray();

            try
            {
                int result = ExecuteSQL(qry, ht);
                if (result > 0)
                {
                    MessageBox.Show("Saved successfully!");
                    Id = 0;
                    txt_Name.Text = "";
                    txt_Price.Text = "";
                    cbCategory.SelectedIndex = -1; // ComboBox'u sıfırla
                    txt_Name.Focus();
                }
                else
                {
                    MessageBox.Show("No changes were made.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ExecuteSQL(string qry, Hashtable ht)
        {
            int result = 0; // Kaç satır etkilendiğini tutar

            // Veritabanı bağlantısı
            using (SQLiteConnection con = new SQLiteConnection("Data Source=RestoranYönetimi.db;Version=3;"))
            {
                try
                {
                    con.Open();

                    // Sorguyu çalıştırmak için komut oluştur
                    using (SQLiteCommand cmd = new SQLiteCommand(qry, con))
                    {
                        // Parametreleri ekle
                        foreach (DictionaryEntry param in ht)
                        {
                            cmd.Parameters.AddWithValue(param.Key.ToString(), param.Value ?? DBNull.Value);
                        }

                        // Komutu çalıştır ve etkilenen satır sayısını al
                        result = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda mesaj göster
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return result;
        }
    }
}
