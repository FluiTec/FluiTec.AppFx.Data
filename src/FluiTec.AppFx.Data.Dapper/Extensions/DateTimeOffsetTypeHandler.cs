﻿using System;
using System.Data;
using Dapper;

namespace FluiTec.AppFx.Data.Dapper.Extensions;

/// <summary>   A date time offset type handler. </summary>
public class DateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    /// <summary>   Assign the value of a parameter before a command executes. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <param name="parameter">    The parameter to configure. </param>
    /// <param name="value">        Parameter value. </param>
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (parameter.DbType)
        {
            case DbType.DateTime:
            case DbType.DateTime2:
            case DbType.String:
            case DbType.AnsiString:
                parameter.Value = value.UtcDateTime;
                break;
            case DbType.Object:
                parameter.Value = value.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                break;
            case DbType.DateTimeOffset:
                parameter.Value = value;
                break;
            default:
                throw new InvalidOperationException(
                    "DateTimeOffset must be assigned to a DbType.DateTime SQL field.");
        }
    }

    /// <summary>   Parse a database value back to a typed value. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <param name="value">    The value from the database. </param>
    /// <returns>   The typed value. </returns>
    public override DateTimeOffset Parse(object value)
    {
        switch (value)
        {
            case DateTime time:
                return new DateTimeOffset(time.Add(TimeZoneInfo.Local.BaseUtcOffset),
                    TimeZoneInfo.Local.BaseUtcOffset);
            case DateTimeOffset dto:
                return dto;
            case string timeString:
                return timeString.Contains('+')
                    ? DateTimeOffset.Parse(timeString)
                    : new DateTimeOffset(DateTime.Parse(timeString).Add(TimeZoneInfo.Local.BaseUtcOffset),
                        TimeZoneInfo.Local.BaseUtcOffset);
            default:
                throw new InvalidOperationException("Must be DateTime or DateTimeOffset object to be mapped.");
        }
    }
}