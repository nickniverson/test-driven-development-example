using System;

namespace TestDrivenDevelopmentExample
{
	public interface ITypeToStringFormatter
	{
		string FormatAsString(Type type);
	}
}