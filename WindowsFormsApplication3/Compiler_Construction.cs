using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowsFormsApplication3
{
    class Compiler_Construction
    {
        public string[] Word_Separator(String code)
        {
            string[] linesofcode = code.Split(';');
            return linesofcode;
        }

        public void is_IDENTIFIER_or_KEYWORD(string word, int linenumber)
        {
            if (word != null)
            {
                Regex Alphabet = new Regex("^[a-zA-z]+$");
                bool AlphabetAnswer = Alphabet.IsMatch(word);
                
                Regex Integer = new Regex("^[0-9]+$");
                bool isINTEGER = Integer.IsMatch(word);
                Regex Float = new Regex("^[0-9]+[.][0-9]+(e)?[0-9]+$");
                bool isFLOAT = Float.IsMatch(word);
                Regex STRING = new Regex("^\"[a-z]*[A-Z]*[0-9]*\"$");
                bool isSTRING = STRING.IsMatch(word);
                Regex Identifier = new Regex("^(([A-Za-z]||[_])+([A-Za-z]||[0-9]||[_])*([A-Za-z]||[0-9])||[A-Za-z])$");
                bool isIDENTIFIER = Identifier.IsMatch(word);
                if (AlphabetAnswer == true)
                {


                    string[] classvalue = KeyWords(word);
                    if (classvalue != null)
                    {
                        string token = "( " + classvalue[0] + " , " + classvalue[1] + " , " + linenumber.ToString() + " )";
                        File.AppendAllText("Token.txt", token + Environment.NewLine);


                    }
                    else
                    {
                        string token = "( " + "Identifier" + " , " + word + " , " + linenumber.ToString() + " )";
                        File.AppendAllText("Token.txt", token + Environment.NewLine);
                    }


                }
                else
                {
                    
                    if (isSTRING==true)
                    {
                         string token = "( " + "String Constatnt" + " , " + word + " , " + linenumber.ToString() + " )";
                        File.AppendAllText("Token.txt", token + Environment.NewLine);
                    }
                    else if (isINTEGER==true)
                    {
                         string token = "( " + "Integer Constant" + " , " + word + " , " + linenumber.ToString() + " )";
                        File.AppendAllText("Token.txt", token + Environment.NewLine);
                    }
                    else if (isFLOAT==true)
                    {
                         string token = "( " + "Float Constant" + " , " + word + " , " + linenumber.ToString() + " )";
                        File.AppendAllText("Token.txt", token + Environment.NewLine);
                    }
                    else if (isIDENTIFIER == true)
                    {
                        string token = "( " + "Identifier" + " , " + word + " , " + linenumber.ToString() + " )";
                        File.AppendAllText("Token.txt", token + Environment.NewLine);


                    }
                    else
                    {
                        string token = "( " + "Invalid Token" + " , " + word + " , " + linenumber.ToString() + " )";
                        File.AppendAllText("Token.txt", token + Environment.NewLine);
                    }


                }


            }
        }


        public string[] KeyWords(string word)
        {
            
            string [][] classes=new string[26][];

            classes[0] = new string[3] { "I/O", "input", "output" };
            classes[1] = new string[2]{"format","format"};
            classes[2] = new string[2] { "break", "break" };
            classes[3] = new string[2] { "class", "class" };
            classes[4] = new string[2] { "continue", "continue" };
            classes[5] = new string[2] { "function", "func" };
            classes[6] = new string[3] { "True/False", "true", "false" };
            classes[7] = new string[2] { "while", "while" };
            classes[8] = new string[2] { "else", "else" };
            classes[9] = new string[2] { "elif", "elif" };
            classes[10] = new string[2] { "for", "for" };
            classes[11] = new string[2] { "from", "from" };
            classes[12] = new string[3] { "empty", "empty", "null" };
            classes[13] = new string[2] { "take", "take" };
            classes[14] = new string[2] { "have", "have" };
            classes[15] = new string[2] { "cmp_equal", "cmp_equal" };
            classes[16] = new string[3] { "out", "out", "return" };
            classes[17] = new string[2] { "to", "to" };
            classes[18] = new string[2] { "with", "with" };
            classes[19] = new string[2] { "new", "new" };
            classes[20] = new string[7] { "datatypes", "int", "double", "float", "string", "object", "char" };
            classes[21] = new string[2] { "static", "static" };
            classes[22] = new string[2] { "if", "if" };
            classes[23] = new string[2] { "dowhile", "dowhile" };
            classes[24] = new string[2] { "array", "array" };
            classes[25] = new string[2] { ".", "." };
            
            for (int i = 0; i < classes.Length; i++)
            {
                for (int j = 0; j < classes[i].Length; j++)
                {
                    if (classes[i][j]==word)
                    {
                        string classpart = classes[i][0];
                        string valuepart = word;
                        string []classvalue=new string[2]{classpart,valuepart};
                        return classvalue;
                    }
                }
            }
            return null;
            
        }



        
    }

    
}
