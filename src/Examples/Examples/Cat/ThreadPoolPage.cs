using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class ThreadPoolPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::ad88e46bb06739991498dee248850223[]
			var response0 = new SearchResponse<object>();
			// end::ad88e46bb06739991498dee248850223[]

			response0.MatchesExample(@"GET /_cat/thread_pool");
		}

		[U(Skip = "Example not implemented")]
		public void Line98()
		{
			// tag::ab5e724a4baa0cc44df33f7d62583e7f[]
			var response0 = new SearchResponse<object>();
			// end::ab5e724a4baa0cc44df33f7d62583e7f[]

			response0.MatchesExample(@"GET /_cat/thread_pool/generic?v&h=id,name,active,rejected,completed");
		}
	}
}