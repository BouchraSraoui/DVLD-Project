using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;

namespace DVLD___Driving_License_Management.Controls
{
    public partial class ctrlFilterBy : UserControl
    {
        public delegate void FilterData(string filter = "",object field = null);
        public event FilterData Filter;


        public Dictionary<string,string> FilterBy { get; set; }
       
        public string ComboboxSelectedItem
        {
            set
            {
                comboBox1.Text = value.Replace(" ", "");
            }
        }
        public string TextBoxValue {
            set
            {
                textBox1.Text = value.Replace(" ", "");
            }
        }
        public ctrlFilterBy()
        {
            InitializeComponent();
        }
        private void _FillCombobox()
        {
            foreach (string filtername in FilterBy.Keys)
            {
                comboBox1.Items.Add(filtername);
            }
        }
        public void InitializeControl()
        {
            _FillCombobox();
            comboBox1.SelectedIndex = 0;
        }
        private void _ExecuteFilter()
        {
            Filter?.Invoke(comboBox1.Text.Replace(" ",""),
                           textBox1.Text.Replace(" ", ""));
        }
     
        private void ctrlFilterBy_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = (FilterBy[comboBox1.Text] != "");

            if (!textBox1.Visible)
            {
                _ExecuteFilter();    
            }

            textBox1.Clear();
        }

        

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if(FilterBy[comboBox1.Text] == "int")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please enter the value",
                        "No value");
                    return;
                }
                e.SuppressKeyPress = true;
                _ExecuteFilter();
            }
        }
    }
}
