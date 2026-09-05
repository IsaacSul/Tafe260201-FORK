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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Mortgage : Page
    {
        public Mortgage()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            double yearlyIntrestRate = double.Parse(annualInterestTextBox.Text);

            double principleBorrow = double.Parse(principalTextbox.Text);

            int Years = int.Parse(yearsTextBox.Text);

            int andMonths = int.Parse(monthsTextbox.Text);



            double monthlyIntrestRate = yearlyIntrestRate / 12.0;

            monthlyIntrestRate = monthlyIntrestRate * 0.01;



            int numberOfPayments = Years * 12 + andMonths;



            double numerator = principleBorrow * Math.Pow(1 + monthlyIntrestRate, numberOfPayments) * monthlyIntrestRate;

            double denominator = Math.Pow(1 + monthlyIntrestRate, numberOfPayments) - 1;

            double monthlyRepayment = numerator / denominator;



            monthlyInterestTextBox.Text = monthlyIntrestRate.ToString();
            monthlyRepaymentTextBox.Text = monthlyRepayment.ToString();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Menu));
        }
    }
}
