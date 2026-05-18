namespace OOP_laba7.presenters;

using OOP_laba7.classes;
using OOP_laba7.services;



/// <summary>
/// MVP: Presenter.
///
/// Отличие от MVC Controller:
/// — Presenter сам подписывается на события View (а не View подписывается на Controller).
/// — Presenter сам вызывает методы View (ShowAirports, AppendLog, ShowError).
/// — View абсолютно пассивен: он только испускает события и выполняет команды.
/// — Presenter тестируется без формы — достаточно передать mock IAirportView.
/// </summary>
public class AirportPresenter
{
    private readonly IAirportView     _view;   // Presenter знает View только через интерфейс
    private readonly AirportCollection _model;

    public AirportPresenter(IAirportView view)
    {
        _view  = view;
        _model = new AirportCollection();

        // Presenter подписывается на события Model → сам решает что сказать View
        _model.ItemAdded   += msg => _view.AppendLog(msg);
        _model.ItemRemoved += msg => _view.AppendLog(msg);

        // Presenter подписывается на события View → сам обрабатывает
        _view.OnAddRandom  += HandleAddRandom;
        _view.OnAddPremium += HandleAddPremium;
        _view.OnDelete     += HandleDelete;
    }

    // ── Обработчики событий View ──────────────────────────────────────────────

    private void HandleAddRandom(object? sender, EventArgs e)
    {
        try
        {
            _model.AddRandomItem();
            RefreshView();
        }
        catch (Exception ex) { _view.ShowError(ex.Message); }
    }

    private void HandleAddPremium(object? sender, EventArgs e)
    {
        try
        {
            _model.AddRandomPremiumItem();
            RefreshView();
        }
        catch (Exception ex) { _view.ShowError(ex.Message); }
    }

    private void HandleDelete(object? sender, EventArgs e)
    {
        try
        {
            _model.Remove(_view.SelectedIndex); // Presenter сам читает индекс из View
            RefreshView();
        }
        catch (IndexOutOfRangeException) { _view.ShowError("Нет элемента с таким индексом."); }
        catch (Exception ex)            { _view.ShowError(ex.Message); }
    }

    // ── Presenter формирует данные и передаёт во View ─────────────────────────

    /// <summary>
    /// Presenter сам решает когда и что показать — View только рисует.
    /// </summary>
    private void RefreshView()
    {
        var rows = _model.List
            .Select((a, i) => new AirportRowViewModel(
                Index:          i,
                Name:           a.Name,
                Location:       a.Location,
                FlightsPerDay:  a.FlightsPerDay,
                TicketsSold:    a.TicketsSold,
                Balance:        a.Balance,
                Rating:         a.Rating,
                EmployeesCount: a.EmployeesCount
            ))
            .ToList();

        _view.ShowAirports(rows); // Presenter командует View
    }
}