using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp12.Model
{
    public partial class frmTableAdd : SampleAdd
    {
        public frmTableAdd()
        {
            InitializeComponent();
        }
        public int Id = 0;

            public override void btn_Save_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txt_Name.Text))
                {
                    MessageBox.Show("Lütfen tablo ismini giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string qry = "";
                Hashtable ht = new Hashtable();


                if (Id == 0) // Insert işlemi
                {
                    qry = "INSERT INTO Tables (Name) VALUES (@Name)";
                    ht.Add("@Name", txt_Name.Text);

                }
                else // Update işlemi
                {
                    qry = "UPDATE Tables SET Name = @Name WHERE Id = @Id";
                    ht.Add("@Id", Id);
                    ht.Add("@Name", txt_Name.Text);
                }

                try
                {
                    int result = ExecuteSQL(qry, ht);
                    if (result > 0)
                    {
                        MessageBox.Show("Saved successfully!");
                        Id = 0;
                        txt_Name.Text = "";
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


        private void frmTableAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
