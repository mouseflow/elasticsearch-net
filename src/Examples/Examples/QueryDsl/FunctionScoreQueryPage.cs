using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class FunctionScoreQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line19()
		{
			// tag::a42f33e15b0995bb4b6058659bfdea85[]
			var response0 = new SearchResponse<object>();
			// end::a42f33e15b0995bb4b6058659bfdea85[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""query"": { ""match_all"": {} },
			            ""boost"": ""5"",
			            ""random_score"": {}, \<1>
			            ""boost_mode"":""multiply""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::b4a0d0ed512dffc10ee53bca2feca49b[]
			var response0 = new SearchResponse<object>();
			// end::b4a0d0ed512dffc10ee53bca2feca49b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			          ""query"": { ""match_all"": {} },
			          ""boost"": ""5"", \<1>
			          ""functions"": [
			              {
			                  ""filter"": { ""match"": { ""test"": ""bar"" } },
			                  ""random_score"": {}, \<2>
			                  ""weight"": 23
			              },
			              {
			                  ""filter"": { ""match"": { ""test"": ""cat"" } },
			                  ""weight"": 42
			              }
			          ],
			          ""max_boost"": 42,
			          ""score_mode"": ""max"",
			          ""boost_mode"": ""multiply"",
			          ""min_score"" : 42
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line139()
		{
			// tag::ec473de07fe89bcbac1f8e278617fe46[]
			var response0 = new SearchResponse<object>();
			// end::ec473de07fe89bcbac1f8e278617fe46[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""query"": {
			                ""match"": { ""message"": ""elasticsearch"" }
			            },
			            ""script_score"" : {
			                ""script"" : {
			                  ""source"": ""Math.log(2 + doc['likes'].value)""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line171()
		{
			// tag::b68c85fe1b0d2f264dc0d1cbf530f319[]
			var response0 = new SearchResponse<object>();
			// end::b68c85fe1b0d2f264dc0d1cbf530f319[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""query"": {
			                ""match"": { ""message"": ""elasticsearch"" }
			            },
			            ""script_score"" : {
			                ""script"" : {
			                    ""params"": {
			                        ""a"": 5,
			                        ""b"": 1.2
			                    },
			                    ""source"": ""params.a / Math.pow(params.b, doc['likes'].value)""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line238()
		{
			// tag::645c4c6e209719d3a4d25b1a629cb23b[]
			var response0 = new SearchResponse<object>();
			// end::645c4c6e209719d3a4d25b1a629cb23b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""random_score"": {
			                ""seed"": 10,
			                ""field"": ""_seq_no""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line267()
		{
			// tag::8eaf4d5dd4ab1335deefa7749fdbbcc3[]
			var response0 = new SearchResponse<object>();
			// end::8eaf4d5dd4ab1335deefa7749fdbbcc3[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""field_value_factor"": {
			                ""field"": ""likes"",
			                ""factor"": 1.2,
			                ""modifier"": ""sqrt"",
			                ""missing"": 1
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line379()
		{
			// tag::ec27afee074001b0e4e393611010842b[]
			var response0 = new SearchResponse<object>();
			// end::ec27afee074001b0e4e393611010842b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			            ""gauss"": {
			                ""date"": {
			                      ""origin"": ""2013-09-17"", \<1>
			                      ""scale"": ""10d"",
			                      ""offset"": ""5d"", \<2>
			                      ""decay"" : 0.5 \<2>
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line577()
		{
			// tag::df17f920b0deab3529b98df88b781f55[]
			var response0 = new SearchResponse<object>();
			// end::df17f920b0deab3529b98df88b781f55[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""function_score"": {
			          ""functions"": [
			            {
			              ""gauss"": {
			                ""price"": {
			                  ""origin"": ""0"",
			                  ""scale"": ""20""
			                }
			              }
			            },
			            {
			              ""gauss"": {
			                ""location"": {
			                  ""origin"": ""11, 12"",
			                  ""scale"": ""2km""
			                }
			              }
			            }
			          ],
			          ""query"": {
			            ""match"": {
			              ""properties"": ""balcony""
			            }
			          },
			          ""score_mode"": ""multiply""
			        }
			    }
			}");
		}
	}
}