using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Suggesters
{
	public class ContextSuggestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line25()
		{
			// tag::46b3154afd9a05f1aadd726efdd9cf98[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::46b3154afd9a05f1aadd726efdd9cf98[]

			response0.MatchesExample(@"PUT place
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""suggest"" : {
			                ""type"" : ""completion"",
			                ""contexts"": [
			                    { \<1>
			                        ""name"": ""place_type"",
			                        ""type"": ""category""
			                    },
			                    { \<2>
			                        ""name"": ""location"",
			                        ""type"": ""geo"",
			                        ""precision"": 4
			                    }
			                ]
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT place_path_category
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""suggest"" : {
			                ""type"" : ""completion"",
			                ""contexts"": [
			                    { \<3>
			                        ""name"": ""place_type"",
			                        ""type"": ""category"",
			                        ""path"": ""cat""
			                    },
			                    { \<4>
			                        ""name"": ""location"",
			                        ""type"": ""geo"",
			                        ""precision"": 4,
			                        ""path"": ""loc""
			                    }
			                ]
			            },
			            ""loc"": {
			                ""type"": ""geo_point""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line100()
		{
			// tag::2e59a0f8721e27dd537566f4af7a568f[]
			var response0 = new SearchResponse<object>();
			// end::2e59a0f8721e27dd537566f4af7a568f[]

			response0.MatchesExample(@"PUT place/_doc/1
			{
			    ""suggest"": {
			        ""input"": [""timmy's"", ""starbucks"", ""dunkin donuts""],
			        ""contexts"": {
			            ""place_type"": [""cafe"", ""food""] \<1>
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line118()
		{
			// tag::d2a53c6c16ff2305830f64a3efd5f61d[]
			var response0 = new SearchResponse<object>();
			// end::d2a53c6c16ff2305830f64a3efd5f61d[]

			response0.MatchesExample(@"PUT place_path_category/_doc/1
			{
			    ""suggest"": [""timmy's"", ""starbucks"", ""dunkin donuts""],
			    ""cat"": [""cafe"", ""food""] \<1>
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line140()
		{
			// tag::8c3e9da5f412261477c032b33f36a3e9[]
			var response0 = new SearchResponse<object>();
			// end::8c3e9da5f412261477c032b33f36a3e9[]

			response0.MatchesExample(@"POST place/_search?pretty
			{
			    ""suggest"": {
			        ""place_suggestion"" : {
			            ""prefix"" : ""tim"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""size"": 10,
			                ""contexts"": {
			                    ""place_type"": [ ""cafe"", ""restaurants"" ]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line169()
		{
			// tag::8ac73762800c9db1ae418bfc0bcfa65a[]
			var response0 = new SearchResponse<object>();
			// end::8ac73762800c9db1ae418bfc0bcfa65a[]

			response0.MatchesExample(@"POST place/_search?pretty
			{
			    ""suggest"": {
			        ""place_suggestion"" : {
			            ""prefix"" : ""tim"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""size"": 10,
			                ""contexts"": {
			                    ""place_type"": [ \<1>
			                        { ""context"" : ""cafe"" },
			                        { ""context"" : ""restaurants"", ""boost"": 2 }
			                     ]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line254()
		{
			// tag::182162241e42f16f5860ea26fdc52c7e[]
			var response0 = new SearchResponse<object>();
			// end::182162241e42f16f5860ea26fdc52c7e[]

			response0.MatchesExample(@"PUT place/_doc/1
			{
			    ""suggest"": {
			        ""input"": ""timmy's"",
			        ""contexts"": {
			            ""location"": [
			                {
			                    ""lat"": 43.6624803,
			                    ""lon"": -79.3863353
			                },
			                {
			                    ""lat"": 43.6624718,
			                    ""lon"": -79.3873227
			                }
			            ]
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line284()
		{
			// tag::bc79a8936474faf7de6d3c9872678176[]
			var response0 = new SearchResponse<object>();
			// end::bc79a8936474faf7de6d3c9872678176[]

			response0.MatchesExample(@"POST place/_search
			{
			    ""suggest"": {
			        ""place_suggestion"" : {
			            ""prefix"" : ""tim"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""size"": 10,
			                ""contexts"": {
			                    ""location"": {
			                        ""lat"": 43.662,
			                        ""lon"": -79.380
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line318()
		{
			// tag::837c765a38fa0fd5f01b1559138469be[]
			var response0 = new SearchResponse<object>();
			// end::837c765a38fa0fd5f01b1559138469be[]

			response0.MatchesExample(@"POST place/_search?pretty
			{
			    ""suggest"": {
			        ""place_suggestion"" : {
			            ""prefix"" : ""tim"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""size"": 10,
			                ""contexts"": {
			                    ""location"": [ \<1>
			                        {
			                            ""lat"": 43.6624803,
			                            ""lon"": -79.3863353,
			                            ""precision"": 2
			                        },
			                        {
			                            ""context"": {
			                                ""lat"": 43.6624803,
			                                ""lon"": -79.3863353
			                            },
			                            ""boost"": 2
			                        }
			                     ]
			                }
			            }
			        }
			    }
			}");
		}
	}
}