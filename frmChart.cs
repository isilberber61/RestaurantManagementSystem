using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp12
{
    public partial class frmChart : Form
    {
        public frmChart()
        {
            InitializeComponent();
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
  
                string connectionString = "Data Source=RestoranYönetimi.db;Version=3;";  // Veritabanı bağlantısı
                string query = "SELECT Name, Price FROM Products";  // Ürünlerin adı ve fiyatı

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Chartı Temizle

                    chart1.Series.Clear();
                    chart1.Series.Add("Product Name");
                    chart1.Series["Product Name"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                    //Verileri charta ekle
                    while (reader.Read())
                    {
                        string Product = reader["Name"].ToString();
                        double value = Convert.ToDouble(reader["Price"]);
                        chart1.Series["Product Name"].Points.AddXY(Product, value);
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata : " + ex.Message);

                }
            }
        }
    }

    }
