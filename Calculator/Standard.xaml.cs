using System;

namespace Calculator;

public partial class Standard : ContentPage
{
    double memory = 0;
    double number1 = 0;
    string operatorSymbol = "";
    bool isNewEntry = false;

    public Standard()
    {
        InitializeComponent();
    }

    private void NumClicked (object sender, EventArgs e)
    {
        var btn = (Button)sender;
        if (Screen.Text == "0" || isNewEntry)
        {
            Screen.Text = btn.Text;
            isNewEntry = false;
        }
        else
        {
            Screen.Text += btn.Text;
        }
    }

    private void OperatorClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        History.Text = Screen.Text + " " + btn.Text;
        number1 = double.Parse(Screen.Text);
        Screen.Text = "0";
        operatorSymbol = btn.Text;
    }

    private void EqualClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(operatorSymbol))
            return; // operatör yoksa hiçbir işlem yapma

        double number2 = double.Parse(Screen.Text);
        double result = 0;

        switch (operatorSymbol)
        {
            case "+":
                result = (number1 + number2); 
                break;
            case "-":
                result = (number1 - number2);
                break;
            case "×":
                result = (number1 * number2);
                break;
            case "÷":
                if (number2 == 0)
                {
                    DisplayAlert("Hata", "Sıfıra bölünemez!", "Tamam");
                    return;
                }
                result = number1 / number2;
                break;
        }

        History.Text = $"{number1} {operatorSymbol} {number2} =";
        Screen.Text = result.ToString();
        number1 = result;
        isNewEntry = true;
        operatorSymbol = "";
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        Screen.Text = "0";
        History.Text= "";
        number1 = 0;
        operatorSymbol = "";
    }

    private void ClearEntry_Clicked(object sender, EventArgs e)
    {
        Screen.Text = "0";
    }
    private void BackSpaceClicked(object sender, EventArgs e)
    {
        Screen.Text = Screen.Text.Length > 1 ? Screen.Text[..^1] : "0";
    }

    private async void UnaryOperator_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        number1 = double.Parse(Screen.Text);
        operatorSymbol = btn.Text;
        var result = 0.0;

        switch (operatorSymbol)
        {
            case "x²":
                result = Math.Pow(number1, 2);
                History.Text = $"sqr({number1})";
                break;
            case "1/x":
                if (number1 != 0)
                    result = 1 / number1;
                else
                {
                    await DisplayAlert("Hata", "Sıfıra bölünemez!", "Tamam");
                    return;
                }
                History.Text = $"1/({number1})";
                break;
            case "%":
                result = number1 / 100;
                History.Text = $"sqr({number1})";
                break;
            case "²√x":
                result = Math.Sqrt(number1);
                History.Text = $"√({number1})";
                break;
        }
        Screen.Text = result.ToString();
    }

    private void PlusMinus_Clicked(object sender, EventArgs e)
    {
        if (Screen.Text != "0")
        {
            Screen.Text = Screen.Text.StartsWith("-") ? Screen.Text.Substring(1) : "-" + Screen.Text;
        }
    }

    private void Decimal_Clicked(object sender, EventArgs e)
    {
        if (Screen.Text.Contains("."))
        {
            return;
        }
        Screen.Text += ".";
    }

    private async void Memory_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        operatorSymbol = btn.Text;
        double current = double.Parse(Screen.Text);

        switch (operatorSymbol)
        {
            case "MC":
                History.Text = "0";
                break;
            case "MR":
                Screen.Text = memory.ToString();
                break;
            case "M+":
                memory += current;
                break;
            case "M-":
                memory -= current;
                break;
            case "MS":
                memory = current;
                break;
            case "Mv":
                await DisplayAlert("Bellek", $"Bellek değeri: {memory}", "Tamam");
                break;
        }
    }

    
}