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
    public partial class frmCollege : Form
    {
       // private CollegeRepository _collegeRepository = new CollegeRepository();
        private IGenericRepository<College> _collegeRepository = new GenericRepository<College>();
        public frmCollege()
        {
            InitializeComponent();
        }

        private void frmCollege_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            dgvTable.DataSource = _collegeRepository.GetData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            _collegeRepository.Add(new College{Name = name});
            GetData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var id =Convert.ToInt32(txtId.Text);
            var name = txtName.Text;
            _collegeRepository.Update(new College { Name = name ,Id = id});
            GetData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtId.Text);
            _collegeRepository.Delete(id);
            //_collegeRepository.Delete(new College{Id = id});
            GetData();
        }
      
        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex<0)
            {
                return;
            }
            txtId.Text = dgvTable.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvTable.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
