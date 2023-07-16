using System.ComponentModel;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

/// <summary>   Interface for entity service. </summary>
public interface IEntityService : INotifyPropertyChanged
{
    /// <summary>   Gets or sets the current entity. </summary>
    /// <value> The current entity. </value>
    DesignEntity? CurrentEntity { get; set; }
}