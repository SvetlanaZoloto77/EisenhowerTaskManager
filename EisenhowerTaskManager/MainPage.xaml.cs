using System.Collections.ObjectModel;

namespace EisenhowerTaskManager;

public partial class MainPage : ContentPage
{
    private TaskDatabase _database;
    public ObservableCollection<TaskItem> Tasks { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        _database = new TaskDatabase();
        TasksCollectionView.ItemsSource = Tasks;
    }

    // Этот метод срабатывает каждый раз, когда страница открывается
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Загружаем задачи из базы данных
        var loadedTasks = await _database.GetTasksAsync();

        // 2. Очищаем текущий список на экране
        Tasks.Clear();

        // 3. Добавляем загруженные задачи обратно на экран
        foreach (var task in loadedTasks)
        {
            Tasks.Add(task);
        }
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        var name = EntryName.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Ошибка", "Введите название задачи", "OK");
            return;
        }

        var newTask = new TaskItem
        {
            Name = name,
            Description = EntryDesc.Text?.Trim() ?? "",
            IsUrgent = CheckUrgent.IsChecked,
            IsImportant = CheckImportant.IsChecked,
            IsCompleted = false
        };

        // СОХРАНЯЕМ В БАЗУ ДАННЫХ
        await _database.SaveTaskAsync(newTask);

        // Добавляем на экран
        Tasks.Add(newTask);

        // Очищаем поля ввода
        EntryName.Text = string.Empty;
        EntryDesc.Text = string.Empty;
        CheckUrgent.IsChecked = false;
        CheckImportant.IsChecked = false;
    }

    // Обработчик кнопки "Удалить"
    private async void OnDeleteTaskClicked(object sender, EventArgs e)
    {
        // Проверяем, что нажата именно кнопка и у нее есть привязанная задача
        if (sender is Button button && button.BindingContext is TaskItem task)
        {
            // УДАЛЯЕМ ИЗ БАЗЫ ДАННЫХ
            await _database.DeleteTaskAsync(task);

            // Убираем с экрана
            Tasks.Remove(task);
        }
    }

    // Обработчик кнопки "Очистить все"
    private async void OnClearAllClicked(object sender, EventArgs e)
    {
        // УДАЛЯЕМ ВСЁ ИЗ БАЗЫ ДАННЫХ
        await _database.DeleteAllTasksAsync();

        // Очищаем экран
        Tasks.Clear();
    }
}
