using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CallerInfoAttbLib;

namespace CallerInfoAttbClient
{
    public partial class Form1 : Form
    {
        Employee emp = null;

        public Form1()
        {
            InitializeComponent();

            emp = new Employee();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            emp.PropertyChanged += emp_PropertyChanged;

            emp.EmployeeID = int.Parse(textBox1.Text);
            emp.FirstName = textBox2.Text;
            emp.LastName = textBox3.Text;

            MessageBox.Show(emp.AddEmployee());
            
        }

        void emp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MessageBox.Show("Property " + e.PropertyName + " has changed!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
