using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltraSuperNoodle
{
    public partial class SearchForm : Form
    {
        private RichTextBox richTextBox;


        public SearchForm()
        {
            InitializeComponent();
        }

        public SearchForm(RichTextBox richTextBox)
        {
            InitializeComponent();
            this.richTextBox = richTextBox;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            int index = richTextBox.Find(text,RichTextBoxFinds.None);

            

            richTextBox.SelectionColor = Color.Red;
            // Find the end index. End Index = number of characters in textbox
            int endindex = text.Length;
            // Highlight the search string
            if (index >= 0)
                richTextBox.Select(index, endindex);
            // mark the start position after the position of
            // last search string
            // start = startindex + endindex;

            richTextBox.SelectionLength = 0;
        }
    }
}
