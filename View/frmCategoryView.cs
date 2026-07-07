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
using WindowsFormsApp12.Model;
using static WindowsFormsApp12.LoginForm;

namespace WindowsFormsApp12.View
{
    public partial class frmCategoryView : SampleView
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            // SQL sorgusu: Category tablosundaki tüm verileri getir
            string qry = "SELECT * FROM Category where Name like '%" + txt_Search.Text + "%' ";

            // ListBox oluştur ve DataGridView'deki sütunları ekle
            ListBox lb = new ListBox();
            lb.Items.Add(dgvSno);   // DataGridView'deki ID sütunu
            lb.Items.Add(dgvName); // DataGridView'deki Name sütunu

            // Verileri yükle
            DatabaseHelper.LoadData(qry, dataGridView1, lb);
        }


        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            GetData();

        }
        public override void btn_Add_Click(object sender, EventArgs e)
        {
            Model.frmCategoryAdd frm = new Model.frmCategoryAdd();
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
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvSno"].Value);
                frm.txt_Name.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.ShowDialog();
                GetData();
            }
            else if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                // Silme işlemi
                int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvSno"].Value);
                DialogResult result = MessageBox.Show(
                    "Bu kategoriyi silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string qry = "DELETE FROM Category WHERE Id = @Id";
                        Hashtable ht = new Hashtable
                {
                    { "@Id", Id }
                };

                        DatabaseHelper.SQLite(qry, ht); // DatabaseHelper sınıfı üzerinden SQL işlemi
                        MessageBox.Show("Kategori başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

