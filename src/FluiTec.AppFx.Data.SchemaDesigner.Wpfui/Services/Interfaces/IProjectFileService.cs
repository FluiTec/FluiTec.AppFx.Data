using System.ComponentModel;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

/// <summary>   Interface for project file service. </summary>
public interface IProjectFileService : INotifyPropertyChanged
{
    /// <summary>   Gets or sets the current file. </summary>
    /// <value> The current file. </value>
    string? CurrentFile { get; set; }
}