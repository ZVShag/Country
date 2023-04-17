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
        SqlConnection SqlConnection=null;
        SqlCommandBuilder SqlCommandBuilder=null;
        SqlDataAdapter SqlDataAdapter=null;
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
            try 
            {
                SqlDataAdapter=new SqlDataAdapter("SELECT *,'DELETE' As [Delete] From Country",SqlConnection);
                SqlCommandBuilder=new SqlCommandBuilder(SqlDataAdapter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
