namespace Calculator;

public partial class Scientific : ContentPage
{
    double memory = 0;
    double number1 = 0;
    string operatorSymbol = "";
    bool isNewEntry = false;
    bool isDeg = true;
    bool isSecond = false;
    bool isHyp = false;

    public Scientific()
	{
		InitializeComponent();
	}

    private void NumClicked(object sender, EventArgs e)
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
            case "mod":
                if (number2 == 0)
                {
                    DisplayAlert("Hata", "0'a göre mod alınamaz!", "Tamam");
                    return;
                }
                result = number1 % number2;
                break;
            case "^":
                result = Math.Pow(number1, number2);
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
        History.Text = "";
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
                History.Text = $"%({number1})";
                break;

            case "²√x":
                result = Math.Sqrt(number1);
                History.Text = $"√({number1})";
                break;

            case "|x|":
                result = Math.Abs(number1);
                History.Text = $"abs({number1})";
                break;

            case "n!":
                if (number1 >= 0)
                    result = Factorial(number1);
                else
                {
                    await DisplayAlert("Hata", "Negatif sayının faktöriyeli alınamaz!", "Tamam");
                    return;
                }
                History.Text = $"fact({number1})";
                break;

            case "10ˣ":
                result = Math.Pow(10, number1);
                History.Text = $"10^{number1}";
                break;

            case "log":
                if (number1 <= 0)
                {
                    await DisplayAlert("Hata", "Negatif veya sıfırın logaritması alınamaz!", "Tamam");
                    return;
                }
                result = Math.Log10(number1);
                History.Text = $"log({number1})";
                break;

