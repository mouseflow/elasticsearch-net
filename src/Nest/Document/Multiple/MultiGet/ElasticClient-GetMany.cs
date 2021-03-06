﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Provides GetMany extensions that make it easier to get many documents given a list of ids
	/// </summary>
	public static class GetManyExtensions
	{
		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// <para> </para>
		/// >http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">IEnumerable of ids as string for the documents to fetch</param>
		/// <param name="index">Optionally override the default inferred index name for T</param>
		/// <param name="type">Optionally overiide the default inferred typename for T</param>
		public static IEnumerable<IMultiGetHit<T>> GetMany<T>(this IElasticClient client, IEnumerable<string> ids, IndexName index = null)
			where T : class
		{
			var result = client.MultiGet(s => s
				.Index(index)
				.RequestConfiguration(r => r.ThrowExceptions())
				.GetMany<T>(ids)
			);
			return result.GetMany<T>(ids);
		}

		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// <para> </para>
		/// >http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">IEnumerable of ids as ints for the documents to fetch</param>
		/// <param name="index">Optionally override the default inferred index name for T</param>
		/// <param name="type">Optionally overiide the default inferred typename for T</param>
		public static IEnumerable<IMultiGetHit<T>> GetMany<T>(this IElasticClient client, IEnumerable<long> ids, IndexName index = null)
			where T : class => client.GetMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index);

		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// <para> </para>
		/// >http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">IEnumerable of ids as string for the documents to fetch</param>
		/// <param name="index">Optionally override the default inferred index name for T</param>
		/// <param name="type">Optionally overiide the default inferred typename for T</param>
		public static async Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(
			this IElasticClient client, IEnumerable<string> ids, IndexName index = null, CancellationToken cancellationToken = default)
			where T : class
		{
			var response = await client.MultiGetAsync(s => s
						.Index(index)
						.RequestConfiguration(r => r.ThrowExceptions())
						.GetMany<T>(ids),
					cancellationToken
				)
				.ConfigureAwait(false);
			return response.GetMany<T>(ids);
		}

		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// <para> </para>
		/// >http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">IEnumerable of ids as ints for the documents to fetch</param>
		/// <param name="index">Optionally override the default inferred index name for T</param>
		/// <param name="type">Optionally overiide the default inferred typename for T</param>
		public static Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(
			this IElasticClient client, IEnumerable<long> ids, IndexName index = null, CancellationToken cancellationToken = default
		)
			where T : class => client.GetManyAsync<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index, cancellationToken);
	}
}
