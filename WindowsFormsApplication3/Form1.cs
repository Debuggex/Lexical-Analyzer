using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        Compiler_Construction CC = new Compiler_Construction();
       
        private void button1_Click(object sender, EventArgs e)
        {   
            
            string[] linesofcode = File.ReadAllLines("Code.txt");
            if (File.Exists("Token.txt"))
            {
                File.WriteAllText("Token.txt","");
            }

            for (int i = 0; i < linesofcode.Length; i++)
            {
                string codeline = linesofcode[i];
                int linenumber = i + 1;
                string word = null;
                string secondword = null;
                for (int j = 0; j < codeline.Length; j++)
                {
                    int invertedcomma = 0;
                    if (codeline[j]=='"')
                    {

                        invertedcomma++;

                        if (invertedcomma==1)
                        {
                            if (word != null)
                            {
                                CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                                word = null;
                                word = word + codeline[j];
                                
                            }
                            word = word + codeline[j];

                        }
                        while (invertedcomma==1&&j!=codeline.Length-1)
                        {
                            j++;
                            word = word + codeline[j];
                            if (codeline[j]=='"')
                            {
                                invertedcomma++;
                                
                                CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                                word = null;
                            }
                        }
                        continue;
                        
                    }
                    if (codeline[j]=='.')
                    {
                        Regex number = new Regex("^[0-9]+$");
                        bool isnumber = number.IsMatch(word);
                        if (isnumber==true)
                        {
                            j++;
                            Regex numbercharacter = new Regex("^[0-9]*$");
                            isnumber=numbercharacter.IsMatch(codeline[j].ToString());
                            while (codeline[j]!=' '||codeline[j]!=';')
                            {
                                secondword = secondword + codeline[j];
                                if (j!=codeline.Length-1)
                                {
                                    j++;
                                    isnumber = numbercharacter.IsMatch(codeline[j].ToString());
                                }
                                else
                                {
                                    break;
                                }
                                
                            }
                            CC.is_IDENTIFIER_or_KEYWORD(word + "." + secondword, linenumber);
                            word = null;
                            if (j != codeline.Length - 1)
                            {
                                j++;
                                word = word + codeline[j];
                            }
                            
                            secondword = null;
                            continue;
                        }
                        else
                        {
                            CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                            string punctuatortoken = "( ., " + codeline[j] + ", " + linenumber.ToString() + ")";

                            File.AppendAllText("Token.txt", punctuatortoken + Environment.NewLine);
                            word = null;
                            continue;
                        }
                    }

                    if (codeline[j] == '(' || codeline[j] == ')' || codeline[j] == '[' || codeline[j] == ']' || codeline[j] == '{' || codeline[j] == '}' || codeline[j] == '\'' || codeline[j] == '\\' || codeline[j] == ',')
                    {

                        if (word!=null)
                        {
                            CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                        }
                            
                         string punctuatortoken = "( Punctuator, " + codeline[j] + ", " + linenumber.ToString()+")";

                            File.AppendAllText("Token.txt", punctuatortoken + Environment.NewLine);
                            word = null;
                            continue;



                    }
                    else if(codeline[j]=='=')
                    {
                        if (word!=null)
                        {
                            CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                        }
                        if (j != codeline.Length - 1)
                        {
                            if (codeline[j + 1] == '=')
                            {
                                string compoundassignment = "( Relational Operator, " + codeline[j] + codeline[j + 1] + ", " + linenumber.ToString() + " )"+Environment.NewLine;
                                File.AppendAllText("Token.txt", compoundassignment);
                                j++;
                                word = null;
                                continue;
                            }

                        }
                        
                        
                            string punctuatortoken = "( Assignment Operator, " + codeline[j] + ", " + linenumber.ToString() + ")";

                            File.AppendAllText("Token.txt", punctuatortoken + Environment.NewLine);
                            word = null;
                            continue;
                        
                        
                    }
                    else if (codeline[j] == '+' || codeline[j] == '-' || codeline[j] == '*' || codeline[j] == '/' || codeline[j] == '%' || codeline[j] == '^')
                    {
                        if (word != null)
                        {
                            CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                        }
                        if (j!=codeline.Length-1&&codeline[j]!='^')
                        {
                            if (codeline[j]==codeline[j+1])
                            {
                                string compoundassignment = "( Compount Assignment, " + codeline[j] + codeline[j + 1] + ", " + linenumber.ToString() + " )" + Environment.NewLine;
                                File.AppendAllText("Token.txt", compoundassignment);
                                j++;
                                word = null;
                                continue;
                            }
                            
                        }
                        if (codeline[j] == '+' || codeline[j] == '-')
                        {
                            string compoundassignment = "( PM, " + codeline[j] + ", " + linenumber.ToString() + " )" + Environment.NewLine;
                            File.AppendAllText("Token.txt", compoundassignment);

                            word = null;
                            continue;
                        }
                        else
                        {
                            string compoundassignment = "( MDME, " + codeline[j] + ", " + linenumber.ToString() + " )" + Environment.NewLine;
                            File.AppendAllText("Token.txt", compoundassignment);

                            word = null;
                            continue;
                        }
                        
                    }
                    else if (codeline[j] == '>' || codeline[j] == '<' || codeline[j] == '!')
                    {
                        if (word != null)
                        {
                            CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                        }
                        if (j != codeline.Length - 1 && codeline[j] != '!')
                        {
                            if (codeline[j+1] == '=')
                            {
                                string compoundassignment = "( Relational Operator, " + codeline[j] + codeline[j + 1] + ", " + linenumber.ToString() + " )" + Environment.NewLine;
                                File.AppendAllText("Token.txt", compoundassignment);
                                j++;
                                word = null;
                                continue;
                            }

                        }
                        if (codeline[j] == '!')
                        {
                            if (j != codeline.Length - 1)
                            {
                                if (codeline[j + 1] == '=')
                                {
                                    string compoundassignment = "( Relational Operator, " + codeline[j] + codeline[j + 1] + ", " + linenumber.ToString() + " )" + Environment.NewLine;
                                    File.AppendAllText("Token.txt", compoundassignment);
                                    j++;
                                    word = null;
                                    continue;
                                }
                                else
                                {
                                    string compoundassignment = "( Invalid Token, " + codeline[j] + ", " + linenumber.ToString() + " )" + Environment.NewLine;
                                    File.AppendAllText("Token.txt", compoundassignment);

                                    word = null;
                                    continue;
                                }
                            }


                        }
                        else
                        {
                            string compoundassignment = "( Relational Operator, " + codeline[j] + ", " + linenumber.ToString() + " )" + Environment.NewLine;
                            File.AppendAllText("Token.txt", compoundassignment);

                            word = null;
                            continue;
                        }
                        
                    }
                    else if (codeline[j]!=' '&&codeline[j]!=';')
                    {
                        word = word + codeline[j];
                        continue;
                    }
                    
                    else
                    {
                        CC.is_IDENTIFIER_or_KEYWORD(word, linenumber);
                        
                        word = null;
                    }
                    
                    
                }
                if (word!=null)
                {
                    CC.is_IDENTIFIER_or_KEYWORD(word,linenumber);

                }

            }
            MessageBox.Show("Token Creation Successfully");


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText("Code.txt", textBox1.Text);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mytext=File.ReadAllText("Token.txt");
            textBox1.Text = "";
            textBox1.Text = mytext;

        }

        public int linenumber { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
