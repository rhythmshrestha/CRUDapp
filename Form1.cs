using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace CRUDapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=RHYTHM\\SQLEXPRESS;Initial Catalog=CRUDapp;Integrated Security=True;TrustServerCertificate=True");
        SqlCommand cmd = new SqlCommand();

        private void DisplayData()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM employeeTable", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void ClearData()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtIndex.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(txtIndex.Text == "" && txtName.Text != "" && txtPhone.Text != "" && txtEmail.Text != "" && txtAddress.Text != "")
            {
                cmd.Connection = conn;
                string query = $"insert into employeeTable values('{txtName.Text.Trim().ToLower()}','{txtPhone.Text.ToLower().Trim()}','{txtEmail.Text.ToLower().Trim()}','{txtAddress.Text.ToLower().Trim()}','{dateTimePicker.Value}')";
                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteNonQuery();
                ClearData();
                conn.Close();
                DisplayData();
            }
            else if(txtIndex.Text != "")
            {
               //Do nothing as save btn has nothing to do with the above expression.
            }
            else
            {
                MessageBox.Show("Fill every fields");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtIndex.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtPhone.Text = row.Cells[2].Value.ToString();
                txtEmail.Text = row.Cells[3].Value.ToString();
                txtAddress.Text = row.Cells[4].Value.ToString();
                dateTimePicker.Value = (DateTime)row.Cells[5].Value;
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtIndex.Text != "" && txtName.Text != "" && txtPhone.Text != "" && txtEmail.Text != "" && txtAddress.Text != "")
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update employeeTable set full_name='" + txtName.Text.Trim().ToLower() + "',phone_no='" + txtPhone.Text.ToLower().Trim() + "',email='" + txtEmail.Text.ToLower().Trim() + "',physical_address='" + txtAddress.Text.ToLower().Trim() + "',date_of_hire='" + dateTimePicker.Value + "' where index_num='" + txtIndex.Text + "' ";
                cmd.ExecuteNonQuery();
                conn.Close();
                DisplayData();
                ClearData();
            }
            else if(txtIndex.Text == "")
            {
               //Do nothing as update btn has nothing to do with the above expression.
            }
            else
            {
                MessageBox.Show("Fill every field");
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            cmd.Connection = conn;
            string query = $"delete employeeTable where index_num='{txtIndex.Text}'";
            cmd.CommandText = query;
            conn.Open();
            cmd.ExecuteNonQuery();
            dataGridView1.DataSource = query;
            ClearData();
            conn.Close();
            DisplayData();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text != "")
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from employeeTable where full_name='" + txtSearch.Text.ToLower().Trim() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            else
            {
                DisplayData();
            }
        }

        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            txtSearch.Text = "";
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar);
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar);
        }
    }
}
