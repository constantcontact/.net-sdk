using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.MyLibrary;
using CTCT.Util;
using CTCT.Exceptions;
using CTCT.Components;

namespace CTCT.Services
{
	/// <summary>
	/// Performs all actions pertaining to the MyLibrary Collection
	/// </summary>
	public class MyLibraryService : BaseService, IMyLibraryService
	{
		/// <summary>
		/// Get MyLibrary usage information
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <returns>Returns a MyLibraryInfo object</returns>
		public MyLibraryInfo GetLibraryInfo(string accessToken, string apiKey)
		{
			MyLibraryInfo result = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.MyLibraryInfo);
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                result = response.Get<MyLibraryInfo>();
            }

            return result;
		}

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		public ResultSet<MyLibraryFolder> GetLibraryFolders(string accessToken, string apiKey, FoldersSortBy? sortBy, int? limit, Pagination pag)
		{
			ResultSet<MyLibraryFolder> results = null;
            string url = (pag == null) ? String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.MyLibraryFolders, GetQueryParameters(new object[] { "sort_by", sortBy, "limit", limit})) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = response.Get<ResultSet<MyLibraryFolder>>();
            }

            return results;
		}

		/// <summary>
		/// Add new folder to MyLibrary
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder AddLibraryFolder(string accessToken, string apiKey, MyLibraryFolder folder)
		{
			MyLibraryFolder newFolder = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.MyLibraryFolders);
            string json = folder.ToJSON();
            CUrlResponse response = RestClient.Post(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                newFolder = Component.FromJSON<MyLibraryFolder>(response.Body);
            }

            return newFolder;
		}

		/// <summary>
		/// Get a folder by Id
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder GetLibraryFolder(string accessToken, string apiKey, string folderId)
		{
			MyLibraryFolder folder = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.MyLibraryFolder, folderId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                folder = Component.FromJSON<MyLibraryFolder>(response.Body);
            }
            
            return folder;
		}

		/// <summary>
		/// Update name and parent_id for a specific folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder UpdateLibraryFolder(string accessToken, string apiKey, MyLibraryFolder folder, bool? includePayload)
		{
			MyLibraryFolder updatedFolder = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, string.Format(Config.Endpoints.MyLibraryFolder, folder.Id), GetQueryParameters(new object[] { "include_payload", includePayload } ));
            string json = folder.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                updatedFolder = Component.FromJSON<MyLibraryFolder>(response.Body);
            }

            return updatedFolder;
		}

		/// <summary>
		/// Delete a specific folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder</param>
		 /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFolder(string accessToken, string apiKey, string folderId)
		{
			string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.MyLibraryFolder, folderId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
		}

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
		public ResultSet<MyLibraryFile> GetLibraryTrashFiles(string accessToken, string apiKey, FileTypes? type, TrashSortBy? sortBy, int? limit, Pagination pag)
		{
			ResultSet<MyLibraryFile> results = null;
            string url = (pag == null) ? String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.MyLibraryTrash, GetQueryParameters(new object[] { "type", type, "sort_by", sortBy, "limit", limit})) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = response.Get<ResultSet<MyLibraryFile>>();
            }

            return results;
		}

		/// <summary>
		/// Delete files in Trash folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		 /// <returns>Returns true if files were deleted successfully, false otherwise</returns>
		public bool DeleteLibraryTrashFiles(string accessToken, string apiKey)
		{
			string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.MyLibraryTrash);
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
		}

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
		public ResultSet<MyLibraryFile> GetLibraryFiles(string accessToken, string apiKey, FileTypes? type, FilesSources? source, int? limit, Pagination pag)
		{
			ResultSet<MyLibraryFile> results = null;
            string url = (pag == null) ? String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.MyLibraryFiles, GetQueryParameters(new object[] { "type", type, "source", source, "limit", limit})) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = response.Get<ResultSet<MyLibraryFile>>();
            }

            return results;
		}

		/// <summary>
		/// Get files from a specific folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder from which to retrieve files</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string accessToken, string apiKey, string folderId, int? limit, Pagination pag)
		{
			ResultSet<MyLibraryFile> results = null;
            string url = (pag == null) ? String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.MyLibraryFolderFiles, folderId), GetQueryParameters(new object[] { "limit", limit})) : pag.GetNextUrl();
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                results = response.Get<ResultSet<MyLibraryFile>>();
            }

            return results;
		}

		/// <summary>
		/// Get file after id
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		public MyLibraryFile GetLibraryFile(string accessToken, string apiKey, string fileId)
		{
			MyLibraryFile file = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.MyLibraryFile, fileId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                file = Component.FromJSON<MyLibraryFile>(response.Body);
            }
            
            return file;
		}

		/// <summary>
		/// Update a specific file
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="file">File to be updated</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		public MyLibraryFile UpdateLibraryFile(string accessToken, string apiKey, MyLibraryFile file, bool? includePayload)
		{
			MyLibraryFile updatedFile = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, string.Format(Config.Endpoints.MyLibraryFile, file.Id), GetQueryParameters(new object[] { "include_payload", includePayload } ));
            string json = file.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                updatedFile = Component.FromJSON<MyLibraryFile>(response.Body);
            }

            return updatedFile;
		}

		/// <summary>
		/// Delete a specific file
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFile(string accessToken, string apiKey, string fileId)
		{
			string url = String.Concat(Config.Endpoints.BaseUrl, String.Format(Config.Endpoints.MyLibraryFile, fileId));
            CUrlResponse response = RestClient.Delete(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
		}

		/// <summary>
		/// Get status for an upload file
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a list of FileUploadStatus objects</returns>
		public IList<FileUploadStatus> GetLibraryFileUploadStatus(string accessToken, string apiKey, string fileId)
		{
			IList<FileUploadStatus> lists = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, string.Format(Config.Endpoints.MyLibraryFileUploadStatus, fileId));
            CUrlResponse response = RestClient.Get(url, accessToken, apiKey);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                lists = response.Get<IList<FileUploadStatus>>();
            }

            return lists;
		}

		/// <summary>
		/// Move files to a different folder
		/// </summary>
		/// <param name="accessToken">Access token.</param>
        /// <param name="apiKey">The API key for the application</param>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="fileIds">List of file ids</param>
		/// <returns>Returns a list of FileMoveResult objects.</returns>
		public IList<FileMoveResult> MoveLibraryFile(string accessToken, string apiKey, string folderId, IList<string> fileIds)
		{
			IList<FileMoveResult> movedFiles = null;
            string url = String.Concat(Config.Endpoints.BaseUrl, string.Format(Config.Endpoints.MyLibraryFolderFiles, folderId));
			string json = fileIds.ToJSON();
            CUrlResponse response = RestClient.Put(url, accessToken, apiKey, json);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

            if (response.HasData)
            {
                movedFiles = Component.FromJSON<IList<FileMoveResult>>(response.Body);
            }

            return movedFiles;
		}

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
		public string AddLibraryFilesMultipart(string accessToken, string apiKey, string fileName, FileType fileType, string folderId, string description, FileSource source, byte[] data)
		{
			string result = null;
			string url = String.Concat(Config.Endpoints.BaseUrl, Config.Endpoints.MyLibraryFiles);
			byte[] content = MultipartBuilder.CreateMultipartContent(fileName, data, null, fileType.ToString(), folderId, description, source.ToString());
			CUrlResponse response = RestClient.PostMultipart(url, accessToken, apiKey, content);

            if (response.IsError)
            {
                throw new CtctException(response.GetErrorMessage());
            }

			if(!response.IsError && response.StatusCode == System.Net.HttpStatusCode.Accepted)
			{
				string location = response.Headers["Location"];
				int idStart = location.LastIndexOf("/") + 1;
				result = location.Substring(idStart);
			}

			return result;
		}
	}

	/// <summary>
    /// Folders sort by enum
    /// </summary>
    public enum FoldersSortBy
    {
		/// <summary>
		/// Sort by date folder was created, ascending
		/// </summary>
        CREATED_DATE,
		/// <summary>
		/// Sort by date folder was last modified, descending
		/// </summary>
		CREATED_DATE_DESC,
		/// <summary>
		/// Sort by date folder was last modified, ascending
		/// </summary>
		MODIFIED_DATE,
		/// <summary>
		/// Sort by date folder was last modified, descending
		/// </summary>
		MODIFIED_DATE_DESC,
		/// <summary>
		/// Sort by name (A to Z)
		/// </summary>
		NAME,
		/// <summary>
		/// Sort by name (Z to A)
		/// </summary>
		NAME_DESC
    }

	/// <summary>
	/// File types enum
	/// </summary>
	public enum FileTypes
	{
		/// <summary>
		/// All files
		/// </summary>
		ALL,
		/// <summary>
		/// Image files
		/// </summary>
		IMAGES,
		/// <summary>
		/// Document files
		/// </summary>
		DOCUMENTS
	}

	/// <summary>
	/// Trash folders sort by enum
	/// </summary>
	public enum TrashSortBy
	{
		/// <summary>
		/// Sort by date folder was created, ascending
		/// </summary>
        CREATED_DATE,
		/// <summary>
		/// Sort by date folder was last modified, descending
		/// </summary>
		CREATED_DATE_DESC,
		/// <summary>
		/// Sort by date folder was last modified, ascending
		/// </summary>
		MODIFIED_DATE,
		/// <summary>
		/// Sort by date folder was last modified, descending
		/// </summary>
		MODIFIED_DATE_DESC,
		/// <summary>
		/// Sort by name (A to Z)
		/// </summary>
		NAME,
		/// <summary>
		/// Sort by name (Z to A)
		/// </summary>
		NAME_DESC,
		/// <summary>
		/// Sort by file size, smallest to largest
		/// </summary>
		SIZE,
		/// <summary>
		/// Sort by file size, largest to smallest
		/// </summary>
		SIZE_DESC,
		/// <summary>
		/// Sort by file domensions, smallest to largest
		/// </summary>
		DIMENSION,
		/// <summary>
		/// Sort by file dimenstiona, largest to smallest
		/// </summary>
		DIMENSION_DESC
	}

	/// <summary>
	/// File source enum
	/// </summary>
	public enum FilesSources
	{
		/// <summary>
		/// Files from all sources
		/// </summary>
		ALL,
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
}
