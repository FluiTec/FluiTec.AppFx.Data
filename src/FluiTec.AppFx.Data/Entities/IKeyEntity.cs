namespace FluiTec.AppFx.Data.Entities
{
    /// <summary>	Interface for an entity with a key. </summary>
    /// <typeparam name="TKey">	Type of the key. </typeparam>
    public interface IKeyEntity<TKey> : IEntity
    {
        /// <summary>	Gets or sets the identifier. </summary>
        /// <value>	The identifier. </value>
        TKey Id { get; set; }
    }
}