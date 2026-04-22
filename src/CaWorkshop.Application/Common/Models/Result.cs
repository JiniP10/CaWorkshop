using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Application.Common.Models;

/// <summary>
/// Represents the outcome of an operation, with success/failure state and optional validation errors.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Indicates whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// A dictionary of validation or business rule errors, if any.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }

    /// <summary>
    /// Protected constructor for Result. Use static methods to create instances.
    /// </summary>
    protected Result(bool isSuccess, IDictionary<string, string[]>? errors = null)
    {
        IsSuccess = isSuccess;
        Errors = errors ?? new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new(true);

    /// <summary>
    /// Creates a failed result with the specified errors.
    /// </summary>
    public static Result Failure(IDictionary<string, string[]> errors) =>
        new(false, errors);

    /// <summary>
    /// Creates a successful typed result.
    /// </summary>
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    /// <summary>
    /// Creates a failed typed result with the specified errors.
    /// </summary>
    public static Result<T> Failure<T>(IDictionary<string, string[]> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return Result<T>.Failure(errors);
    }
}

/// <summary>
/// Represents the outcome of an operation that returns a value on success.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// The value returned from a successful operation.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Protected constructor for Result<T>. Use static methods to create instances.
    /// </summary>
    protected Result(bool isSuccess, T? value, IDictionary<string, string[]>? errors = null)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    public static Result<T> Success(T value) =>
        new(true, value);

    /// <summary>
    /// Creates a failed result with the specified errors.
    /// </summary>
    public static new Result<T> Failure(IDictionary<string, string[]> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return new(false, default, errors);
    }

    /// <summary>
    /// Implicitly converts a value to a successful Result<T>.
    /// </summary>
    public static implicit operator Result<T>(T value) => Success(value);
}

