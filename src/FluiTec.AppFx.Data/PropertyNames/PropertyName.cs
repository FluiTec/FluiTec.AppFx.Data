using System;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   An Property name. </summary>
public class PropertyName : IEquatable<string>
{
    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="columnName">   The name. </param>
    /// <param name="propertyName"> The name of the property. </param>
    public PropertyName(string columnName, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(columnName))
            throw new ArgumentNullException(nameof(columnName));
        ColumnName = columnName;

        if (string.IsNullOrWhiteSpace(propertyName))
            throw new ArgumentNullException(nameof(propertyName));
        Name = propertyName;
    }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public string ColumnName { get; }

    /// <summary>   Gets the name of the property. </summary>
    /// <value> The name of the property. </value>
    public string Name { get; }

    /// <summary>
    ///     Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">    An object to compare with this object. </param>
    /// <returns>
    ///     <see langword="true" /> if the current object is equal to the <paramref name="other" />
    ///     parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(string other)
    {
        return ColumnName == other;
    }

    /// <summary>
    ///     Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">    An object to compare with this object. </param>
    /// <returns>
    ///     <see langword="true" /> if the current object is equal to the <paramref name="other" />
    ///     parameter; otherwise, <see langword="false" />.
    /// </returns>
    protected bool Equals(PropertyName other)
    {
        return ColumnName == other.ColumnName && Name == other.Name;
    }

    /// <summary>   Determines whether the specified object is equal to the current object. </summary>
    /// <param name="obj">  The object to compare with the current object. </param>
    /// <returns>
    ///     <see langword="true" /> if the specified object  is equal to the current object;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() == typeof(PropertyName)) return Equals((PropertyName)obj);
        if (obj is string s) return Equals(s);
        return false;
    }

    /// <summary>   Equality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==(PropertyName obj1, PropertyName obj2)
    {
        return obj1.Equals(obj2);
    }

    /// <summary>   Inequality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=(PropertyName obj1, PropertyName obj2)
    {
        return !obj1.Equals(obj2);
    }

    /// <summary>   Equality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==(PropertyName obj1, string obj2)
    {
        return obj1.Equals(obj2);
    }

    /// <summary>   Inequality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=(PropertyName obj1, string obj2)
    {
        return !obj1.Equals(obj2);
    }

    /// <summary>   Equality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==(PropertyName obj1, object obj2)
    {
        return obj1.Equals(obj2);
    }

    /// <summary>   Inequality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=(PropertyName obj1, object obj2)
    {
        return !obj1.Equals(obj2);
    }


    /// <summary>   Serves as the default hash function. </summary>
    /// <returns>   A hash code for the current object. </returns>
    public override int GetHashCode()
    {
        return Name.GetHashCode() + ColumnName.GetHashCode();
    }
}