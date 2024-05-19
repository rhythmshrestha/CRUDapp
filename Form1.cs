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

namespace CRUDapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=RHYTHM\\SQLEXPRESS;Initial Catalog=CRUDapp;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM employeeTable", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

    }
}
