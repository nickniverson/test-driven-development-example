using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDrivenDevelopmentExample.Tests
{
	[TestClass]
	public class RecursiveGenericTypeFormatterTests
	{
		internal ITypeToStringFormatter Target { get; set; }

		internal RecursiveGenericTypeFormatterBuilder Builder { get; set; }


		[TestInitialize]
		public virtual void TestInitialize()
		{
			Builder = new RecursiveGenericTypeFormatterBuilder();
		}



		[TestClass]
		public class Constructor : RecursiveGenericTypeFormatterTests
		{
			[TestMethod]
			public void Should_Create_An_Instance()
			{
				Target = Builder.Build();

				Assert.IsInstanceOfType(
					value: Target,
					expectedType: typeof(RecursiveGenericTypeFormatter));
			}
		}



		[TestClass]
		public class FormatAsStringMethod : RecursiveGenericTypeFormatterTests
		{
			[TestInitialize]
			public override void TestInitialize()
			{
				base.TestInitialize();

				Target = Builder.Build();
			}


			[TestMethod]
			public void Should_Work_With_Non_Generic_Types()
			{
				var result = Target.FormatAsString(typeof(string));

				Assert.AreEqual(
					expected: "string",
					actual: result);
			}


			[TestMethod]
			public void Should_Support_Generics_With_One_Type_Parameter()
			{
				var result = Target.FormatAsString(typeof(List<string>));

				Assert.AreEqual(
					expected: "List<string>",
					actual: result);
			}


			[TestMethod]
			public void Should_Support_Multiple_Levels_Of_Nested_Generics_With_One_Parameter()
			{
				var result = Target.FormatAsString(typeof(List<List<List<string>>>));

				Assert.AreEqual(
					expected: "List<List<List<string>>>",
					actual: result);
			}


			[TestMethod]
			public void Should_Support_Generics_With_Multiple_Type_Parameters()
			{
				var result = Target.FormatAsString(typeof(Dictionary<string, string>));

				Assert.AreEqual(
					expected: "Dictionary<string, string>",
					actual: result);
			}


			[TestMethod]
			public void Should_Support_Multiple_Levels_Of_Nested_Generics_With_Multiple_Parameters()
			{
				var result = Target.FormatAsString(typeof(Dictionary<Dictionary<string, List<string>>, Dictionary<string, string>>));

				Assert.AreEqual(
					expected: "Dictionary<Dictionary<string, List<string>>, Dictionary<string, string>>",
					actual: result);
			}
		}



		internal class RecursiveGenericTypeFormatterBuilder
		{
			internal ITypeToStringFormatter Build()
			{
				return new RecursiveGenericTypeFormatter();
			}
		}
	}
}