            case "ln":
                if (number1 <= 0)
                {
                    await DisplayAlert("Hata", "Negatif veya sıfırın ln değeri alınamaz!", "Tamam");
                    return;
                }
                result = Math.Log(number1);
                History.Text = $"ln({number1})";
                break;
            case "exp":
                result = Math.Exp(number1);
                History.Text = $"exp({number1})";
                break;
        }
        Screen.Text = result.ToString();
        isNewEntry = true;
        operatorSymbol = "";
        number1 = 0;
    }

    private double Factorial(double n)
    {
        if (n < 0)
            return double.NaN;

        double result = 1;
        for (int i = 2; i <= (int)n; i++)
            result *= i;
        return result;
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

    private void SecondFunction_Clicked(object sender, EventArgs e)
    {

    }

    private void Constant_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;

        switch (btn.Text)
        {
            case "π":
                Screen.Text = Math.PI.ToString();
                History.Text = "π";
                break;
            case "e":
                Screen.Text = Math.E.ToString();
                History.Text = "e";
                break;
        }
        isNewEntry = true;
    }

    private void Parenthesis_Clicked(object sender, EventArgs e)
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

    private void DegRad_Clicked(object sender, EventArgs e)
    {
        isDeg = !isDeg;
        var btn = (Button)sender;
        btn.Text = isDeg ? "DEG" : "RAD";
    }
    private void TrigFunction_Clicked(object sender, EventArgs e)
    {
        TrigPanel.IsVisible = !TrigPanel.IsVisible;
    }

    private void UpdateTrigButtons()
    {
        foreach (var child in TrigPanel.Children)
        {
            if (child is Button b)
            {
                string baseName = b.AutomationId; // Her butona AutomationId olarak "sin", "cos" vs. ver
                switch (baseName)
                {
                    case "sin":
                        b.Text = isSecond && isHyp ? "sinh⁻¹" :
                                 isSecond ? "sin⁻¹" :
                                 isHyp ? "sinh" : "sin";
                        break;
                    case "cos":
                        b.Text = isSecond && isHyp ? "cosh⁻¹" :
                                 isSecond ? "cos⁻¹" :
                                 isHyp ? "cosh" : "cos";
                        break;
                    case "tan":
                        b.Text = isSecond && isHyp ? "tanh⁻¹" :
                                 isSecond ? "tan⁻¹" :
                                 isHyp ? "tanh" : "tan";
                        break;
                    case "sec":
                        b.Text = isSecond && isHyp ? "sech⁻¹" :
                                 isSecond ? "sec⁻¹" :
                                 isHyp ? "sech" : "sec";
                        break;
                    case "csc":
                        b.Text = isSecond && isHyp ? "csch⁻¹" :
                                 isSecond ? "csc⁻¹" :
                                 isHyp ? "csch" : "csc";
                        break;
                    case "cot":
                        b.Text = isSecond && isHyp ? "coth⁻¹" :
                                 isSecond ? "cot⁻¹" :
                                 isHyp ? "coth" : "cot";
                        break;
                }
            }
        }
    }

    private void TrigOption_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;

        if (btn.Text == "2ⁿᵈ")
        {
            isSecond = !isSecond;
            btn.BackgroundColor = isSecond ? Colors.LightGray : Colors.Transparent;
        }
        else if (btn.Text == "hyp")
        {
            isHyp = !isHyp;
            btn.BackgroundColor = isHyp ? Colors.LightGray : Colors.Transparent;
        }

        UpdateTrigButtons();

    }

    private async void Trig_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        double value = double.Parse(Screen.Text);

        double angle = isDeg ? value * Math.PI / 180 : value; // Derece/Radyan kontrolü ile açı değeri atama
        double result = 0;

        const double epsilon = 1e-10; // Yaklaşık sıfır kontrolü için sabit değer

        switch (btn.Text)
        {

            case "sin":
                result = Math.Sin(angle);
                break;
            case "cos":
                result = Math.Cos(angle);
                break;
            case "tan":
                if (Math.Abs(Math.Cos(angle)) < epsilon)
                {
                    await DisplayAlert("Hata", "tan(x) tanımsız!", "Tamam");
                    return;
                }
                result = Math.Tan(angle);
                break;
            case "sec":
                if (Math.Abs(Math.Cos(angle)) < epsilon)
                {
                    await DisplayAlert("Hata", "sec(x) tanımsız!", "Tamam");
                    return;
                }
                result = 1 / Math.Cos(angle);
                break;
            case "csc":
                if (Math.Abs(Math.Sin(angle)) < epsilon)
                {
                    await DisplayAlert("Hata", "csc(x) tanımsız!", "Tamam");
                    return;
                }
                result = 1 / Math.Sin(angle);
                break;
            case "cot":
                if (Math.Abs(Math.Tan(angle)) < epsilon)
                {
                    await DisplayAlert("Hata", "cot(x) tanımsız!", "Tamam");
                    return;
                }
                result = 1 / Math.Tan(angle);
                break;

            // 2^nd
            case "sin⁻¹":
                if (value < -1 || value > 1)
                {
                    await DisplayAlert("Hata", "sin⁻¹ için değer -1 ile 1 arasında olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Asin(value);
                if (isDeg) result = result * 180 / Math.PI;
                break;
            case "cos⁻¹":
                if (value < -1 || value > 1)
                {
                    await DisplayAlert("Hata", "cos⁻¹ için değer -1 ile 1 arasında olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Acos(value);
                if (isDeg) result = result * 180 / Math.PI;
                break;
            case "tan⁻¹":
                result = Math.Atan(value);
                if (isDeg) result = result * 180 / Math.PI;
                break;
            case "sec⁻¹":
                if (Math.Abs(value) < 1)
                {
                    await DisplayAlert("Hata", "sec⁻¹ için |x| ≥ 1 olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Acos(1 / value);
                if (isDeg) result = result * 180 / Math.PI;
                break;
            case "csc⁻¹":
                if (Math.Abs(value) < 1)
                {
                    await DisplayAlert("Hata", "csc⁻¹ için |x| ≥ 1 olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Asin(1 / value);
                if (isDeg) result = result * 180 / Math.PI;
                break;
            case "cot⁻¹":
                if (Math.Abs(value) < epsilon)
                {
                    await DisplayAlert("Hata", "cot⁻¹ için değer sıfır olamaz!", "Tamam");
                    return;
                }
                result = Math.Atan(1 / value);
                if (isDeg) result = result * 180 / Math.PI;
                break;

            // hyp
            case "sinh":
                result = Math.Sinh(value);
                break;
            case "cosh":
                result = Math.Cosh(value);
                break;
            case "tanh":
                if (Math.Abs(Math.Tanh(value)) >= 1.0 - epsilon)
                {
                    await DisplayAlert("Hata", "tanh(x) ±1 değerine çok yakın, tanımsız olabilir!", "Tamam");
                    return;
                }
                result = Math.Tanh(value);
                break;
            case "sech":
                result = 1 / Math.Cosh(value);
                break;
            case "csch":
                if (Math.Abs(value) < epsilon)
                {
                    await DisplayAlert("Hata", "csch(0) tanımsız!", "Tamam");
                    return;
                }
                result = 1 / Math.Sinh(value);
                break;
            case "coth":
                if (Math.Abs(value) < epsilon)
                {
                    await DisplayAlert("Hata", "coth(0) tanımsız!", "Tamam");
                    return;
                }
                result = 1 / Math.Tanh(value);
                break;

            // 2^nd + hyp
            case "sinh⁻¹":
                result = Math.Asinh(value);
                break;
            case "cosh⁻¹":
                if (value < 1)
                {
                    await DisplayAlert("Hata", "cosh⁻¹ için değer ≥ 1 olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Acosh(value);
                break;
            case "tanh⁻¹":
                if (value <= -1 || value >= 1)
                {
                    await DisplayAlert("Hata", "tanh⁻¹ için değer -1 < x < 1 olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Atanh(value);
                break;

            case "sech⁻¹":
                if (value <= 0 || value > 1)
                {
                    await DisplayAlert("Hata", "sech⁻¹ için 0 < x ≤ 1 olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Acosh(1 / value);
                break;
            case "csch⁻¹":
                if (Math.Abs(value) < epsilon)
                {
                    await DisplayAlert("Hata", "csch⁻¹ için x ≠ 0 olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Asinh(1 / value);
                break;
            case "coth⁻¹":
                if (Math.Abs(value) <= 1)
                {
                    await DisplayAlert("Hata", "coth⁻¹ için |x| > 1 olmalıdır!", "Tamam");
                    return;
                }
                result = Math.Atanh(1 / value);
                break;
        }

        Screen.Text = result.ToString();
        History.Text = $"{btn.Text}({value})";
    }


}