using Google.Apis.Drive.v3;
using System;

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
    }
}