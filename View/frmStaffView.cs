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
    public partial class frmStaffView : SampleView
    {
        public frmStaffView()
        {
            InitializeComponent();
        }

        private void frmStaffView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            // SQL sorgusu: Staff tablosundaki tüm verileri getir
            string qry = "SELECT * FROM Staff where Name like '%" + txt_Search.Text + "%' ";

            // ListBox oluştur ve DataGridView'deki sütunları ekle
            ListBox lb = new ListBox();
            lb.Items.Add(dgvSno);   // DataGridView'deki ID sütunu
            lb.Items.Add(dgvName); // DataGridView'deki Name sütunu
            lb.Items.Add(dgvPhone); // DataGridView'deki Phone sütunu
            lb.Items.Add(dgvRole); // DataGridView'deki Role sütunu


            // Verileri yükle
            DatabaseHelper.LoadData(qry, dataGridView1, lb);
        }

        public override void btn_Add_Click(object sender, EventArgs e)
        {
            Model.frmStaffAdd frm = new Model.frmStaffAdd();
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
                frmStaffAdd frm = new frmStaffAdd();
                frm.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvSno"].Value);
                frm.txt_Name.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.txt_Phone.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvPhone"].Value);
                frm.cbRole.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvRole"].Value);

                frm.ShowDialog();
                GetData();
            }
            else if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                // Silme işlemi
                int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvSno"].Value);
                DialogResult result = MessageBox.Show(
                    "Bu personeli silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string qry = "DELETE FROM Staff WHERE Id = @Id";
                        Hashtable ht = new Hashtable
                {
                    { "@Id", Id }
                };

                        DatabaseHelper.SQLite(qry, ht); // DatabaseHelper sınıfı üzerinden SQL işlemi
                        MessageBox.Show("Personel başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
