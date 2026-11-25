using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerTaskManager;

public class TaskItem
{
    [PrimaryKey, AutoIncrement] // <--- Добавить это
    public int Id { get; set; } // <--- Добавить это
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsUrgent { get; set; }
    public bool IsImportant { get; set; }
    public bool IsCompleted { get; set; }

    // Логика матрицы Эйзенхауэра
    public string Quadrant
    {
        get
        {
            if (IsImportant && IsUrgent) return "Важно и срочно";
            if (IsImportant && !IsUrgent) return "Не срочно";
            if (!IsImportant && IsUrgent) return "Срочно, но не важно";
            return "Не обязательно";
        }
    }

    // Строка для отображения в списке
    public string DisplayText => $"{Name} [{Quadrant}]";
}
