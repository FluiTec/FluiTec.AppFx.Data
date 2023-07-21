using System;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   An Property name. </summary>
public class PropertyName : IEquatable<string>
{
    /// <summary>   Constructor. </summary>
    /// <param name="name">     The name. </param>
    public PropertyName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        Name = name;
    }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; }

    protected bool Equals(PropertyName other)
    {
        return Name == other.Name;
    }

    public bool Equals(string other)
    {
        return Name == other;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() == typeof(PropertyName)) return Equals((PropertyName)obj);
        if (obj is string s) return Equals(s);
        return false;
    }

    /// <summary>   Equality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==(PropertyName obj1, PropertyName obj2) { return obj1.Equals(obj2); }

    /// <summary>   Inequality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=(PropertyName obj1, PropertyName obj2) { return !obj1.Equals(obj2); }

    /// <summary>   Equality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==(PropertyName obj1, string obj2) { return obj1.Equals(obj2); }

    /// <summary>   Inequality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=(PropertyName obj1, string obj2) { return !obj1.Equals(obj2); }

    /// <summary>   Equality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==(PropertyName obj1, object obj2) { return obj1.Equals(obj2); }

    /// <summary>   Inequality operator. </summary>
    /// <param name="obj1"> The first instance to compare. </param>
    /// <param name="obj2"> The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=(PropertyName obj1, object obj2) { return !obj1.Equals(obj2); }


    /// <summary>   Serves as the default hash function. </summary>
    /// <returns>   A hash code for the current object. </returns>
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}