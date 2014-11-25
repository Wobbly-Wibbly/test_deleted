using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Words.Forms
{
    public partial class Question : Form
    {
        public Question()
        {
            InitializeComponent();
        }

        public Question(string Label)
        {
            InitializeComponent();
            label1.Text = Label;
        }

        public Question(string Label, string Input)
        {
            InitializeComponent();
            label1.Text = Label;
            textBox1.Text = Input;
        }

        private void Question_Load(object sender, EventArgs e)
        {

        }

        public string Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
    }
}
