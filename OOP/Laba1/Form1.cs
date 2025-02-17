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

namespace Laba1
{
    public partial class Form1 : Form
    {
        public string ActiveButton = "";
        int num1;
        int num2;
        int sum;
        
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyPreview = true;
        }



        private void AND_Click(object sender, EventArgs e)
        {
            ActiveButton = "AND";
            Number2.Visible = true;
            label2.Visible = true;
        }

        private void OR_Click(object sender, EventArgs e)
        {
            ActiveButton = "OR";
            Number2.Visible = true;
            label2.Visible = true;
        }

        private void IsklOR_Click(object sender, EventArgs e)
        {
            ActiveButton = "IsklOR";
            Number2.Visible = true;
            label2.Visible = true;
        }

        private void NOT_Click(object sender, EventArgs e)
        {
            ActiveButton = "NOT";
            Number2.Visible = false;
            label2.Visible = false;
        }

        private void Finish_Click(object sender, EventArgs e)
        {
            if (Number1.Text != null && Number2.Text != null)
            {
                try{
                    if (ActiveButton == "NOT")
                    {
                        num1 = int.Parse(Number1.Text);
                    }

                    else
                    {
                        num1 = int.Parse(Number1.Text);
                        num2 = int.Parse(Number2.Text);
                    }
                    switch (ActiveButton)
                    {
                        case "AND":
                            sum = num1 & num2;
                            Result.Text = sum.ToString();
                            break;

                        case "OR":
                            sum = num1 | num2;
                            Result.Text = sum.ToString();
                            break;
                        case "IsklOR":
                            sum = num1 ^ num2;
                            Result.Text = sum.ToString();
                            break;
                        case "NOT":
                            sum = ~num1 + 1;
                            Result.Text = sum.ToString();
                            break;
                    }
                }
                catch {
                    Result.Text = "Неккоректнр";
                    


                }             
            }
           
        }

        private void buttonSeconary_Click(object sender, EventArgs e)
        {
            Result.Text = Convert.ToString(sum, 2);
            
        }

        private void buttonEigth_Click(object sender, EventArgs e)
        {
            Result.Text = Convert.ToString(sum, 8);
        }

        private void buttonTenth_Click(object sender, EventArgs e)
        {
            Result.Text = Convert.ToString(sum, 10);
        }

        private void buttonSixteens_Click(object sender, EventArgs e)
        {
            Result.Text = Convert.ToString(sum, 16);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            num1 = 0;
            num2 = 0;
            sum = 0;
            Number1.Text = "0";
            Number2.Text = "0";
            Result.Text = "";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
