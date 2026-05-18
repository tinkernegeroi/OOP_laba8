namespace OOP_laba7;

public partial class WelcomeForm : Form
{
    public WelcomeForm()
    {
        InitializeComponent();
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
        this.Hide();
        using (MainForm form = new MainForm())
        {
            form.ShowDialog();
        }
        this.Show();
            
    }
}