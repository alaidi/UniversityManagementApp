using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityManagement.DataLayer;

namespace UniversityManagement.Forms
{
    public partial class frmLogin : Form
    {
        private DataAccess da = new DataAccess();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = txtUserName.Text;
            var password = txtPassword.Text;
            var sql = $"Select Password from Users where UserName=@userName";

            var obj = da.GetSingleItem(sql, new SqlParameter[]
            {
                new SqlParameter("@userName",userName)
            });

            if (obj != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, obj.ToString()))
                {
                    var frm = new frmMain(
                        new frmCollege(),
                        new frmDepartment(),
                        new frmUsers(),
                        new frmInstructor()
                    );
                   // var frm = new frmMain();
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show(@"User Name or Password mismatch");
                }

            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
