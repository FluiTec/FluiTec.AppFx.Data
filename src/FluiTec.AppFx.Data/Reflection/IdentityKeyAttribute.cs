using System;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Attribute for an identity key. </summary>
/// <remarks>
///     Only one identity key is considered and auto incremented by the db.
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class IdentityKeyAttribute : Attribute
{
}