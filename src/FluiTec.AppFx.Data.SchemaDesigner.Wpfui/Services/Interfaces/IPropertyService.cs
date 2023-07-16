using System.ComponentModel;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

/// <summary>   Interface for property service. </summary>
public interface IPropertyService : INotifyPropertyChanged
{
    /// <summary>   Gets or sets the current property. </summary>
    /// <value> The current property. </value>
    DesignProperty? CurrentProperty { get; set; }
}