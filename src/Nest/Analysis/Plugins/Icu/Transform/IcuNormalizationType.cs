﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Forward (default) for LTR and reverse for RTL
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuTransformDirection
	{
		/// <summary>LTR</summary>
		[EnumMember(Value = "forward")]
		Forward,

		/// <summary> RTL</summary>
		[EnumMember(Value = "reverse")]
		Reverse,
	}
}
