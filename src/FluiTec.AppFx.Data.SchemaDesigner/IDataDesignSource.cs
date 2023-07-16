using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

namespace FluiTec.AppFx.Data.SchemaDesigner;

/// <summary>   Interface for data design source. </summary>
public interface IDataDesignSource
{
    /// <summary>   Gets the load. </summary>
    /// <returns>   A DesignSchemaProject. </returns>
    DesignSchemaProject Load();

    /// <summary>   Saves the given design. </summary>
    /// <param name="design">   The design to save. </param>
    void Save(DesignSchemaProject design);
}