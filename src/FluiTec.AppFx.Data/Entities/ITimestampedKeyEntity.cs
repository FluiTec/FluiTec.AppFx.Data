namespace FluiTec.AppFx.Data.Entities
{
    /// <summary>   Interface for timestampted key entity. </summary>
    public interface ITimeStampedKeyEntity : IEntity
    {
        /// <summary>   Gets or sets the timestamp. </summary>
        /// <value> The timestamp. </value>
        long TimeStamp { get; set; }
    }
}