using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;

namespace Hugger_Application.Models.GoogleDriveAPI
{
    public class FolderManager
    {
        public static void CreateFolder(string folderName, DriveService service)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            Console.WriteLine("Folder ID: " + file.Id);
        }

        public static void DeleteFolder(string folderName, DriveService service)
        {
            var request = service.Files.Delete(folderName);

            request.Execute();
        }

        public static string ReturnFolderIdByName(DriveService service, string folderName)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' and name = '{folderName}'";
            listRequest.Fields = "nextPageToken, files(id, name)";

            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            string folderID = null;

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    folderID = file.Id;
                }
                Console.WriteLine($"Folder: {folderName} has ID: {folderID}");
            } else
                Console.WriteLine($"Folder {folderName} not found!");
            
            return folderID;
        }
    }
}