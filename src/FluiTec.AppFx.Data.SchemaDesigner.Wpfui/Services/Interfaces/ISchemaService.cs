using System.ComponentModel;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

/// <summary>   Interface for schema service. </summary>
public interface ISchemaService : INotifyPropertyChanged
{
    /// <summary>   Gets the current schema. </summary>
    /// <value> The current schema. </value>
    DesignSchema? CurrentSchema { get; set; }
}