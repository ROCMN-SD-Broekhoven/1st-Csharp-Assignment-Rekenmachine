using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Linq.Expressions;

namespace Rekenmachine
{
    public partial class Form1 : Form
    {
        bool needNum = true;
        bool addComma = false;
        bool commaAdded = false;
        bool lastNrMin = false;
        List<double> numbers = new List<double>();
        List<string> ops = new List<string>();
        string screentext = "";
        string tempNr = "";

        public Form1()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            addNumber("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addNumber("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addNumber("4");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            addOperator("+");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            addNumber("5");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addNumber("3");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addNumber("6");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (needNum == true && lastNrMin == false)
            {
                addNumber("-");
            }
            else
            {
                addOperator("-");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            addNumber("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            addNumber("8");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            addNumber("9");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            addOperator(":");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            calculate();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            addNumber("0");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (addComma == true)
            {
                addComma = false;
                button11.BackColor = Color.White;
            }
            else
            {
                addComma = true;
                button11.BackColor = Color.Yellow;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            addOperator("X");
        }

        private void addNumber(string nr)
        {
            if (addComma == true && commaAdded == false)
            {
                tempNr = tempNr + ".";
                screentext = screentext + ".";
                addComma = false;
                commaAdded = true;
                button11.BackColor = Color.White;
            }
            tempNr = tempNr + nr;
            screentext = screentext + nr;
            refreshScreenText();
            if (nr != "-")
            {
                needNum = false;
            }
            else
            {
                lastNrMin = true;
            }
            Console.WriteLine(tempNr);
        }

        private void addOperator(string oper)
        {
            if (needNum == false)
            {
                try
                {
                    numbers.Add(double.Parse(tempNr));
                }
                catch (FormatException)
                {
                    tempNr = 0 + tempNr;
                    numbers.Add(double.Parse(tempNr));
                }
                ops.Add(oper);
                screentext = screentext + oper;
                refreshScreenText();
                tempNr = null;
                needNum = true;
                commaAdded = false;
            }
        }

        private void calculate()
        {
            bool passedNullTest = false;

            try
            {
                numbers.Add(double.Parse(tempNr));
                passedNullTest = true;
            }
            catch (FormatException)
            {
                tempNr = 0 + tempNr;
                numbers.Add(double.Parse(tempNr));
                passedNullTest = true;
            }
            catch (ArgumentNullException)
            {
                screentext = "Syntax Error";
                refreshScreenText();
                screentext = "";
                numbers.Clear();
                ops.Clear();
            }

            if (passedNullTest == true)
            {
                string math = "";
                for (int i = 0; i < numbers.Count; i++)
                {
                    math = math + numbers[i];
                    if (ops.ElementAtOrDefault(i) != null)
                    {
                        string opSwitch = ops[i];

                        switch (opSwitch)
                        {
                            case "+":
                                math = math + "+";
                                break;
                            case "-":
                                math = math + "-";
                                break;
                            case ":":
                                math = math + "/";
                                break;
                            default:
                                math = math + "*";
                                break;
                        }
                    }
                }
                Console.WriteLine(math);
                var loDataTable = new DataTable();
                var loDataColumn = new DataColumn("Eval", typeof(double), math);
                loDataTable.Columns.Add(loDataColumn);
                loDataTable.Rows.Add(0);
                double answer = Convert.ToDouble(loDataTable.Rows[0]["Eval"]);
                numbers = new List<double>();
                ops = new List<string>();
                screentext = answer.ToString();
                if (screentext == "∞" || screentext == "NaN")
                {
                    screentext = "Math Error";
                }
                refreshScreenText();
                screentext = "";
                tempNr = "";
                lastNrMin = false;
                needNum = true;
                addComma = false;
                commaAdded = false;
                button11.BackColor = Color.White;
            }

        }

        private void refreshScreenText()
        {
            label1.Text = screentext;
        }
    }
}
