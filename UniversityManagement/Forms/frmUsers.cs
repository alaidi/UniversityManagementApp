using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityManagement.BusinessLayer;
using UniversityManagement.DataLayer;
using UniversityManagement.Entities;

namespace UniversityManagement.Forms
{
    public partial class frmUsers : Form
    {
        private readonly IGenericRepository<Users> _userRepository = new GenericRepository<Users>();
        public frmUsers()
        {
            InitializeComponent();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            var dt = _userRepository.GetData();
            dgvTable.DataSource =dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var user = new Users();
            //user.Id = Convert.ToInt32(txtId.Text);
            user.UserName = txtName.Text;
            user.Password = txtPassword.Text;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _userRepository.Add(user);
            GetData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var user = new Users();
            user.Id = Convert.ToInt32(txtId.Text);
            user.UserName = txtName.Text;
            user.Password = txtPassword.Text;

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _userRepository.Update(user);

           GetData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            _userRepository.Delete(id);
            GetData();
        }
    }
}
