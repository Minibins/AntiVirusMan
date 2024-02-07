using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
public class DustyWindows
{
    [STAThread]
    public static void Main()
    {
        // ������� ������ ���������� WPF
        Application app = new Application();

        // ������� ����� ����
        Window mainWindow = new Window
        {
            Title = "��� WPF ����",
            Width = 300,
            Height = 200,
            WindowStartupLocation = WindowStartupLocation.CenterScreen // ���������� ���� �� ������
        };

        // ������� ��������� ���� (Label) � ��������� ��� �� ����
        Label label = new Label
        {
            Content = "������, ��� ��� WPF ����!"
        };
        mainWindow.Content = label;

        // ���������� ����
        mainWindow.Show();

        // ��������� ���������� WPF
        app.Run();
    }
}
