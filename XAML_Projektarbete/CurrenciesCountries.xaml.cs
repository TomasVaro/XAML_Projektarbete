﻿using System;
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
using XAML_Projektarbete.DataProvider;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XAML_Projektarbete
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrenciesCountries : Page
    {
        public CurrenciesCountries()
        {
            this.InitializeComponent();
            getAllCurrencies();
        }

        Dictionary<string, Models.Countries> allCountries;
        private async void getAllCurrencies()
        {
            CountryDataProvider cdp = new CountryDataProvider();
            allCountries = await cdp.getAllCountriesUrl();
            ComboBoxItem currenciesNoDuplicates;
            string lastCurrency = string.Empty;

            foreach (var cur in allCountries.OrderBy(f => f.Value.CurrencyName))
            {
                if (cur.Value.CurrencyName.ToLower() != lastCurrency.ToLower() && cur.Value.CurrencyName != "United States dollar")
                {
                    currenciesNoDuplicates = new ComboBoxItem { Content = cur.Value.CurrencyName };
                    CurrencyName.Items.Add(currenciesNoDuplicates);
                    lastCurrency = cur.Value.CurrencyName;
                }
            }
            CurrencyName.SelectedIndex = 44;
            getCountries();
        }

        private void ShowCurrencies_OnDropDownClosed(object sender, object e)
        {
            getCountries();
        }

        private void getCountries()
        {
            ComboBoxItem selected = CurrencyName.SelectedItem as ComboBoxItem;
            string selectedCurrency = selected.Content as String;
            CountryName.Text = string.Empty;
            CountryCode.Text = string.Empty;
            CountryId.Text = string.Empty;

            foreach (var cur in allCountries.OrderBy(f => f.Value.Name))
            {
                if (cur.Value.CurrencyName == "United States dollar")
                {
                    cur.Value.CurrencyName = "U.S. Dollar";
                }
                if (cur.Value.CurrencyName.ToLower() == selectedCurrency.ToLower())
                {                    
                    CurrencyId.Text = cur.Value.CurrencyId;
                    CurrencySymbol.Text = cur.Value.CurrencySymbol;
                    CountryName.Text = CountryName.Text + "\n" + cur.Value.Name;
                    CountryCode.Text = CountryCode.Text + "\n" + cur.Value.Alpha3;
                    CountryId.Text = CountryId.Text + "\n" + cur.Value.Id;                    
                }
            }
        }
    }
}