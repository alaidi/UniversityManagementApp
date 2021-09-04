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
using UniversityManagement.Entities;

namespace UniversityManagement.Forms
{
    public partial class frmInstructor : Form
    {
        private IGenericRepository<Instructor> _instructorRepository = new GenericRepository<Instructor>();

        public frmInstructor()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            dgvTable.DataSource = _instructorRepository.GetData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var ins = new Instructor();
            ins.Name = txtName.Text;
            ins.Degree = txtDgree.Text;
            ins.Certificate = txtCertificate.Text;

            _instructorRepository.Add(ins);
            GetData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var ins = new Instructor();
            ins.Id = Convert.ToInt32(txtId.Text);
            ins.Name = txtName.Text;
            ins.Degree = txtDgree.Text;
            ins.Certificate = txtCertificate.Text;

            _instructorRepository.Update(ins);
            GetData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtId.Text);
            _instructorRepository.Delete(id);
            GetData();
        }

        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            txtId.Text = dgvTable.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvTable.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void frmInstructor_Load(object sender, EventArgs e)
        {
            GetData();
        }

    }
}
