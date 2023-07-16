using System.ComponentModel;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

public interface IProjectService : INotifyPropertyChanged
{
    /// <summary>   Gets or sets the current project. </summary>
    /// <value> The current project. </value>
    DesignSchemaProject? CurrentProject { get; }

    /// <summary>   Gets the current design source. </summary>
    /// <value> The current design source. </value>
    IDataDesignSource? CurrentDesignSource { get; }
}