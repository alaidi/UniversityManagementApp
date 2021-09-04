using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class frmDepartment : Form
    {
        private IGenericRepository<Department> _departmentRepository = new GenericRepository<Department>();
        private IGenericRepository<College> _collegeRepository = new GenericRepository<College>();
        public frmDepartment()
        {
            InitializeComponent();
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            var dtCollege = _collegeRepository.GetData();
            cobCollege.DataSource = dtCollege;
            cobCollege.ValueMember = "id";
            cobCollege.DisplayMember = "name";
            //dgvTable.AutoGenerateColumns = false;
            //dgvTable.Columns.Add(new DataGridViewTextBoxColumn()
            //{
            //    HeaderText = "Id",
            //    DataPropertyName = "Id"
            //});
            //dgvTable.Columns.Add(new DataGridViewTextBoxColumn()
            //{
            //    HeaderText = "Name",
            //    DataPropertyName = "Name"
            //});
            //dgvTable.Columns.Add(new DataGridViewTextBoxColumn()
            //{
            //    HeaderText = "College",
            //    DataPropertyName = "College",
            //});

            dgvTable.DataSource = _departmentRepository.GetData();
            //dgvTable.CellFormatting += CellFormatting;
            //dgvTable.CellParsing += CellParsing;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtId.Text);
            var name = txtName.Text;
            var collegeId = (int)cobCollege.SelectedValue;
            _departmentRepository.Add(new Department
            {
                Id = id,
                Name = name,
                CollegeId = collegeId
            });
            GetData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtId.Text);
            var name = txtName.Text;
            var collegeId = (int)cobCollege.SelectedValue;
            _departmentRepository.Update(new Department
            {
                Id = id,
                Name = name,
                CollegeId = collegeId
            });
            GetData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtId.Text);
            _departmentRepository.Delete(id);
            GetData();
        }
    }
}
