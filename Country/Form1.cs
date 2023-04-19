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

namespace Country
{
    public partial class Form1 : Form
    {
        private SqlConnection SqlConnection=null;
        private SqlCommandBuilder SqlCommandBuilder=null;
        private SqlDataAdapter SqlDataAdapter=null;
        private DataSet dataSet = new DataSet();
        private bool push=false;
        private bool newRowAdding = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection=new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Studen 2\source\repos\Network\Country\Country\Database1.mdf"";Integrated Security=True");

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            обновитьToolStripMenuItem.Enabled = true;
            try 
            {
                SqlDataAdapter=new SqlDataAdapter("SELECT *,'Delete' As [Delete] From Country",SqlConnection);
                SqlCommandBuilder=new SqlCommandBuilder(SqlDataAdapter);
                SqlCommandBuilder.GetDeleteCommand();
                SqlCommandBuilder.GetInsertCommand();
                SqlCommandBuilder.GetUpdateCommand();
                SqlDataAdapter.Fill(dataSet,"Country");
                dataGridView1.DataSource = dataSet.Tables["Country"];

                for (int i =0;i<dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell lnk=new DataGridViewLinkCell();
                    dataGridView1[6, i] = lnk;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSet = new DataSet();
            dataGridView1.DataSource = dataSet.Tables;
            try 
            {
                
            SqlDataAdapter.Fill(dataSet, "Country");
            dataGridView1.DataSource = dataSet.Tables["Country"];

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewLinkCell lnk = new DataGridViewLinkCell();
                dataGridView1[6, i] = lnk;
            }

        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    string t = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (t=="Delete")
                    {
                        if (MessageBox.Show("Вы действительно хотите удалить строку?","Удалить",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (MessageBox.Show("Вы хотите удалить строку только в таблице?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int rindex = e.RowIndex;
                                dataGridView1.Rows.RemoveAt(rindex);
                                dataSet.Tables["Country"].Rows.RemoveAt(e.RowIndex);
                                SqlDataAdapter.Update(dataSet, "Country");
                                
                            }

                        }
                    }
                    else
                    if (t=="Insert")
                    {
                        int ind = dataGridView1.Rows.Count - 2;
                        DataRow row = dataSet.Tables["Country"].NewRow();
                        row["Name"] = dataGridView1.Rows[ind].Cells["Name"].Value;
                        row["Capital"] = dataGridView1.Rows[ind].Cells["Capital"].Value;
                        row["Population"] = dataGridView1.Rows[ind].Cells["Population"].Value;
                        row["Square"] = dataGridView1.Rows[ind].Cells["Square"].Value;
                        row["Part"] = dataGridView1.Rows[ind].Cells["Part"].Value;
                        dataSet.Tables["Country"].Rows.Add(row);
                        dataSet.Tables["Country"].Rows.RemoveAt(dataSet.Tables["Country"].Rows.Count - 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                        int z=SqlDataAdapter.Update(dataSet, "Country");
                        MessageBox.Show("Успешно добавлено"+z.ToString()+" записей!","",MessageBoxButtons.OK);
                        newRowAdding = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try 
            {
                if (newRowAdding==false)
                {
                    newRowAdding= true;
                    int lastrow = dataGridView1.RowCount - 2;
                    DataGridViewRow row=dataGridView1.Rows[lastrow];
                    DataGridViewLinkCell cell=new DataGridViewLinkCell();
                    dataGridView1[6,lastrow] = cell;
                    row.Cells["Delete"].Value = "Insert";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
