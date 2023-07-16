using System.Windows;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

/// <summary>   A service for accessing windows confirms information. </summary>
public class WindowsConfirmService : IConfirmService
{
    /// <summary>   Confirms. </summary>
    /// <param name="title">    The title. </param>
    /// <param name="text">     The text. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool Confirm(string title, string text)
    {
        return MessageBox.Show(text, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}