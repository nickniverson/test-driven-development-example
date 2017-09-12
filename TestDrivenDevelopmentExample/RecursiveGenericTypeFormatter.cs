using System;
using System.Text;

namespace TestDrivenDevelopmentExample
{
	public class RecursiveGenericTypeFormatter : ITypeToStringFormatter
	{
		public string FormatAsString(Type type)
		{
			return FormatAsString(
				type: type,
				depthCounter: 0);
		}


		private string FormatAsString(Type type, int depthCounter)
		{
			// this shouldn't happen, but we'll leave it in here as a safety guard just in case an issue manifests
			if (depthCounter > 1000)
			{
				throw new InfiniteLoopDetectedException("inifinite loop detected in:  RecursiveGenericTypeFormatter.FormatAsString(...)");
			}

			if (!type.IsConstructedGenericType)
			{
				return LookupPreferredTypeNameFor(type);
			}

			var result = new StringBuilder();

			result
				.Append(FormatTypeNameWithoutGenericTickMarks(type))
				.Append("<")
				.Append(FormatGenericTypeArguments(type, depthCounter))
				.Append(">");

			return result.ToString();
		}


		private string FormatTypeNameWithoutGenericTickMarks(Type type)
		{
			return type.Name.Substring(0, type.Name.IndexOf("`"));
		}


		/// <remarks>
		/// Recurses back into FormatAsString
		/// </remarks>
		private string FormatGenericTypeArguments(Type type, int depthCounter)
		{
			var result = new StringBuilder();

			for (var i = 0; i < type.GenericTypeArguments.Length; i++)
			{
				Type argumentType = type.GenericTypeArguments[i];

				if (HasMoreThanOneArgument(type) && IsNotLastArgument(type, i))
				{
					result
						.Append(FormatAsString(argumentType, ++depthCounter))
						.Append(", ");
				}
				else
				{
					result.Append(FormatAsString(argumentType, ++depthCounter));
				}
			}

			return result.ToString();
		}


		private bool HasMoreThanOneArgument(Type type)
		{
			return type.GenericTypeArguments.Length > 1;
		}


		private bool IsNotLastArgument(Type type, int i)
		{
			return i < type.GenericTypeArguments.Length - 1;
		}


		/// <summary>
		/// A few of the simple types don't quite match the way they're declared.  
		/// For example string becomes String and int becomes Int32.  So, this 
		/// function just tries to reconcile those naming quirks.  I'm sure there
		/// are other types that I'm missing, but this should suffice for now.
		/// </summary>
		private string LookupPreferredTypeNameFor(Type type)
		{
			if (type == typeof(string))
			{
				return "string";
			}

			if (type == typeof(short))
			{
				return "short";
			}

			if (type == typeof(ushort))
			{
				return "ushort";
			}

			if (type == typeof(int))
			{
				return "int";
			}

			if (type == typeof(uint))
			{
				return "uint";
			}

			if (type == typeof(long))
			{
				return "long";
			}

			if (type == typeof(ulong))
			{
				return "ulong";
			}

			if (type == typeof(double))
			{
				return "double";
			}

			if (type == typeof(float))
			{
				return "float";
			}

			if (type == typeof(decimal))
			{
				return "decimal";
			}

			return type.Name;
		}
	}
}