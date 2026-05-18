using OOP_laba7.Controllers;
using OOP_laba7.utils;

namespace OOP_laba7;

/// <summary>
/// MVC: View
/// Отображает данные и реагирует на пользовательский ввод.
/// НЕ содержит бизнес-логики. НЕ обращается к Model напрямую.
/// Всё делегирует Controller.
/// </summary>
public partial class MainForm : Form
{
    // View знает только о Controller, не о Model
    private readonly AirportController _controller;

    public MainForm()
    {
        InitializeComponent();

        // View создаёт Controller
        _controller = new AirportController();

        // View подписывается на события Controller
        _controller.DataChanged  += RefreshGrid;     // обновить таблицу
        _controller.ActionLogged += AppendActionLog; // дописать лог
    }

    // ──────────────────────────────────────────────
    //  Обработчики кнопок — View → Controller
    // ──────────────────────────────────────────────

    private void button_CreateObj_Click(object sender, EventArgs e)
    {
        try   { _controller.AddRandomAirport(); }
        catch (Exception ex) { ShowError(ex.Message); }
    }

    private void button_CreatePremiumObj_Click(object sender, EventArgs e)
    {
        try   { _controller.AddRandomPremiumAirport(); }
        catch (Exception ex) { ShowError(ex.Message); }
    }

    private void button_DeleteObj_Click(object sender, EventArgs e)
    {
        try   { _controller.DeleteAirport((int)numericUpDown_Obj.Value); }
        catch (IndexOutOfRangeException) { ShowError("Нет элемента с таким индексом."); }
        catch (Exception ex)            { ShowError(ex.Message); }
    }

    private void button_Back_Click(object sender, EventArgs e) => this.Close();
    private void button_Exit_Click(object sender, EventArgs e) => Application.Exit();

    // ──────────────────────────────────────────────
    //  Методы отображения — Controller → View
    // ──────────────────────────────────────────────

    /// <summary>
    /// View получает от Controller готовые ViewModel-ы и просто рисует их.
    /// Никакой логики — только отображение.
    /// </summary>
    private void RefreshGrid()
    {
        dataGridView1.Rows.Clear();

        foreach (var vm in _controller.GetAirports())
        {
            dataGridView1.Rows.Add(
                vm.Index,
                vm.Name,
                vm.Location,
                vm.FlightsPerDay,
                vm.TicketsSold,
                vm.Balance,
                vm.Rating,
                vm.EmployeesCount
            );
        }
    }

    private void AppendActionLog(string message)
    {
        textBox_Actions.Text += message;
    }

    private static void ShowError(string message)
    {
        NativeMessageBox.MessageBox(
            0, message, "Ошибка",
            NativeMessageBox.MB_OK | NativeMessageBox.MB_ICONERROR
        );
    }
}