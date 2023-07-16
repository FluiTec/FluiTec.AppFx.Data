using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using Microsoft.Win32;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

/// <summary>   A service for accessing windows select files information. </summary>
public class WindowsSelectFileService : ISelectFileService
{
    /// <summary>   Select file. </summary>
    /// <param name="fileName"> [out] Filename of the file. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool SelectFile(out string fileName)
    {
        var dialog = new OpenFileDialog();
        if (dialog.ShowDialog() == true)
        {
            fileName = dialog.FileName;
            return true;
        }

        fileName = null!;
        return false;
    }
}