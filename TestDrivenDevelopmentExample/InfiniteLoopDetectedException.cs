using System;

namespace TestDrivenDevelopmentExample
{
	public class InfiniteLoopDetectedException : ApplicationException
	{
		public InfiniteLoopDetectedException()
		{
		}

		public InfiniteLoopDetectedException(string message) : base(message)
		{
		}
	}
}