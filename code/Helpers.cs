using System.Diagnostics.CodeAnalysis;
using Sandbox;

public static class Helpers
{
#nullable enable
	public static bool IsValid([NotNullWhen(true)] this IValid? obj) => obj != null && obj.IsValid;
#nullable disable
}
