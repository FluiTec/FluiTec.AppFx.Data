using System;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Attribute for unmapped. </summary>
[AttributeUsage(AttributeTargets.Property)]
public class UnmappedAttribute : Attribute
{
}