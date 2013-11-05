using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.MyLibrary
{
	/// <summary>
	/// Represents a single MyLibrary File
	/// </summary>
	[DataContract]
    [Serializable]
	public class MyLibraryFile : BaseLibrary
	{
		/// <summary>
		/// Gets or sets the description of the file
		/// </summary>
		[DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }
		/// <summary>
        /// File type, string representation.
        /// </summary>
        [DataMember(Name = "file_type", EmitDefaultValue = false)]
        private string FileTypeString { get; set; }
        /// <summary>
        /// Gets or sets the source of the original file
        /// </summary>
        public FileType FileType
        {
            get { return this.FileTypeString.ToEnum<FileType>(); }
            set { this.FileTypeString = value.ToString(); }
        }
		/// <summary>
		/// Gets or sets the name of the folder the file is in
		/// </summary>
		[DataMember(Name = "folder", EmitDefaultValue = false)]
        public string Folder { get; set; }
		/// <summary>
		/// Gets or sets the id of the folder the file is in
		/// </summary>
		[DataMember(Name = "folder_id", EmitDefaultValue = false)]
        public string FolderId { get; set; }
		/// <summary>
		/// Gets or sets the height in pixels of the image 
		/// </summary>
		[DataMember(Name = "height", EmitDefaultValue = false)]
        public int Height { get; set; }
		/// <summary>
		/// Gets or sets whether the file is an image (true) or not (false)
		/// </summary>
		[DataMember(Name = "is_image", EmitDefaultValue = false)]
        public bool IsImage { get; set; }
		/// <summary>
		/// Gets or sets  the size of the file (in bytes) 
		/// </summary>
		[DataMember(Name = "size", EmitDefaultValue = false)]
        public int Size { get; set; }
		/// <summary>
        /// Source, string representation.
        /// </summary>
        [DataMember(Name = "source", EmitDefaultValue = false)]
        private string SourceString { get; set; }
        /// <summary>
        /// Gets or sets the source of the original file
        /// </summary>
        public FileSource Source
        {
            get { return this.SourceString.ToEnum<FileSource>(); }
            set { this.SourceString = value.ToString(); }
        }
		/// <summary>
        /// Status, string representation.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        private string StatusString { get; set; }
        /// <summary>
        /// Gets or sets the file status
        /// </summary>
        public FileStatus Status
        {
            get { return this.StatusString.ToEnum<FileStatus>(); }
            set { this.StatusString = value.ToString(); }
        }
		/// <summary>
		/// Gets or sets the url of the file
		/// </summary>
		[DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }
		/// <summary>
		/// Gets or sets the width (in pixels) of the image
		/// </summary>
		[DataMember(Name = "width", EmitDefaultValue = false)]
        public int Width { get; set; }
		/// <summary>
		/// Gets or sets the thumbnail image of the file
		/// </summary>
		[DataMember(Name = "thumbnail", EmitDefaultValue = false)]
        public Thumbnail Thumbnail { get; set; }

		/// <summary>
		/// Class contructor
		/// </summary>
		public MyLibraryFile()
		{
			this.Thumbnail = new Thumbnail();
		}
	}

	/// <summary>
	/// File type enum
	/// </summary>
	public enum FileType
	{
		/// <summary>
		/// JPEG file type
		/// </summary>
		JPEG,
		/// <summary>
		/// JPG file type
		/// </summary>
		JPG,
		/// <summary>
		/// GIF file type
		/// </summary>
		GIF,
		/// <summary>
		/// PDF file type
		/// </summary>
		PDF,
		/// <summary>
		/// PNG file type
		/// </summary>
		PNG
	}

	/// <summary>
	/// File source enum
	/// </summary>
	public enum FileSource
	{
		/// <summary>
		/// Computer source
		/// </summary>
		MyComputer,
		/// <summary>
		/// StockImage source
		/// </summary>
		StockImage,
		/// <summary>
		/// Facebook source - MyLibrary Plus customers only
		/// </summary>
		Facebook,
		/// <summary>
		/// Istagram source - MyLibrary Plus customers only
		/// </summary>
		Instagram,
		/// <summary>
		/// Shutterstock source
		/// </summary>
		Shutterstock,
		/// <summary>
		/// Mobile source
		/// </summary>
		Mobile
	}

	/// <summary>
	/// File status enum
	/// </summary>
	public enum FileStatus
	{
		/// <summary>
		/// File is available for use
		/// </summary>
		Active,
		/// <summary>
		/// File is in the process of being uploaded to account
		/// </summary>
		Processing,
		/// <summary>
		/// File has been uploaded, but not yet available for use
		/// </summary>
		Uploaded,
		/// <summary>
		/// Virus scan during upload detected a virus, upload cancelled
		/// </summary>
		VirusFound,
		/// <summary>
		/// File upload failed
		/// </summary>
		Failed,
		/// <summary>
		/// File was deleted from MyLibrary
		/// </summary>
		Deleted
	}
}
