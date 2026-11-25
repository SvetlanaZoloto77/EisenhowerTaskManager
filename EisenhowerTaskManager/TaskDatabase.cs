using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EisenhowerTaskManager;

public class TaskDatabase
{
    SQLiteAsyncConnection Database;

    public TaskDatabase() { }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.CreateTableAsync<TaskItem>();
    }

    // Получить все задачи
    public async Task<List<TaskItem>> GetTasksAsync()
    {
        await Init();
        return await Database.Table<TaskItem>().ToListAsync();
    }

    // Сохранить новую или обновить существующую задачу
    public async Task<int> SaveTaskAsync(TaskItem item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    // Удалить задачу
    public async Task<int> DeleteTaskAsync(TaskItem item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }

    // Очистить все задачи
    public async Task<int> DeleteAllTasksAsync()
    {
        await Init();
        return await Database.DeleteAllAsync<TaskItem>();
    }
}
