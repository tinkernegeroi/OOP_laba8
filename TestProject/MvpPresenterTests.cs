namespace TestProject;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP_laba7.presenters;



/// <summary>
/// Модульные тесты для MVP.
///
/// Главное преимущество MVP перед MVC: Presenter тестируется через интерфейс IAirportView.
/// Вместо реальной формы подставляется MockView — лёгкая реализация интерфейса.
/// Это позволяет проверить не только логику, но и то, что Presenter
/// правильно вызвал методы View (ShowAirports, AppendLog, ShowError).
/// Форма (MainForm) в тестах не запускается вообще.
/// </summary>
[TestClass]
public class MvpPresenterTests
{
    // ── Mock View ─────────────────────────────────────────────────────────────
    // Заглушка вместо MainForm — реализует IAirportView без UI.

    private class MockView : IAirportView
    {
        // Captured state — что Presenter передал во View
        public IReadOnlyList<AirportRowViewModel> LastShownAirports { get; private set; } = [];
        public List<string> LogMessages { get; } = [];
        public string? LastError { get; private set; }
        public int SelectedIndex { get; set; } = 0;

        // IAirportView events — тесты поднимают их вручную
        public event EventHandler? OnAddRandom;
        public event EventHandler? OnAddPremium;
        public event EventHandler? OnDelete;

        // IAirportView methods — Presenter вызывает, Mock запоминает
        public void ShowAirports(IReadOnlyList<AirportRowViewModel> rows)
            => LastShownAirports = rows;

        public void AppendLog(string message)
            => LogMessages.Add(message);

        public void ShowError(string message)
            => LastError = message;

        // Хелперы для имитации нажатия кнопок из тестов
        public void ClickAddRandom()  => OnAddRandom?.Invoke(this, EventArgs.Empty);
        public void ClickAddPremium() => OnAddPremium?.Invoke(this, EventArgs.Empty);
        public void ClickDelete()     => OnDelete?.Invoke(this, EventArgs.Empty);
    }

    // ── Тест 1: Presenter вызывает ShowAirports после добавления ─────────────

    [TestMethod]
    public void AddRandom_ShouldCallShowAirportsWithOneRow()
    {
        // Arrange
        var view = new MockView();
        _ = new AirportPresenter(view); // Presenter подписывается на события View

        // Act — имитируем нажатие кнопки в форме
        view.ClickAddRandom();

        // Assert — Presenter должен был вызвать ShowAirports с 1 строкой
        Assert.AreEqual(1, view.LastShownAirports.Count);
    }

    // ── Тест 2: Presenter вызывает AppendLog после добавления ────────────────

    [TestMethod]
    public void AddRandom_ShouldCallAppendLogWithMessage()
    {
        // Arrange
        var view = new MockView();
        _ = new AirportPresenter(view);

        // Act
        view.ClickAddRandom();

        // Assert — лог должен содержать хотя бы одно сообщение от Presenter-а
        Assert.IsTrue(view.LogMessages.Count > 0, "Presenter должен залогировать добавление");
        StringAssert.Contains(view.LogMessages[0], "Добавлен");
    }

    // ── Тест 3: Presenter корректно удаляет и обновляет View ─────────────────

    [TestMethod]
    public void Delete_AfterAddingTwo_ShouldShowOneRow()
    {
        // Arrange
        var view = new MockView();
        _ = new AirportPresenter(view);
        view.ClickAddRandom();
        view.ClickAddRandom();
        view.SelectedIndex = 0; // Presenter прочитает это свойство сам

        // Act
        view.ClickDelete();

        // Assert
        Assert.AreEqual(1, view.LastShownAirports.Count);
    }

    // ── Тест 4: Presenter вызывает ShowError при неверном индексе ────────────

    [TestMethod]
    public void Delete_InvalidIndex_ShouldCallShowError()
    {
        // Arrange
        var view = new MockView();
        _ = new AirportPresenter(view);
        view.ClickAddRandom();    // один элемент
        view.SelectedIndex = 99; // несуществующий индекс

        // Act
        view.ClickDelete();

        // Assert — Presenter должен сообщить об ошибке через View, а не бросить исключение
        Assert.IsNotNull(view.LastError, "Presenter должен передать ошибку во View через ShowError");
    }

    // ── Тест 5: премиум аэропорт добавляется и передаётся во View ────────────

    [TestMethod]
    public void AddPremium_ShouldShowAirportWithHighRating()
    {
        // Arrange
        var view = new MockView();
        _ = new AirportPresenter(view);

        // Act
        view.ClickAddPremium();

        // Assert — премиум фабрика генерирует рейтинг от 4.5
        Assert.AreEqual(1, view.LastShownAirports.Count);
        Assert.IsTrue(
            view.LastShownAirports[0].Rating >= 4.5,
            $"Премиум аэропорт должен иметь рейтинг ≥ 4.5, получили {view.LastShownAirports[0].Rating}"
        );
    }
}