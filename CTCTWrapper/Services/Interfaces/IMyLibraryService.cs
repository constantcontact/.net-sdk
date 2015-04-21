using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.MyLibrary;
using CTCT.Components;

namespace CTCT.Services
{
	/// <summary>
	/// Interface for MyLibraryService class
	/// </summary>
	public interface IMyLibraryService
	{
		/// <summary>
		/// Get MyLibrary usage information
		/// </summary>
		/// <returns>Returns a MyLibraryInfo object</returns>
		MyLibraryInfo GetLibraryInfo();

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy, int? limit, Pagination pag);

		/// <summary>
		/// Add new folder to MyLibrary
		/// </summary>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		MyLibraryFolder AddLibraryFolder(MyLibraryFolder folder);

		/// <summary>
		/// Get a specific folder by Id
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		MyLibraryFolder GetLibraryFolder(string folderId);

		/// <summary>
		/// Update name and parent_id for a specific folder
		/// </summary>
		/// <param name="folder">Folder to be updated (with name and parent id)</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder, bool? includePayload);

		/// <summary>
		/// Delete a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		bool DeleteLibraryFolder(string folderId);

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy, int? limit, Pagination pag);

		/// <summary>
		/// Delete files in Trash folder
		/// </summary>
		 /// <returns>Returns true if files were deleted successfully, false otherwise</returns>
		bool DeleteLibraryTrashFiles();

		/// <summary>
		/// Get files
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="source">Specifies to retrieve files from a particular source</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source, int? limit, Pagination pag);

		/// <summary>
		/// Get files from a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder from which to retrieve files</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId, int? limit, Pagination pag);

		/// <summary>
		/// Get file after id
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		MyLibraryFile GetLibraryFile(string fileId);

		/// <summary>
		/// Update a specific file
		/// </summary>
		/// <param name="file">File to be updated</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		MyLibraryFile UpdateLibraryFile(MyLibraryFile file, bool? includePayload);

		/// <summary>
		/// Delete a specific file
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		bool DeleteLibraryFile(string fileId);

		/// <summary>
		/// Get status for an upload file
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a list of FileUploadStatus objects</returns>
		IList<FileUploadStatus> GetLibraryFileUploadStatus(string fileId);

		/// <summary>
		/// Move files to a different folder
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="fileIds">List of comma separated file ids</param>
		/// <returns>Returns a list of FileMoveResult objects.</returns>
		IList<FileMoveResult> MoveLibraryFile(string folderId, IList<string> fileIds);

		/// <summary>
		/// Add files using the multipart content-type
		/// </summary>
		/// <param name="fileName">The file name and extension</param>
		/// <param name="fileType">The file type</param>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="description">The description of the file</param>
		/// <param name="source">The source of the original file</param>
		/// <param name="data">The data contained in the file being uploaded</param>
		/// <returns>Returns the file Id associated with the uploaded file</returns>
		string AddLibraryFilesMultipart(string fileName, FileType fileType, string folderId, string description, FileSource source, byte[] data);

        /// <summary>
        /// Get all existing MyLibrary folders
        /// </summary>
        /// <returns>Returns a collection of MyLibraryFolder objects.</returns>
        ResultSet<MyLibraryFolder> GetLibraryFolders();

        /// <summary>
        /// Update name and parent_id for a specific folder
        /// </summary>
        /// <param name="folder">Folder to be added (with name and parent id)</param>
        /// <returns>Returns a MyLibraryFolder object.</returns>
        MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder);

        /// <summary>
        /// Get files from Trash folder
        /// </summary>
        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
        ResultSet<MyLibraryFile> GetLibraryTrashFiles();

        /// <summary>
        /// Get files
        /// </summary>
        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
        ResultSet<MyLibraryFile> GetLibraryFiles();

        /// <summary>
        /// Get files from a specific folder
        /// </summary>
        /// <param name="folderId">The id of the folder from which to retrieve files</param>
        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
        ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId);
	}
}
