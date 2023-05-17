using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluiTec.AppFx.Console.Modularization;
using Terminal.Gui;
using static ModuleConsole.Modules.ViewSettings;

namespace ModuleConsole.Modules;

/// <summary>   A data module. </summary>
public class DataModule : IWindowModuleItem
{
    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public string Name => "Data";
    
    /// <summary>   Gets the view. </summary>
    /// <returns>   The view. </returns>
    public View GetView()
    {
        var dataView = new DataFrameView("FluiTec.Appfx.Data")
        {
            X = 1,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill(1),
            CanFocus = true
        };

        return dataView;
    }

    /// <summary>   Returns a string that represents the current object. </summary>
    /// <returns>   A string that represents the current object. </returns>
    public override string ToString() => Name;
}

public class DataFrameView : FrameView
{
    public ViewSettings Settings { get; } = new() ;

    public DataFrameView(string name) : base(name)
    {
        Render();
        Settings.PropertyChanged += Settings_PropertyChanged;
    }

    private void Settings_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Settings.Mode))
            Render();
    }

    protected void Render()
    {
        Clear();
        switch (Settings.Mode)
        {
            case ViewMode.Services:
                RenderServicesMode();
                break;
            case ViewMode.Service:
                RenderServiceMode();
                break;
            default:
                RenderServiceMode();
                break;
        }
    }

    protected void RenderServicesMode()
    {
        Add(new Label("Test"));
    }

    protected void RenderServiceMode()
    {

    }
}

public class ViewSettings : INotifyPropertyChanged
{
    private ViewMode _mode;

    public enum ViewMode
    {
        Services,
        Service
    }

    public ViewMode Mode
    {
        get => _mode;
        set
        {
            if (value == _mode) return;
            _mode = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}