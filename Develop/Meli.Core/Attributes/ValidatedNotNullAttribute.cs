namespace Meli.Core.Attributes;
using System;

/// <summary>
/// The not null validation attribute.
/// </summary>
/// <seealso cref="System.Attribute" />
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
public sealed class ValidatedNotNullAttribute : Attribute
{
}