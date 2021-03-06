﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public interface IFileSystemRepository : IRepository<IFileSystemRepositorySettings> { }

	public class FileSystemRepository : IFileSystemRepository
	{
		public FileSystemRepository(FileSystemRepositorySettings settings) => Settings = settings;

		public IFileSystemRepositorySettings Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Settings;
		public string Type { get; } = "fs";
	}

	public interface IFileSystemRepositorySettings : IRepositorySettings
	{
		[DataMember(Name ="chunk_size")]
		string ChunkSize { get; set; }

		[DataMember(Name ="compress")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Compress { get; set; }

		[DataMember(Name ="concurrent_streams")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? ConcurrentStreams { get; set; }

		[DataMember(Name ="location")]
		string Location { get; set; }

		[DataMember(Name ="max_restore_bytes_per_second")]
		string RestoreBytesPerSecondMaximum { get; set; }

		[DataMember(Name ="max_snapshot_bytes_per_second")]
		string SnapshotBytesPerSecondMaximum { get; set; }
	}

	public class FileSystemRepositorySettings : IFileSystemRepositorySettings
	{
		internal FileSystemRepositorySettings() { }

		public FileSystemRepositorySettings(string location) => Location = location;

		public string ChunkSize { get; set; }

		public bool? Compress { get; set; }

		public int? ConcurrentStreams { get; set; }

		public string Location { get; set; }

		public string RestoreBytesPerSecondMaximum { get; set; }

		public string SnapshotBytesPerSecondMaximum { get; set; }
	}

	public class FileSystemRepositorySettingsDescriptor
		: DescriptorBase<FileSystemRepositorySettingsDescriptor, IFileSystemRepositorySettings>, IFileSystemRepositorySettings
	{
		string IFileSystemRepositorySettings.ChunkSize { get; set; }
		bool? IFileSystemRepositorySettings.Compress { get; set; }
		int? IFileSystemRepositorySettings.ConcurrentStreams { get; set; }
		string IFileSystemRepositorySettings.Location { get; set; }
		string IFileSystemRepositorySettings.RestoreBytesPerSecondMaximum { get; set; }
		string IFileSystemRepositorySettings.SnapshotBytesPerSecondMaximum { get; set; }

		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public FileSystemRepositorySettingsDescriptor Location(string location) => Assign(location, (a, v) => a.Location = v);

		/// <summary>
		/// Turns on compression of the snapshot files. Defaults to true.
		/// </summary>
		/// <param name="compress"></param>
		public FileSystemRepositorySettingsDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public FileSystemRepositorySettingsDescriptor ConcurrentStreams(int? concurrentStreams) =>
			Assign(concurrentStreams, (a, v) => a.ConcurrentStreams = v);

		/// <summary>
		/// Big files can be broken down into chunks during snapshotting if needed.
		/// The chunk size can be specified in bytes or by using size value notation, i.e. 1g, 10m, 5k.
		/// Defaults to null (unlimited chunk size).
		/// </summary>
		/// <param name="chunkSize"></param>
		public FileSystemRepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(chunkSize, (a, v) => a.ChunkSize = v);

		/// <summary>
		/// Throttles per node restore rate. Defaults to 20mb per second.
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositorySettingsDescriptor RestoreBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(maximumBytesPerSecond, (a, v) => a.RestoreBytesPerSecondMaximum = v);

		/// <summary>
		/// Throttles per node snapshot rate. Defaults to 20mb per second.
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositorySettingsDescriptor SnapshotBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(maximumBytesPerSecond, (a, v) => a.SnapshotBytesPerSecondMaximum = v);
	}

	public class FileSystemRepositoryDescriptor
		: DescriptorBase<FileSystemRepositoryDescriptor, IFileSystemRepository>, IFileSystemRepository
	{
		IFileSystemRepositorySettings IRepository<IFileSystemRepositorySettings>.Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Self.Settings;
		string ISnapshotRepository.Type { get; } = "fs";

		public FileSystemRepositoryDescriptor Settings(string location,
			Func<FileSystemRepositorySettingsDescriptor, IFileSystemRepositorySettings> settingsSelector = null
		) =>
			Assign(settingsSelector.InvokeOrDefault(new FileSystemRepositorySettingsDescriptor().Location(location)), (a, v) => a.Settings = v);
	}
}
