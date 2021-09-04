using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UniversityManagement.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace UniversityManagement
{
    public partial class frmMain : Form
    {
        public frmMain(frmCollege frmCollege, frmDepartment frmDepartment, frmUsers frmUsers, frmInstructor frmInstructor)
        {
            _frmCollege = frmCollege;
            _frmDepartment = frmDepartment;
            _frmUsers = frmUsers;
            _frmInstructor = frmInstructor;
            InitializeComponent();
        }

        public frmMain()
        {
            _frmCollege = new frmCollege();
            _frmDepartment = new frmDepartment();
            _frmUsers = new frmUsers();
            _frmInstructor = new frmInstructor();

            InitializeComponent();
        }
        private readonly frmCollege _frmCollege;
        private readonly frmDepartment _frmDepartment;
        private readonly frmUsers _frmUsers;
        private readonly frmInstructor _frmInstructor;
        private void btnCollege_Click(object sender, EventArgs e)
        {
            GetForms(_frmCollege,sender, btnCollege.BackColor);
        }
        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                .Concat(controls)
                .Where(c => c.GetType() == type);
        }
        private void GetForms(Form form, object sender, Color backColor)
        {
            lblHeader.Text = ((Button) sender).Text;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelDeskTop.Controls.Add(form);
            panelDeskTop.Tag = form;
            form.BringToFront();
           // form.BackColor = backColor;
            // form
            form.AutoScaleDimensions = new SizeF(9F, 21F);
            form.AutoScaleMode = AutoScaleMode.Font;
            form.BackColor = Color.White;
           // form.ClientSize = new Size(371, 444);
            form.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            form.Margin = new Padding(4, 5, 4, 5);

           // var textBoxes = GetAll(form, typeof(TextBox));
           
            form.Show();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            // GetForms(new frmDepartment());
            GetForms(_frmDepartment, sender, btnDepartment.BackColor);

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            GetForms(_frmUsers, sender, btnUsers.BackColor);
        }

        private void btnInstructor_Click(object sender, EventArgs e)
        {
            GetForms(_frmInstructor, sender, btnInstructor.BackColor);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
          //  panelDeskTop.Controls.Clear();
            foreach (Control item in panelDeskTop.Controls.OfType<Form>().ToList())
            {
                panelDeskTop.Controls.Remove(item);
            }
        }
    }
}
