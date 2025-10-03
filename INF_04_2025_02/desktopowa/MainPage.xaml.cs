using System;

namespace MauiApp1;
public partial class MainPage : ContentPage
{
    private readonly string[][] jednostki = new string[][]
    {
        new string[] { "m", "km", "mi" },    
        new string[] { "mg", "kg", "lb" },   
        new string[] { "C", "F", "K" }      
    };
    public MainPage()
    {
        InitializeComponent();
        TypKonwersjiPicker.SelectedIndex = 0;
        OnTypChanged(this, null);
    }
    private void OnTypChanged(object sender, EventArgs e)
    {
        int idx = TypKonwersjiPicker.SelectedIndex;
        if (idx < 0) return;

        InputUnitPicker.ItemsSource = jednostki[idx];
        OutputUnitPicker.ItemsSource = jednostki[idx];

        InputUnitPicker.SelectedIndex = 0;
        OutputUnitPicker.SelectedIndex = 1;
    }
    private void OnKonwertuj(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(InputEntry.Text))
            {
                DisplayAlert("Błąd", "Wprowadź poprawną wartość do konwersji (liczba większa od 0)!", "OK");
                return;
            }

            if (!double.TryParse(InputEntry.Text, out double wartosc) || wartosc < 0)
            {
                DisplayAlert("Błąd", "Niepoprawna wartość.(liczba większa od 0)", "OK");
                return;
            }

            string jednostkaZ = InputUnitPicker.SelectedItem?.ToString();
            string jednostkaNa = OutputUnitPicker.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(jednostkaZ) || string.IsNullOrEmpty(jednostkaNa))
            {
                DisplayAlert("Błąd", "Wybierz jednostki konwersji!", "OK");
                return;
            }
            int typ = TypKonwersjiPicker.SelectedIndex + 1;

            double wynik = Konwertuj(typ, jednostkaZ, jednostkaNa, wartosc);

            if (wynik == -1.0)
                DisplayAlert("Błąd", "Niepoprawne jednostki.", "OK");
            else
                OutputEntry.Text = wynik.ToString("F3");
        }
        catch (Exception ex)
        {
            DisplayAlert("Błąd", ex.Message, "OK");
        }
    }
    private double Konwertuj(int typ, string jednostkaZ, string jednostkaNa, double wartosc)
    {
        if (typ == 1)
        {
            double wMetrach;
            if (jednostkaZ == "m") wMetrach = wartosc;
            else if (jednostkaZ == "km") wMetrach = wartosc * 1000;
            else if (jednostkaZ == "mi") wMetrach = wartosc * 1609.34;
            else return -1;

            if (jednostkaNa == "m") return wMetrach;
            else if (jednostkaNa == "km") return wMetrach / 1000;
            else if (jednostkaNa == "mi") return wMetrach / 1609.34;
            else return -1;
        }
        if (typ == 2)
        {
            double wKg;
            if (jednostkaZ == "mg") wKg = wartosc / 1_000_000;
            else if (jednostkaZ == "kg") wKg = wartosc;
            else if (jednostkaZ == "lb") wKg = wartosc * 0.453592;
            else return -1;

            if (jednostkaNa == "mg") return wKg * 1_000_000;
            else if (jednostkaNa == "kg") return wKg;
            else if (jednostkaNa == "lb") return wKg / 0.453592;
            else return -1;
        }
        if (typ == 3)
        {
            double wC;
            if (jednostkaZ == "C") wC = wartosc;
            else if (jednostkaZ == "F") wC = (wartosc - 32) * 5 / 9;
            else if (jednostkaZ == "K") wC = wartosc - 273.15;
            else return -1;

            if (jednostkaNa == "C") return wC;
            else if (jednostkaNa == "F") return wC * 9 / 5 + 32;
            else if (jednostkaNa == "K") return wC + 273.15;
            else return -1;
        }
        return -1;
    }
}
