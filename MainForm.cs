using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            btn_Exit.Location = new Point(panel2.Width - btn_Exit.Width - 10, 10);

            // Panelin boyutları değiştiğinde butonun konumunu güncelle
            panel2.Resize += (s, args) =>
            {
                btn_Exit.Location = new Point(panel2.Width - btn_Exit.Width - 10, 10);
            };
        }

        public void AddControls( Form f)
        {
            CenterPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();


        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void btn_Categories_Click(object sender, EventArgs e)
        {
            AddControls(new View.frmCategoryView());
        }

        private void btn_Table_Click(object sender, EventArgs e)
        {

            AddControls(new View.frmTableView());
        }

        private void btn_Staff_Click(object sender, EventArgs e)
        {
            AddControls(new View.frmStaffView());
        }

        private void btn_Product_Click(object sender, EventArgs e)
        {
            AddControls(new View.frmProductView());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddControls(new frmChart());
        }
    }
}
