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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrencyConverter : Page
    {
        static Dictionary<string, float> US_CONVERSION_RATES = new Dictionary<string, float>
        {
            { "EUR - Euro", 0.85189982f},
            { "UK - Pound", 0.72872436f},
            { "INR - Rupee", 74.257327f}
        };
        static Dictionary<string, float> EUR_CONVERSION_RATES = new Dictionary<string, float>
        {
            { "USD - US Dollar", 1.1739732f},
            { "UK - Pound", 0.8556672f},
            { "INR - Rupee", 87.00755f}
        };
        static Dictionary<string, float> UK_CONVERSION_RATES = new Dictionary<string, float>
        {
            { "USD - US Dollar", 1.371907f},
            { "EUR - Euro", 1.1686692f},
            { "INR - Rupee", 101.68635f}
        };
        static Dictionary<string, float> INR_CONVERSION_RATES = new Dictionary<string, float>
        {
            { "USD - US Dollar", 0.011492628f},
            { "EUR - Euro", 0.013492774f},
            { "UK - Pound", 0.0098339397f}
        };

        static Dictionary<string, Dictionary<string, float>> CONVERSION_TABLES = new Dictionary<string, Dictionary<string, float>>
        {
            { "USD - US Dollar", US_CONVERSION_RATES},
            { "EUR - Euro", EUR_CONVERSION_RATES},
            { "UK - Pound", UK_CONVERSION_RATES},
            { "INR - Rupee", INR_CONVERSION_RATES}
        };

        public CurrencyConverter()
        {
            this.InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Menu));
        }

        private async void convertButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.amountTextInput.Text))
            {
                DisplayDialog("Validation Error", "Please input an amount");
                return;
            }

            float amount;
            try
            {
                amount = float.Parse(this.amountTextInput.Text);
            } catch
            {
                DisplayDialog("Validation Error", "Amount must be a number, please re-enter.");
                return;
            }

            String from = (this.fromCurrency.SelectedItem as ComboBoxItem)?.Content?.ToString();
            String to = (this.toCurrency.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (from == null)
            {
                DisplayDialog("Validation Error", "Please select a from currency");
                return;
            }
            else if (to == null)
            {
                DisplayDialog("Validation Error", "Please select a to currency");
                return;
            }
            else if (from.Equals(to))
            {
                DisplayDialog("Validation Error", "Currencys can not be the same");
                return;
            }

            // fetch correct conversion table and get rates
            Dictionary<string, float> convertTable = CONVERSION_TABLES[from];
            Dictionary<string, float> negitiveConvertTable = CONVERSION_TABLES[to];
            float rate = convertTable[to];
            float negitiveRate = negitiveConvertTable[from];

            this.amountTextOuput.Text = this.amountTextInput.Text + " " + from + " =";
            this.equals.Text = (amount * rate).ToString();
            this.oneFrom.Text = "1 " + from + " = " + rate + " " + to;
            this.oneTo.Text = "1 " + to + " = " + negitiveRate + " " + from;
        }

        private async void DisplayDialog(String title, String content)
        {
            ContentDialog dialog = new ContentDialog
            {
                XamlRoot = this.Content.XamlRoot,
                Title = title,
                Content = content,
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }
    }
}
