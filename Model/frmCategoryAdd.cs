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
using System.Xml.Linq;

namespace WindowsFormsApp12.Model
{
    public partial class frmCategoryAdd : SampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }
        public int Id = 0;

        public override void btn_Save_Click(object sender, EventArgs e)
        {
            string qry = "";

            // SQL sorgusunu belirle (Insert veya Update)
            if (Id == 0) // Insert işlemi
            {
                qry = "INSERT INTO Category (Name) VALUES (@Name)";
            }
            else // Update işlemi
            {
                qry = "UPDATE Category SET Name = @Name WHERE Id = @Id";
            }

            // Parametreleri ekle
            Hashtable ht = new Hashtable();
            ht.Add("@Id", Id);
            ht.Add("@Name", txt_Name.Text);

            // Veritabanına kaydet
            if (ExecuteSQL(qry, ht) > 0)
            {
                MessageBox.Show("Saved successfully!");
                Id = 0; // ID'yi sıfırla
                txt_Name.Text = "";
                txt_Name.Focus(); // İmleci Name alanına getir
            }
        }

        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
