using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CTCT.Components.MyLibrary;

namespace CTCT.Util
{
	/// <summary>
	/// Multipart Builder class implementation
	/// </summary>
	public class MultipartBuilder
	{
		private const string CRLF = "\r\n";
		private const string SEPARATOR = "--";

		/// <summary>
		/// Boundary used for Multipart activities
		/// </summary>
		public static string MULTIPART_BOUNDARY = ProcessorBase.MULTIPART_BOUNDARY;

		/// <summary>
		/// Create multipart content in binary format
		/// </summary>
		/// <param name="fileName">The name of the file</param>
		/// <param name="fileContent">The content of the file</param>
		/// <param name="lists">List of contact list Ids to add/remove contacts to</param>
		/// <param name="fileType">The type of the file</param>
		/// <param name="folderId">The id of the folder</param>
		/// <param name="description">The file description</param>
		/// <param name="source">The file source</param>
		/// <returns>Returns a byte array used for request</returns>
		public static byte[] CreateMultipartContent(string fileName, byte[] fileContent,  IList<string> lists = null, string fileType = null, string folderId = null, string description = null, string source = null)
		{
			var encoding = Encoding.UTF8;

		    using(MemoryStream ms = new MemoryStream())
		    {
		        using(BinaryWriter writer = new BinaryWriter(ms))
		        {
		            //Write the filename
					writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + CRLF));
		            writer.Write(encoding.GetBytes("Content-Disposition: form-data; name=\"file_name\"" + CRLF));
		            writer.Write(encoding.GetBytes("Content-Type: text/plain" + CRLF + CRLF));
		            writer.Write(encoding.GetBytes(fileName + CRLF));

		            //Write lists info
					if(lists != null)
					{
						writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + CRLF));
						writer.Write(encoding.GetBytes("Content-Disposition: form-data; name=\"lists\"" + CRLF));
						writer.Write(encoding.GetBytes("Content-Type: text/plain" + CRLF + CRLF));
						writer.Write(encoding.GetBytes(lists[0]));
						lists.RemoveAt(0);
						foreach(string list in lists)
						{			
							writer.Write(encoding.GetBytes(list));
							writer.Write(encoding.GetBytes(","));
						}
						writer.Write(encoding.GetBytes(CRLF));
					}

					//Write the file type
					if(!string.IsNullOrEmpty(fileType))
					{
						writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + CRLF));
						writer.Write(encoding.GetBytes("Content-Disposition: form-data; name=\"file_type\"" + CRLF));
						writer.Write(encoding.GetBytes("Content-Type: text/plain" + CRLF + CRLF));
						writer.Write(encoding.GetBytes(fileType + CRLF));
					}

					//Write the folder id
					if(!string.IsNullOrEmpty(folderId))
					{					
						writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + CRLF));
						writer.Write(encoding.GetBytes("Content-Disposition: form-data; name=\"folder_id\"" + CRLF));
						writer.Write(encoding.GetBytes("Content-Type: text/plain" + CRLF + CRLF));
						writer.Write(encoding.GetBytes(folderId + CRLF));
					}

					 //Write the description
					if(!string.IsNullOrEmpty(description))
					{				
						writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + CRLF));
						writer.Write(encoding.GetBytes("Content-Disposition: form-data; name=\"description\"" + CRLF));
						writer.Write(encoding.GetBytes("Content-Type: text/plain" + CRLF + CRLF));
						writer.Write(encoding.GetBytes(description + CRLF));
					}

					//Write the source
					if(!string.IsNullOrEmpty(source))
					{					
						writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + CRLF));
						writer.Write(encoding.GetBytes("Content-Disposition: form-data; name=\"source\"" + CRLF));
						writer.Write(encoding.GetBytes("Content-Type: text/plain" + CRLF + CRLF));
						writer.Write(encoding.GetBytes(source + CRLF));
					}

		            //Write the file content
		            writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + CRLF));
		            writer.Write(encoding.GetBytes("Content-Disposition: form-data; name=\"data\"" + CRLF));
		            writer.Write(encoding.GetBytes("Content-Type: application/octet-stream" + CRLF));
		            writer.Write(encoding.GetBytes("Content-Transfer-Encoding: binary" + CRLF + CRLF));
		            writer.Write(fileContent, 0, fileContent.Length);
		            writer.Write(encoding.GetBytes(CRLF));
		            writer.Write(encoding.GetBytes(SEPARATOR + MULTIPART_BOUNDARY + SEPARATOR + CRLF));

		            writer.Flush();
		        }

		        return ms.ToArray();
		    }			
		}
	}
}
