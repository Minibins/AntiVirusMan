using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
public class DustyWindows
{
    [STAThread]
    public static void Main()
    {
        // Создаем объект приложения WPF
        Application app = new Application();

        // Создаем новое окно
        Window mainWindow = new Window
        {
            Title = "Мое WPF окно",
            Width = 300,
            Height = 200,
            WindowStartupLocation = WindowStartupLocation.CenterScreen // Центрируем окно на экране
        };

        // Создаем текстовый блок (Label) и добавляем его на окно
        Label label = new Label
        {
            Content = "Привет, это мое WPF окно!"
        };
        mainWindow.Content = label;

        // Показываем окно
        mainWindow.Show();

        // Запускаем приложение WPF
        app.Run();
    }
}
