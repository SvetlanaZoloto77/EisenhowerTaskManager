using System.IO;

namespace EisenhowerTaskManager;

public static class Constants
{
    public const string DatabaseFilename = "TasksSQLite.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // открывать базу в режиме чтения/записи
        SQLite.SQLiteOpenFlags.ReadWrite |
        // создавать базу, если её нет
        SQLite.SQLiteOpenFlags.Create |
        // включить многопоточный доступ
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
