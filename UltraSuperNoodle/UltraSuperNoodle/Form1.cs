using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.Administration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace UltraSuperNoodle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerManager serverManager = new ServerManager();
            //var apps;
            serverManager.Sites.ToList().ForEach(site => site.Applications.Where(app => app.Path != @"/").ToList().ForEach(app => comboBoxAppName.Items.Add(app)));

            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            //var x = instance.GetDataSources();
            //x = instance.GetDataSources();

            //var z = 
            //string constring = "server=" + x.Rows[0].ItemArray[0] + ";Integrated Security = sspi";
            string constring = "server=localhost;Integrated Security = sspi";
            using (var con = new SqlConnection(constring))
            using (var da = new SqlDataAdapter("SELECT Name FROM master.sys.databases", con))
            {
                var ds = new DataSet();
                da.Fill(ds);
                var x3 = ds.Tables[0].Rows.Cast<DataRow>().Select(x2 => x2["Name"].ToString());
                comboBoxDbNames.DataSource = x3.ToList();
                
            }
            
        }

        private void comboBoxDbNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string databaseName = (string)((ComboBox)sender).SelectedItem;
            string constring = "server=localhost;Integrated Security = sspi";
            using (var con = new SqlConnection(constring))
                try {
                    using (var da = new SqlDataAdapter("select name from " + databaseName + ".sys.sysusers where islogin = 1", con))
                        {
                            var ds = new DataSet();
                            da.Fill(ds);
                            var x3 = ds.Tables[0].Rows.Cast<DataRow>().Select(x2 => x2["Name"].ToString());
                            comboBoxUserNames.DataSource = x3.ToList();

                        }
                    }
            catch(Exception ex)
                {               
                }
        }

        private void comboBoxAppName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Microsoft.Web.Administration.Application applicationName = (Microsoft.Web.Administration.Application)((ComboBox)sender).SelectedItem;

            var physicalPath = applicationName.VirtualDirectories[0].PhysicalPath + @"\web.config";

            XDocument xDoc = XDocument.Load(physicalPath);

            foreach (XElement element in xDoc.Root.Elements())
            {
                if (element.Name.LocalName.Equals("connectionStrings"))
                {
                    var yy = 5;
                }
            }

            
        }

        private void button_save_Click(object sender, EventArgs e)
        {
           

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
         //   if (keyData = "Control")
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                Form searchForm = new SearchForm(this.richTextBox1);
                searchForm.Show();
            }
        }
    }
}
