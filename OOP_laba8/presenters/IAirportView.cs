namespace OOP_laba7.presenters;


/// <summary>
/// MVP: интерфейс View.
///
/// Ключевое отличие от MVC:
/// View объявляет интерфейс — что Presenter может у него прочитать и что вызвать.
/// Presenter работает только через этот контракт, не зная о Form2 вообще.
/// View сам не решает что показать — он только выполняет команды Presenter-а.
/// </summary>
public interface IAirportView
{
    // ── Что Presenter читает из View ──────────────────────────────────────────

    /// <summary>Индекс для удаления — Presenter читает сам, View просто хранит.</summary>
    int SelectedIndex { get; }

    // ── Что Presenter вызывает на View ────────────────────────────────────────

    /// <summary>Показать строки таблицы. View не решает откуда данные — Presenter передаёт.</summary>
    void ShowAirports(IReadOnlyList<AirportRowViewModel> rows);

    /// <summary>Дописать строку в лог событий.</summary>
    void AppendLog(string message);

    /// <summary>Показать диалог ошибки.</summary>
    void ShowError(string message);

    // ── События, на которые Presenter подписывается ───────────────────────────
    // View не знает что с ними делать — он только сигнализирует о нажатии кнопки.

    event EventHandler OnAddRandom;
    event EventHandler OnAddPremium;
    event EventHandler OnDelete;
}

/// <summary>
/// Простой DTO — Presenter формирует, View отображает.
/// </summary>
public record AirportRowViewModel(
    int     Index,
    string  Name,
    string  Location,
    int     FlightsPerDay,
    int     TicketsSold,
    decimal Balance,
    double  Rating,
    int     EmployeesCount
);