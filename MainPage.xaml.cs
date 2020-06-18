using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private void ClickEqual(object sender, RoutedEventArgs e)
        {
            string newDisplay = Calculate(Display.Text);
            if (!float.TryParse(newDisplay, out float trash))
                Display.Foreground = new SolidColorBrush(Colors.Red);
            Display.Text = newDisplay;
        }

        string Calculate(string input)
        {
            string output = default;
            if (IsInputCorrect(input))
            {
                //Step 1: Parse to postfix from infix:
                Stack<char> operands = new Stack<char>();
                string PostFix = "";
                for(int i=0; i<input.Length; i++)
                {
                    if (input[i] == '+' || input[i] == '-' || input[i] == '*' || input[i] == '/')
                    {
                        PostFix += ',';
                        if (operands.Count == 0 || Precedence(input[i]) > Precedence(operands.Peek()))
                            operands.Push(input[i]);
                        else
                        {
                            while (operands.Count != 0 && Precedence(operands.Peek()) > Precedence(input[i]))
                                PostFix += operands.Pop();
                            operands.Push(input[i]);
                        }
                    }
                    else PostFix += input[i];
                }

                while (operands.Count != 0)
                    PostFix += operands.Pop();

                //Step 2: Parse from postfix to output
                Stack<float> Output = new Stack<float>();
                StringBuilder num = new StringBuilder();
                for (int i=0; i<PostFix.Length; i++)
                {
                    if(PostFix[i] == ',')
                    {
                        if (float.TryParse(num.ToString(), out float result))
                            Output.Push(result);
                        else return "Input Error!";
                        num.Clear();
                    }

                    else if (PostFix[i] == '+' || PostFix[i] == '-' || PostFix[i] == '*' || PostFix[i] == '/')
                    {
                        if(num.Length != 0)
                        {
                            if (float.TryParse(num.ToString(), out float result))
                                Output.Push(result);
                            else return "Input Error!";
                            num.Clear();
                      
                                float op = Output.Pop();
                                switch (PostFix[i])
                                {
                                    case '+':
                                        op += Output.Pop();
                                        break;

                                    case '-':
                                        op = Output.Pop() - op;
                                        break;

                                    case '*':
                                        op *= Output.Pop();
                                        break;

                                    case '/':
                                        op = Output.Pop() / op;
                                        break;

                                }
                                Output.Push(op);
                    
                        }

                        else
                        {
                            float op = Output.Pop();  
                            switch (PostFix[i])
                            {
                                case '+':
                                    op += Output.Pop();
                                    break;

                                case '-':
                                    op = Output.Pop() - op;
                                    break;
                               
                                case '*':
                                    op *= Output.Pop();
                                    break;
                                
                                case '/':
                                    op = Output.Pop() / op;
                                    break;

                            }
                            Output.Push(op);
                        }
                    }

                    else
                    {
                        num.Append(PostFix[i]);       
                    }
                }

                output = Output.Peek().ToString();
            }

            else output = "Innput Error";
            return output;
        }

        int Precedence (char op)
        {
            if (op == '/' || op == '*')
                return 2;
            else return 1;
        }

        bool IsInputCorrect(string input)
        {
            //to check whether the expression is incomplete
            int indexPlus = input.LastIndexOf('+');
            if (indexPlus == input.Length - 1)
                return false;
            int indexMinus = input.LastIndexOf('-');
            if (indexMinus == input.Length - 1)
                return false;
            int indexMultiply = input.LastIndexOf('*');
            if (indexMultiply == input.Length - 1)
                return false;
            int indexDivide = input.LastIndexOf('/');
            if (indexDivide == input.Length - 1)
                return false;

            //to check if there's any division by zero
            //also checks if two operators don't have an operand in between or the numeric input is inappropriate
            //also checks if the first char of input is not a number
            char[] separators = { '+', '-', '/', '*' };
            string[] Nums = input.Split(separators);
            if (input.IndexOf(Nums[0]) != 0)
                return false;
            if (Nums.Length == 1)
                return false;
            for (var i=1; i<Nums.Length; i++)
            {   float Number = default;
                if (float.TryParse(Nums[i], out Number))
                {
                    if (Number == (float)0 && input[input.IndexOf(Nums[i]) - 1] == '/')
                        return false;
                }
                else return false;
            }
            return true;
        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length != 0)
            {
                string newDisplay = Display.Text.Substring(0, Display.Text.Length - 1);
                Display.Text = newDisplay;
                Display.Foreground = new SolidColorBrush(Colors.Black);
            }

            else Display.Foreground = new SolidColorBrush(Colors.Black); ;
        }

        private void ClickClear(object sender, RoutedEventArgs e)
        {
            Display.Text = "";
            Display.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Click9(object sender, RoutedEventArgs e)
        {
            Display.Text += '9';
        }

        private void Click8(object sender, RoutedEventArgs e)
        {
            Display.Text += '8';
        }

        private void Click7(object sender, RoutedEventArgs e)
        {
            Display.Text += '7';
        }

        private void ClickDivide(object sender, RoutedEventArgs e)
        {
            Display.Text += '/';
        }

        private void Click6(object sender, RoutedEventArgs e)
        {
            Display.Text += '6';
        }

        private void Click5(object sender, RoutedEventArgs e)
        {
            Display.Text += '5';
        }

        private void Click4(object sender, RoutedEventArgs e)
        {
            Display.Text += '4';
        }

        private void ClickMultiply(object sender, RoutedEventArgs e)
        {
            Display.Text += '*';
        }

        private void Click3(object sender, RoutedEventArgs e)
        {
            Display.Text += '3';
        }

        private void Click2(object sender, RoutedEventArgs e)
        {
            Display.Text += '2';
        }

        private void Click1(object sender, RoutedEventArgs e)
        {
            Display.Text += '1';
        }

        private void ClickPlus(object sender, RoutedEventArgs e)
        {
            Display.Text += '+';
        }

        private void Click00(object sender, RoutedEventArgs e)
        {
            Display.Text += "00";
        }

        private void Click0(object sender, RoutedEventArgs e)
        {
            Display.Text += '0';
        }

        private void ClickPoint(object sender, RoutedEventArgs e)
        {
            Display.Text += '.';
        }

        private void ClickMinus(object sender, RoutedEventArgs e)
        {
            Display.Text += '-';
        }
    }
}
