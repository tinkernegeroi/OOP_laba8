using OOP_laba7.presenters;
using OOP_laba7.utils;

namespace OOP_laba7;

/// <summary>
/// MVP: View (реализация IAirportView).
///
/// Главное отличие от MVC Form2:
/// — Form2 реализует интерфейс IAirportView.
/// — Кнопки только испускают события — никакой логики внутри обработчиков.
/// — Form2 не знает ни о Model, ни об AirportPresenter — только о контракте IAirportView.
/// — ShowAirports, AppendLog, ShowError вызывает Presenter, не Form2 сама.
/// </summary>
public partial class MainForm : Form, IAirportView
{
    // ── IAirportView: события — View только сигнализирует ─────────────────────
    public event EventHandler? OnAddRandom;
    public event EventHandler? OnAddPremium;
    public event EventHandler? OnDelete;
 
    // ── IAirportView: свойство — Presenter читает сам ─────────────────────────
    public int SelectedIndex => (int)numericUpDown_Obj.Value;
 
    public MainForm()
    {
        InitializeComponent();
 
        // View создаёт Presenter и передаёт себя через интерфейс.
        // Presenter сам подпишется на события и возьмёт управление.
        _ = new AirportPresenter(this);
    }
 
    // ── Обработчики кнопок: только raise event, никакой логики ───────────────
 
    private void button_CreateObj_Click(object sender, EventArgs e)
        => OnAddRandom?.Invoke(this, EventArgs.Empty);
 
    private void button_CreatePremiumObj_Click(object sender, EventArgs e)
        => OnAddPremium?.Invoke(this, EventArgs.Empty);
 
    private void button_DeleteObj_Click(object sender, EventArgs e)
        => OnDelete?.Invoke(this, EventArgs.Empty);
 
    private void button_Back_Click(object sender, EventArgs e) => this.Close();
    private void button_Exit_Click(object sender, EventArgs e) => Application.Exit();
 
    // ── IAirportView: методы — Presenter вызывает, View только рисует ─────────
 
    public void ShowAirports(IReadOnlyList<AirportRowViewModel> rows)
    {
        dataGridView1.Rows.Clear();
        foreach (var r in rows)
        {
            dataGridView1.Rows.Add(
                r.Index,
                r.Name,
                r.Location,
                r.FlightsPerDay,
                r.TicketsSold,
                r.Balance,
                r.Rating,
                r.EmployeesCount
            );
        }
    }
 
    public void AppendLog(string message)
        => textBox_Actions.Text += message;
 
    public void ShowError(string message)
        => NativeMessageBox.MessageBox(
            0, message, "Ошибка",
            NativeMessageBox.MB_OK | NativeMessageBox.MB_ICONERROR
        );
}