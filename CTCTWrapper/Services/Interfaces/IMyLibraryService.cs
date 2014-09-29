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
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <returns>Returns a MyLibraryInfo object</returns>
		MyLibraryInfo GetLibraryInfo(string accessToken, string apiKey);

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		ResultSet<MyLibraryFolder> GetLibraryFolders(string accessToken, string apiKey, FoldersSortBy? sortBy, int? limit, Pagination pag);

		/// <summary>
		/// Add new folder to MyLibrary
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		MyLibraryFolder AddLibraryFolder(string accessToken, string apiKey, MyLibraryFolder folder);

		/// <summary>
		/// Get a specific folder by Id
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		MyLibraryFolder GetLibraryFolder(string accessToken, string apiKey, string folderId);

		/// <summary>
		/// Update name and parent_id for a specific folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folder">Folder to be updated (with name and parent id)</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		MyLibraryFolder UpdateLibraryFolder(string accessToken, string apiKey, MyLibraryFolder folder, bool? includePayload);

		/// <summary>
		/// Delete a specific folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		bool DeleteLibraryFolder(string accessToken, string apiKey, string folderId);

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		ResultSet<MyLibraryFile> GetLibraryTrashFiles(string accessToken, string apiKey, FileTypes? type, TrashSortBy? sortBy, int? limit, Pagination pag);

		/// <summary>
		/// Delete files in Trash folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		 /// <returns>Returns true if files were deleted successfully, false otherwise</returns>
		bool DeleteLibraryTrashFiles(string accessToken, string apiKey);

		/// <summary>
		/// Get files
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="source">Specifies to retrieve files from a particular source</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		ResultSet<MyLibraryFile> GetLibraryFiles(string accessToken, string apiKey, FileTypes? type, FilesSources? source, int? limit, Pagination pag);

		/// <summary>
		/// Get files from a specific folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder from which to retrieve files</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string accessToken, string apiKey, string folderId, int? limit, Pagination pag);

		/// <summary>
		/// Get file after id
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		MyLibraryFile GetLibraryFile(string accessToken, string apiKey, string fileId);

		/// <summary>
		/// Update a specific file
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="file">File to be updated</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		MyLibraryFile UpdateLibraryFile(string accessToken, string apiKey, MyLibraryFile file, bool? includePayload);

		/// <summary>
		/// Delete a specific file
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		bool DeleteLibraryFile(string accessToken, string apiKey, string fileId);

		/// <summary>
		/// Get status for an upload file
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a list of FileUploadStatus objects</returns>
		IList<FileUploadStatus> GetLibraryFileUploadStatus(string accessToken, string apiKey, string fileId);

		/// <summary>
		/// Move files to a different folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="fileIds">List of comma separated file ids</param>
		/// <returns>Returns a list of FileMoveResult objects.</returns>
		IList<FileMoveResult> MoveLibraryFile(string accessToken, string apiKey, string folderId, IList<string> fileIds);

		/// <summary>
		/// Add files using the multipart content-type
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileName">The file name and extension</param>
		/// <param name="fileType">The file type</param>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="description">The description of the file</param>
		/// <param name="source">The source of the original file</param>
		/// <param name="data">The data contained in the file being uploaded</param>
		/// <returns>Returns the file Id associated with the uploaded file</returns>
		string AddLibraryFilesMultipart(string accessToken, string apiKey, string fileName, FileType fileType, string folderId, string description, FileSource source, byte[] data);
	}
}
