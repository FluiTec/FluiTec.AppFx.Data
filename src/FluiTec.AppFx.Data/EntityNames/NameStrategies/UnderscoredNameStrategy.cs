namespace FluiTec.AppFx.Data.EntityNames.NameStrategies;

/// <summary>   An underscored name strategy. </summary>
public class UnderscoredNameStrategy : SeparatorNameStrategy
{
    /// <summary>   Default constructor. </summary>
    public UnderscoredNameStrategy() : base("_")
    {
    }
}