using OOP_laba7.classes;
using OOP_laba7.services;

namespace OOP_laba7.Controllers;

/// <summary>
/// MVC: Controller
/// Принимает команды от View, управляет Model, возвращает данные обратно во View.
/// View не знает о Model — только о Controller.
/// </summary>
public class AirportController
{
    // Controller владеет Model
    private readonly AirportCollection _model;

    // Controller сообщает View об изменениях через события
    public event Action? DataChanged;
    public event Action<string>? ActionLogged;

    public AirportController()
    {
        _model = new AirportCollection();

        // Controller подписывается на события Model и транслирует их во View
        _model.ItemAdded   += msg => ActionLogged?.Invoke(msg);
        _model.ItemRemoved += msg => ActionLogged?.Invoke(msg);
    }

    // --- Команды от View ---

    public void AddRandomAirport()
    {
        _model.AddRandomItem();
        DataChanged?.Invoke();
    }

    public void DeleteAirport(int index)
    {
        _model.Remove(index);   // бросает IndexOutOfRangeException — View поймает
        DataChanged?.Invoke();
    }

    public void AddRandomPremiumAirport()
    {
        _model.AddRandomPremiumItem();
        DataChanged?.Invoke();
    }

    // --- Данные для View ---

    /// <summary>
    /// View запрашивает у Controller список для отображения.
    /// View получает копию — он не трогает Model напрямую.
    /// </summary>
    public IReadOnlyList<AirportViewModel> GetAirports()
    {
        return _model.List
            .Select((a, i) => new AirportViewModel(
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
    }

    public (string Airport, string Airplane) CreateRandomFamily()
    {
        var factory  = new RandomAbstractFactory();
        var airport  = factory.CreateAirport();
        var airplane = factory.CreateAirplane();
        return (airport.ToString(), airplane.ToString());
    }

    public (string Airport, string Airplane) CreatePremiumFamily()
    {
        var factory  = new PremiumAbstractFactory();
        var airport  = factory.CreateAirport();
        var airplane = factory.CreateAirplane();
        return (airport.ToString(), airplane.ToString());
    }
}

/// <summary>
/// ViewModel — простой DTO, который Controller отдаёт во View.
/// View знает только этот тип, не знает класс Airport из Model.
/// </summary>
public record AirportViewModel(
    int     Index,
    string  Name,
    string  Location,
    int     FlightsPerDay,
    int     TicketsSold,
    decimal Balance,
    double  Rating,
    int     EmployeesCount
);