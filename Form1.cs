using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VertGird
{
    public partial class Form1 : Form
    {
        DataTable tbl = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Datatable, remember this can be your dataset from your DB.
            //I don't create DBs I'm not a DBA  
            this.tbl = GetCoutries();
            LoadNomalGrid();
            LoadVerticalGrid();

            //lets cheat to hide column header on vertical grid
            this.dataGridView2.ColumnHeadersVisible = false;

        }

        /// <summary>
        /// We're going to have some nested loops here.
        /// </summary>
        private void LoadVerticalGrid()
        {
            //first lets create a new table which will have the new vertical structure
            DataTable VerticalTable = new DataTable();

            //get number of rows to make new columns
            for (int i = 0; i < this.tbl.Rows.Count; i++)
            {
                VerticalTable.Columns.Add();
            }
            DataRow row;
            //New rows for the columns
            for (int j = 0; j < this.tbl.Columns.Count; j++)
            {

                row = VerticalTable.NewRow();
                row[0] = this.tbl.Columns[j].ToString();// set first field of the row to be coulmn fro the table
                for (int k = 1; k < this.tbl.Rows.Count; k++)
                {

                    row[k] = this.tbl.Rows[k - 1][j];//noticed the -1 on the rows count
                }
                VerticalTable.Rows.Add(row);
            }

            VerticalTable.AcceptChanges();
            this.dataGridView2.DataSource = VerticalTable.DefaultView;

        }

        private void LoadNomalGrid()
        {
            this.dataGridView1.DataSource = this.tbl.DefaultView;
        }

        private DataTable GetCoutries()
        {
            DataTable table = new DataTable("Coutries");
            //Add Cloumns
            table.Columns.Add("Country");
            table.Columns.Add("Currency");
            table.Columns.Add("Population");
            table.Columns.Add("President");

            //Populate dummy data
            table.Rows.Add(new object[] { "South Africa", "ZAR", "48 Mil", "Comrade JZ" });
            table.Rows.Add(new object[] { "USA", "USD", "250 Mil", "Obama" });
            table.Rows.Add(new object[] { "Zim", "ZMD", "20 Mil", "Uncle Bob" });
            table.Rows.Add(new object[] { "Nigeria", "Naira", "200 Mil", "Goodluck Jonathan" });

            table.AcceptChanges();

            return table;
        }

     

       

      
    }
}
