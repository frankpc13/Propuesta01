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
using System.Configuration;

namespace Propuesta1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["frank"].ConnectionString);

        public void ClientList()
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("Usp_ListaCLientes_Neptuno", connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet dataSet = new DataSet())
                {
                    adapter.Fill(dataSet, "Clientes");
                    dataGridClient.DataSource = dataSet.Tables["Clientes"];
                    labelTotal.Text = dataSet.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ClientList();
        }

        private void EditTextName_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridClient.DataSource;
            bs.Filter = dataGridClient.Columns[2].HeaderText.ToString() + " LIKE '%" + EditTextName.Text + "%' ";
            dataGridClient.DataSource = bs;
            labelTotal.Text = dataGridClient.RowCount.ToString();
        }
    }
}
