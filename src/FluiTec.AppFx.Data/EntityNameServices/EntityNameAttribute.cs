using System;

namespace FluiTec.AppFx.Data.EntityNameServices
{
    /// <summary>	Attribute for entity name. </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityNameAttribute : Attribute
    {
        #region Constructors

        /// <summary>	Constructor. </summary>
        /// <param name="name">	The name of the entity. </param>
        public EntityNameAttribute(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>	Gets or sets the name. </summary>
        /// <value>	The name of the entity. </value>
        public string Name { get; set; }

        #endregion
    }
}