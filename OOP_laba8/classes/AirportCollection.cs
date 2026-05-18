using OOP_laba7.services;

namespace OOP_laba7.classes;


/// <summary>
/// MVC: Model
/// Хранит данные и бизнес-логику. Ничего не знает о View и Controller.
/// Уведомляет об изменениях через стандартные события.
/// </summary>
public class AirportCollection
{
    public List<Airport> List { get; } = new();
 
    private readonly AbstractFactory _randomFactory        = new RandomAbstractFactory();
    private readonly AbstractFactory _randomPremiumFactory = new PremiumAbstractFactory();
 
    public event Action<string>? ItemAdded;
    public event Action<string>? ItemRemoved;
 
    public void Add(Airport airport)
    {
        List.Add(airport);
        ItemAdded?.Invoke($"Добавлен: {airport.Name}{Environment.NewLine}");
    }
 
    public void Remove(int index)
    {
        if (index < 0 || index >= List.Count)
            throw new IndexOutOfRangeException($"Индекс {index} вне диапазона.");
 
        string name = List[index].Name;
        List.RemoveAt(index);
        ItemRemoved?.Invoke($"Удалён [{index}]: {name}{Environment.NewLine}");
    }
 
    public void AddRandomItem()        => Add(_randomFactory.CreateAirport());
    public void AddRandomPremiumItem() => Add(_randomPremiumFactory.CreateAirport());
 
    public int Count => List.Count;
}