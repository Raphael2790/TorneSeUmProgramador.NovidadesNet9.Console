namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class InterceptadorAttribute(string filePath, int line, int character) : Attribute
{
}
