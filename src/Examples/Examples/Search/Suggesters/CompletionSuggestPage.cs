using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Suggesters
{
	public class CompletionSuggestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line28()
		{
			// tag::b8718ca915bbb848925a5fb593a03e70[]
			var response0 = new SearchResponse<object>();
			// end::b8718ca915bbb848925a5fb593a03e70[]

			response0.MatchesExample(@"PUT music
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""suggest"" : {
			                ""type"" : ""completion""
			            },
			            ""title"" : {
			                ""type"": ""keyword""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line85()
		{
			// tag::223787a2b80e132a22548768ccf7052d[]
			var response0 = new SearchResponse<object>();
			// end::223787a2b80e132a22548768ccf7052d[]

			response0.MatchesExample(@"PUT music/_doc/1?refresh
			{
			    ""suggest"" : {
			        ""input"": [ ""Nevermind"", ""Nirvana"" ],
			        ""weight"" : 34
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line112()
		{
			// tag::5e9f3b7246f4549624fa5b9dd3719d75[]
			var response0 = new SearchResponse<object>();
			// end::5e9f3b7246f4549624fa5b9dd3719d75[]

			response0.MatchesExample(@"PUT music/_doc/1?refresh
			{
			    ""suggest"" : [
			        {
			            ""input"": ""Nevermind"",
			            ""weight"" : 10
			        },
			        {
			            ""input"": ""Nirvana"",
			            ""weight"" : 3
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line134()
		{
			// tag::7c3414279d47e9c29105d061ed316ef8[]
			var response0 = new SearchResponse<object>();
			// end::7c3414279d47e9c29105d061ed316ef8[]

			response0.MatchesExample(@"PUT music/_doc/1?refresh
			{
			  ""suggest"" : [ ""Nevermind"", ""Nirvana"" ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line152()
		{
			// tag::7f951981bd8ed09e56aebeb13adb96ce[]
			var response0 = new SearchResponse<object>();
			// end::7f951981bd8ed09e56aebeb13adb96ce[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nir"", \<1>
			            ""completion"" : { \<2>
			                ""field"" : ""suggest"" \<3>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line222()
		{
			// tag::565ef4aad0c7765879325cc5d2e3c530[]
			var response0 = new SearchResponse<object>();
			// end::565ef4aad0c7765879325cc5d2e3c530[]

			response0.MatchesExample(@"POST music/_search
			{
			    ""_source"": ""suggest"", \<1>
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nir"",
			            ""completion"" : {
			                ""field"" : ""suggest"", \<2>
			                ""size"" : 5 \<3>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line314()
		{
			// tag::b2a6fb1a94dd10bf594dafe727647e1d[]
			var response0 = new SearchResponse<object>();
			// end::b2a6fb1a94dd10bf594dafe727647e1d[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nor"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""skip_duplicates"": true
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line340()
		{
			// tag::a4eac3c0bac550247e8c7d3f9bcaac1c[]
			var response0 = new SearchResponse<object>();
			// end::a4eac3c0bac550247e8c7d3f9bcaac1c[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nor"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""fuzzy"" : {
			                    ""fuzziness"" : 2
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line399()
		{
			// tag::62280b8a1ec0c214b3110a2c42a55fce[]
			var response0 = new SearchResponse<object>();
			// end::62280b8a1ec0c214b3110a2c42a55fce[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""regex"" : ""n[ever|i]r"",
			            ""completion"" : {
			                ""field"" : ""suggest""
			            }
			        }
			    }
			}");
		}
	}
}