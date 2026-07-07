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
    public partial class frmStaffAdd : SampleAdd
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }

        public int Id = 0;

        public override void btn_Save_Click(object sender, EventArgs e)
        {
            // Giriş kontrolleri
            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("Lütfen isim giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Phone.Text))
            {
                MessageBox.Show("Lütfen telefon numarası giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbRole.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir rol seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string qry = "";
            Hashtable ht = new Hashtable();

            if (Id == 0) // Insert işlemi
            {
                qry = "INSERT INTO Staff (Name, Phone, Role) VALUES (@Name, @Phone, @Role)";
                ht.Add("@Name", txt_Name.Text);
                ht.Add("@Phone", txt_Phone.Text);
                ht.Add("@Role", cbRole.SelectedItem.ToString());
            }
            else // Update işlemi
            {
                qry = "UPDATE Staff SET Name = @Name, Phone = @Phone, Role = @Role WHERE Id = @Id";
                ht.Add("@Id", Id);
                ht.Add("@Name", txt_Name.Text);
                ht.Add("@Phone", txt_Phone.Text);
                ht.Add("@Role", cbRole.SelectedItem.ToString());
            }

            try
            {
                int result = ExecuteSQL(qry, ht);
                if (result > 0)
                {
                    MessageBox.Show("Saved successfully!");
                    Id = 0;
                    txt_Name.Text = "";
                    txt_Phone.Text = "";
                    cbRole.SelectedIndex = -1; // ComboBox'u sıfırla
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
