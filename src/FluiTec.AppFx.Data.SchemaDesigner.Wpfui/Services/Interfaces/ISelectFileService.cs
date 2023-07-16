namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

/// <summary>   Interface for select file service. </summary>
public interface ISelectFileService
{
    /// <summary>   Select file. </summary>
    /// <param name="fileName"> [out] Filename of the file. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    bool SelectFile(out string fileName);
}