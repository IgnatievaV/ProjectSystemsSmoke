namespace Smoke.WinForms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Text = Smoke.Library.Greeter.Hello("WinForms");
    }
}
