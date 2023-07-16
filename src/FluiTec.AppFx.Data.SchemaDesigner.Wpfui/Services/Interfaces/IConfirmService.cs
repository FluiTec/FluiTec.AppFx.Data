namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

/// <summary>   Interface for confirm service. </summary>
public interface IConfirmService
{
    /// <summary>   Confirms. </summary>
    /// <param name="title">    The title. </param>
    /// <param name="text">     The text. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    bool Confirm(string title, string text);
}