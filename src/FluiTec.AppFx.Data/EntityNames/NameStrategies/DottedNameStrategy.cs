namespace FluiTec.AppFx.Data.EntityNames.NameStrategies;

/// <summary>   A dotted name strategy. </summary>
public class DottedNameStrategy : SeparatorNameStrategy
{
    /// <summary>   Default constructor. </summary>
    public DottedNameStrategy() : base(".")
    {
    }
}