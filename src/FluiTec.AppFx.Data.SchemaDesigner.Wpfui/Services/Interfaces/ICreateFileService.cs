namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

/// <summary>   Interface for create file service. </summary>
public interface ICreateFileService
{
    /// <summary>   Creates a file. </summary>
    /// <param name="fileName"> [out] Filename of the file. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    bool CreateFile(out string fileName);
}