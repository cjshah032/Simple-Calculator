using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            Display.Text = newDisplay;
        }

        string Calculate(string input)
        {
            string output = default;
            if (IsInputCorrect(input))
            {
                char[] separators = { '+', '-', '/', '*' };
                string[] Nums = input.Split(separators);
                int index = 0;
                float Result = 0;
                foreach (var s in Nums)
                {
                    float num = 0;
                    float.TryParse(s, out num);
                    index = input.IndexOf(s, index);
                    if (index == 0)
                        Result += num;
                    else
                    {
                        switch (input[index - 1])
                        {
                            case '+':
                                Result += num;
                                break;
                            case '-':
                                Result -= num;
                                break;
                            case '*':
                                Result *= num;
                                break;
                            case '/':
                                Result /= num;
                                break;
                        }
                    }
                    index += s.Length;
                }
                output = Result.ToString();
            }

            else output = "Likhte bhi nahi aata!";
            return output;
        }

        bool IsInputCorrect(string input)
        {
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

            char[] separators = { '+', '-', '/', '*' };
            string[] Nums = input.Split(separators);
            for (var i=1; i<Nums.Length; i++)
            {   float Number = default;
                float.TryParse(Nums[i], out Number);
                if (Number == (float)0 && input[input.IndexOf(Nums[i]) - 1] == '/')
                    return false;
            }
            return true;
        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            string newDisplay = Display.Text.Substring(0, Display.Text.Length - 1);
            Display.Text = newDisplay;
        }

        private void ClickClear(object sender, RoutedEventArgs e)
        {
            Display.Text = "";
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
