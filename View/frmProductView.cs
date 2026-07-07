using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp12.LoginForm;
using WindowsFormsApp12.Model;

namespace WindowsFormsApp12.View
{
    public partial class frmProductView : SampleView
    {
        public frmProductView()
        {
            InitializeComponent();
        }

        private void frmProductView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            string qry = "SELECT p.Id, p.Name AS ProductName, p.Price, c.Name AS CategoryName " +
                         "FROM Products p " +
                         "INNER JOIN Category c ON p.CategoryID = c.Id " +
                         "WHERE p.Name LIKE '%" + txt_Search.Text.Replace("'", "''") + "%'";

            // ListBox oluştur ve DataGridView sütunlarını ekle
            ListBox lb = new ListBox();
            lb.Items.Add(dgvSno);       // p.Id
            lb.Items.Add(dgvName);     // p.Name AS ProductName
            lb.Items.Add(dgvPrice);    // p.Price
            lb.Items.Add(dgvcat);      // c.Name AS CategoryName

            // Verileri yükle
            try
            {
                DatabaseHelper.LoadData(qry, dataGridView1, lb);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public override void btn_Add_Click(object sender, EventArgs e)
        {
            Model.frmProductAdd frm = new Model.frmProductAdd();
            frm.ShowDialog();
            GetData();
        }

        public override void txt_Search_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                // Düzenleme işlemi
                frmProductAdd frm = new frmProductAdd();
                frm.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvSno"].Value);
                frm.txt_Name.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.txt_Price.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvPrice"].Value);
                frm.cbCategory.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvcat"].Value);

                frm.ShowDialog();
                GetData();
            }
            else if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                // Silme işlemi
                int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvSno"].Value);
                DialogResult result = MessageBox.Show(
                    "Bu ürünü silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string qry = "DELETE FROM Products WHERE Id = @Id";
                        Hashtable ht = new Hashtable
                        {
                            { "@Id", Id }
                        };

                        DatabaseHelper.SQLite(qry, ht); // DatabaseHelper sınıfı üzerinden SQL işlemi
                        MessageBox.Show("Ürün başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
