﻿using FluentValidation;

namespace FluiTec.AppFx.Data.Dapper;

/// <summary>   A dapper service options validator. </summary>
// ReSharper disable once UnusedMember.Global
public class DapperServiceOptionsValidator : AbstractValidator<DapperServiceOptions>
{
    /// <summary>   Default constructor. </summary>
    public DapperServiceOptionsValidator()
    {
        RuleFor(options => options.ConnectionFactory).NotNull();
        RuleFor(options => options.ConnectionString).NotEmpty();
    }
}