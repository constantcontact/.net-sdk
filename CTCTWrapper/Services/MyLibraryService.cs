using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Components.MyLibrary;
using CTCT.Util;
using CTCT.Exceptions;
using CTCT.Components;
using System.IO;

namespace CTCT.Services
{
	/// <summary>
	/// Performs all actions pertaining to the MyLibrary Collection
	/// </summary>
	public class MyLibraryService : BaseService, IMyLibraryService
	{
        /// <summary>
        /// My library service constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public MyLibraryService(IUserServiceContext userServiceContext)
            : base(userServiceContext)
        {
        }

		/// <summary>
		/// Get MyLibrary usage information
		/// </summary>
		/// <returns>Returns a MyLibraryInfo object</returns>
		public MyLibraryInfo GetLibraryInfo()
		{
            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.MyLibraryInfo);
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
               var result = response.Get<MyLibraryInfo>();
               return result;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
		}

		/// <summary>
		/// Get all existing MyLibrary folders
		/// </summary>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFolder objects.</returns>
		public ResultSet<MyLibraryFolder> GetLibraryFolders(FoldersSortBy? sortBy, int? limit, Pagination pag)
		{
            string url = (pag == null) ? String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.MyLibraryFolders, GetQueryParameters(new object[] { "sort_by", sortBy, "limit", limit })) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<MyLibraryFolder>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
		}

		/// <summary>
		/// Add new folder to MyLibrary
		/// </summary>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder AddLibraryFolder(MyLibraryFolder folder)
		{
            if (folder == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.MyLibraryFolders);
            string json = folder.ToJSON();
            RawApiResponse response = RestClient.Post(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var newFolder = response.Get<MyLibraryFolder>();
                return newFolder;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
		}

		/// <summary>
		/// Get a folder by Id
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder GetLibraryFolder(string folderId)
		{
            if (string.IsNullOrEmpty(folderId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.MyLibraryFolder, folderId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var folder = response.Get<MyLibraryFolder>();
                return folder;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
		}

		/// <summary>
		/// Update name and parent_id for a specific folder
		/// </summary>
		/// <param name="folder">Folder to be added (with name and parent id)</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFolder object.</returns>
		public MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder, bool? includePayload)
		{
            if (folder == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, string.Format(Settings.Endpoints.Default.MyLibraryFolder, folder.Id), GetQueryParameters(new object[] { "include_payload", includePayload } ));
            string json = folder.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var updatedFolder = response.Get<MyLibraryFolder>();
                return updatedFolder;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }  
		}

		/// <summary>
		/// Delete a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		 /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFolder(string folderId)
		{
            if (string.IsNullOrEmpty(folderId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

			string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.MyLibraryFolder, folderId));
            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }    
		}

        /// <summary>
        /// Delete a specific folder
        /// </summary>
        /// <param name="folder">The MyLibraryFolder</param>
        /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
        public bool DeleteLibraryFolder(MyLibraryFolder folder)
        {
            if (folder == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            return DeleteLibraryFolder(folder.Id);
        }

		/// <summary>
		/// Get files from Trash folder
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="sortBy">Specifies how the list of folders is sorted</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryTrashFiles(FileTypes? type, TrashSortBy? sortBy, int? limit, Pagination pag)
		{
            string url = (pag == null) ? String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.MyLibraryTrash, GetQueryParameters(new object[] { "type", type, "sort_by", sortBy, "limit", limit})) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<MyLibraryFile>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Delete files in Trash folder
		/// </summary>
		 /// <returns>Returns true if files were deleted successfully, false otherwise</returns>
		public bool DeleteLibraryTrashFiles()
		{
			string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.MyLibraryTrash);
            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Get files
		/// </summary>
		/// <param name="type">The type of the files to retrieve</param>
		/// <param name="source">Specifies to retrieve files from a particular source</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFiles(FileTypes? type, FilesSources? source, int? limit, Pagination pag)
		{
            string url = (pag == null) ? String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.MyLibraryFiles, GetQueryParameters(new object[] { "type", type, "source", source, "limit", limit})) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<MyLibraryFile>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Get files from a specific folder
		/// </summary>
		/// <param name="folderId">The id of the folder from which to retrieve files</param>
		/// <param name="limit">Specifies the number of results per page in the output, from 1 - 50, default = 50.</param>
		/// <param name="pag">Pagination object.</param>
		/// <returns>Returns a collection of MyLibraryFile objects.</returns>
		public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId, int? limit, Pagination pag)
		{
            if (string.IsNullOrEmpty(folderId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = (pag == null) ? String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.MyLibraryFolderFiles, folderId), GetQueryParameters(new object[] { "limit", limit})) : pag.GetNextUrl();
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var results = response.Get<ResultSet<MyLibraryFile>>();
                return results;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Get file after id
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		public MyLibraryFile GetLibraryFile(string fileId)
		{
            if (string.IsNullOrEmpty(fileId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.MyLibraryFile, fileId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var file = response.Get<MyLibraryFile>();
                return file;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Update a specific file
		/// </summary>
		/// <param name="file">File to be updated</param>
		/// <param name="includePayload">Determines if update's folder JSON payload is returned</param>
		/// <returns>Returns a MyLibraryFile object.</returns>
		public MyLibraryFile UpdateLibraryFile(MyLibraryFile file, bool? includePayload)
		{
            if (file == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, string.Format(Settings.Endpoints.Default.MyLibraryFile, file.Id), GetQueryParameters(new object[] { "include_payload", includePayload } ));
            string json = file.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var updatedFile = response.Get<MyLibraryFile>();
                return updatedFile;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Delete a specific file
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
		public bool DeleteLibraryFile(string fileId)
		{
            if (string.IsNullOrEmpty(fileId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

			string url = String.Concat(Settings.Endpoints.Default.BaseUrl, String.Format(Settings.Endpoints.Default.MyLibraryFile, fileId));
            RawApiResponse response = RestClient.Delete(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                return (!response.IsError && response.StatusCode == System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
		}

        /// <summary>
        /// Delete a specific file
        /// </summary>
        /// <param name="file">The MyLibraryFile</param>
        /// <returns>Returns true if folder was deleted successfully, false otherwise</returns>
        public bool DeleteLibraryFile(MyLibraryFile file)
        {
            if (file == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            return DeleteLibraryFile(file.Id);
        }

		/// <summary>
		/// Get status for an upload file
		/// </summary>
		/// <param name="fileId">The id of the file</param>
		/// <returns>Returns a list of FileUploadStatus objects</returns>
		public IList<FileUploadStatus> GetLibraryFileUploadStatus(string fileId)
		{
            if (string.IsNullOrEmpty(fileId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, string.Format(Settings.Endpoints.Default.MyLibraryFileUploadStatus, fileId));
            RawApiResponse response = RestClient.Get(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey);
            try
            {
                var lists = response.Get<IList<FileUploadStatus>>();
                return lists;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
		}

		/// <summary>
		/// Move files to a different folder
		/// </summary>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="fileIds">List of file ids</param>
		/// <returns>Returns a list of FileMoveResult objects.</returns>
		public IList<FileMoveResult> MoveLibraryFile(string folderId, IList<string> fileIds)
		{
            if (string.IsNullOrEmpty(folderId))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            if (fileIds == null || fileIds.Count.Equals(0))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.MyLibraryOrId);
            }

            string url = String.Concat(Settings.Endpoints.Default.BaseUrl, string.Format(Settings.Endpoints.Default.MyLibraryFolderFiles, folderId));
			string json = fileIds.ToJSON();
            RawApiResponse response = RestClient.Put(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, json);
            try
            {
                var movedFiles = response.Get<IList<FileMoveResult>>();
                return movedFiles;
            }
            catch (Exception ex)
            {
                throw new CtctException(ex.Message, ex);
            } 
		}

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
		public string AddLibraryFilesMultipart(string fileName, FileType fileType, string folderId, string description, FileSource source, byte[] data)
		{
            if (string.IsNullOrEmpty(fileName))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileNameNull);
            }

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            string[] fileTypes = new string[5] { ".jpeg", ".jpg", ".gif", ".png", ".pdf" };

            if (!((IList<string>)fileTypes).Contains(extension))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileTypeInvalid);
            }

            if (string.IsNullOrEmpty(folderId) || string.IsNullOrEmpty(description))
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FieldNull);
            }

            if (data == null)
            {
                throw new IllegalArgumentException(CTCT.Resources.Errors.FileNull);
            }

			string result = null;
			string url = String.Concat(Settings.Endpoints.Default.BaseUrl, Settings.Endpoints.Default.MyLibraryFiles);
			byte[] content = MultipartBuilder.CreateMultipartContent(fileName, data, null, fileType.ToString(), folderId, description, source.ToString());
            RawApiResponse response = RestClient.PostMultipart(url, UserServiceContext.AccessToken, UserServiceContext.ApiKey, content);

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

        /// <summary>
        /// Get all existing MyLibrary folders
        /// </summary>
        /// <returns>Returns a collection of MyLibraryFolder objects.</returns>
        public ResultSet<MyLibraryFolder> GetLibraryFolders()
        {
            return GetLibraryFolders(null, null, null);
        }

        /// <summary>
        /// Update name and parent_id for a specific folder
        /// </summary>
        /// <param name="folder">Folder to be added (with name and parent id)</param>
        /// <returns>Returns a MyLibraryFolder object.</returns>
        public MyLibraryFolder UpdateLibraryFolder(MyLibraryFolder folder)
        {
            return UpdateLibraryFolder(folder, null);
        }

        /// <summary>
        /// Get files from Trash folder
        /// </summary>
        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
        public ResultSet<MyLibraryFile> GetLibraryTrashFiles()
        {
            return GetLibraryTrashFiles(null, null, null, null);
        }

        /// <summary>
        /// Get files
        /// </summary>
        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
        public ResultSet<MyLibraryFile> GetLibraryFiles()
        {
            return GetLibraryFiles(null, null, null, null);
        }

        /// <summary>
        /// Get files from a specific folder
        /// </summary>
        /// <param name="folderId">The id of the folder from which to retrieve files</param>
        /// <returns>Returns a collection of MyLibraryFile objects.</returns>
        public ResultSet<MyLibraryFile> GetLibraryFilesByFolder(string folderId)
        {
            return GetLibraryFilesByFolder(folderId, null, null);
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
