using FluiTec.AppFx.Data.SchemaDesigner.DataModels;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

namespace FluiTec.AppFx.Data.SchemaDesigner;

/// <summary>   A model data design source. </summary>
public abstract class ModelDataDesignSource : IDataDesignSource
{
    /// <summary>   Gets the load. </summary>
    /// <returns>   A DesignSchemaProject. </returns>
    public virtual DesignSchemaProject Load()
    {
        var project = LoadProject();
        var design = new DesignSchemaProject
        {
            Schemata = new ObservableCollectionWithItemNotify<DesignSchema>(project.Schemata.Select(ds =>
                new DesignSchema
                {
                    Name = ds.Name,
                    Entities = new ObservableCollectionWithItemNotify<DesignEntity>(ds.Entities.Select(de =>
                        new DesignEntity
                        {
                            Name = de.Name,
                            Properties = new ObservableCollectionWithItemNotify<DesignProperty>(de.Properties.Select(
                                dp => new DesignProperty
                                {
                                    Name = dp.Name,
                                    Type = Type.GetType(dp.TypeName)!,
                                    KeyOrder = dp.KeyOrder,
                                    Nullable = dp.Nullable
                                }))
                        }))
                }))
        };
        AcceptChanges(design);

        return design;
    }

    /// <summary>   Saves the given design. </summary>
    /// <param name="design">   The design to save. </param>
    public virtual void Save(DesignSchemaProject design)
    {
        var project = new DataSchemaProject
        {
            Schemata = design.Schemata.Select(ds => new DataSchema
            {
                Name = ds.Name,
                Entities = ds.Entities.Select(de => new DataEntity
                {
                    Name = de.Name,
                    Properties = de.Properties.Select(dp => new DataProperty
                    {
                        Name = dp.Name,
                        TypeName = dp.Type.AssemblyQualifiedName!,
                        KeyOrder = dp.KeyOrder,
                        Nullable = dp.Nullable
                    }).ToList()
                }).ToList()
            }).ToList()
        };

        Save(project);
        AcceptChanges(design);
    }

    /// <summary>   Accept changes. </summary>
    /// <param name="design">   The design to save. </param>
    protected virtual void AcceptChanges(DesignSchemaProject design)
    {
        design.AcceptChanges();
        foreach (var schema in design.Schemata)
        {
            schema.AcceptChanges();
            foreach (var entity in schema.Entities)
            {
                entity.AcceptChanges();
                foreach (var property in entity.Properties) property.AcceptChanges();
            }
        }
    }

    /// <summary>   Loads the project. </summary>
    /// <returns>   The project. </returns>
    protected abstract DataSchemaProject LoadProject();

    /// <summary>   Saves the given design. </summary>
    /// <param name="project">  The project to save. </param>
    protected abstract void Save(DataSchemaProject project);
}