using System;

namespace FluiTec.AppFx.Data.SequentialGuid;

/// <summary>
/// Interface for sequential unique identifier generator.
/// </summary>
public interface ISequentialGuidGenerator
{
    /// <summary>
    /// Generates a sequential unique identifier.
    /// </summary>
    ///
    /// <returns>
    /// The sequential unique identifier.
    /// </returns>
    Guid GenerateSequentialGuid();
}