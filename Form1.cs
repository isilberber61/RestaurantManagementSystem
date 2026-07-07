using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;
using System.Data.SqlClient;

namespace WindowsFormsApp12
{
    public partial class LoginForm : Form
    {

        string connectionString;

        string connPat = "RestoranYönetimi.db";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {

            string connectionString = "Data Source =RestoranYönetimi.db;Version=3;";


            // Kullanıcıdan gelen giriş bilgilerini al
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQLite bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL sorgusu
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        // Sorguyu çalıştır ve sonucu kontrol et
                        int userExists = Convert.ToInt32(command.ExecuteScalar());
                        if (userExists > 0)
                        {
                            MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Yeni formu göster ve mevcut formu gizle
                            MainForm MainForm = new MainForm(); // MainForm oluştur
                            MainForm.Show();                    // Yeni formu göster
                            this.Hide();                        // LoginForm'u gizle


                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        // SQLite için CRUD işlemi metodu
        public static int ExecuteSQL(string query, Hashtable parameters)
        {
            int result = 0;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=RestoranYönetimi.db;Version=3;"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        // Parametreleri ekle
                        foreach (DictionaryEntry param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key.ToString(), param.Value ?? DBNull.Value);
                        }
                       

                        // Bağlantıyı aç ve sorguyu çalıştır
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }


        //for loading data from database 
        // SQLite için veritabanından veri yükleme metodu
        public static void LoadData(string query, DataGridView gridView, ListBox listBox)
        {

            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=RestoranYönetimi.db;Version=3;"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // ListBox'taki sütun adlarını bağla
                            for (int i = 0; i < listBox.Items.Count; i++)
                            {
                                string columnName = ((DataGridViewColumn)listBox.Items[i]).Name;
                                gridView.Columns[columnName].DataPropertyName = dataTable.Columns[i].ToString();
                            }

                            // Mevcut DataTable'den sütunları DataGridView'e eşleştiriyorsanız:
                            for (int i = 0; i < dataTable.Columns.Count; i++)
                            {
                                if (dataTable.Columns[i].ColumnName == "Image")
                                    continue; // Eğer sütun "Image" ise bu sütunu atlıyoruz.

                                gridView.Columns[i].DataPropertyName = dataTable.Columns[i].ColumnName; // Sütunu bağlama
                            }

                            // DataGridView'e veriyi bağla
                            gridView.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        public static class DatabaseHelper
        {
            private static SQLiteConnection con = new SQLiteConnection("Data Source=RestoranYönetimi.db;Version=3;");

            public static void LoadData(string qry, DataGridView gv, ListBox lb)
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(qry, con);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (lb.Items.Count != dt.Columns.Count)
                    {
                        throw new Exception("ListBox ve DataTable sütun sayıları uyuşmuyor.");
                    }

                    for (int i = 0; i < lb.Items.Count; i++)
                    {
                        string colName = ((DataGridViewColumn)lb.Items[i]).Name;
                        if (i >= dt.Columns.Count)
                        {
                            throw new Exception($"DataTable'da index {i} mevcut değil.");
                        }
                        gv.Columns[colName].DataPropertyName = dt.Columns[i].ColumnName;
                    }

                    gv.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata Detayı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }



            }
            public static void SQLite(string query, Hashtable parameters)
            {
                try
                {
                    using (SQLiteConnection con = new SQLiteConnection("Data Source=RestoranYönetimi.db;Version=3;"))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                        {
                            // Parametreleri ekle
                            foreach (DictionaryEntry param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key.ToString(), param.Value);
                            }

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Veritabanı hatası: " + ex.Message);
                }
            }


            public static void CBFill(string qry, ComboBox cb)
            {
                SQLiteCommand cmd = new SQLiteCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);


                cb.ValueMember = "Id";   // ID değerini sakla
                cb.DisplayMember = "CategoryName"; // Görünen adı ayarla
                cb.DataSource = dt;
                cb.SelectedIndex = -1;


            }

        }

    }
}

