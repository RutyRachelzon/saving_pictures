using Entities;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;




namespace DL
{
    public class SavingPicturesDL : ISavingPicturesDL
    {
        public async Task<CollectionInfo> getCollectionName(string collectionId)
        {
            try
            {
                string filePath= Path.Combine(Directory.GetCurrentDirectory(), "..",collectionId+".json" );
                // Read the entire file content
                string jsonContent = File.ReadAllText(filePath, Encoding.UTF8);

                // Deserialize JSON content into your object
                if (!string.IsNullOrEmpty(jsonContent))
                {
                    CollectionInfo collectionInfo = JsonSerializer.Deserialize<CollectionInfo>(jsonContent);
                    return collectionInfo;
                }
                else
                {
                    Console.WriteLine("File is empty.");
                    return null;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            };

        }


        //public async Task<string> addNewPicture(NewPicture[] newPictures)
        //{
        //    foreach (var newPicture in newPictures)
        //    {

        //        try
        //        {
        //            using (var image = new Image<Rgba32>(800, 600))
        //            {
        //                string folderPath = $"C:\\Users\\Rach\\Desktop\\task_ruty\\SavingPictures\\images\\{newPicture.CollectionSymbolizationID}";
        //                if (!Directory.Exists(folderPath))
        //                {
        //                    Directory.CreateDirectory(folderPath);
        //                }
        //                string RecordsfolderPath = $"C:\\Users\\Rach\\Desktop\\task_ruty\\SavingPictures\\records\\{newPicture.CollectionSymbolizationID}";
        //                string imagePath = $"{folderPath}\\{newPicture.PictureName}.jpg";
        //                image.Save(imagePath, new JpegEncoder());
        //                await UpdateRecordsFile(newPicture, imagePath);

        //            }
        //            var collectionInfo = await GetCollectionInfo(newPicture.CollectionSymbolizationID);
        //            collectionInfo.NumOfPictures++;
        //            await SaveCollectionInfo(collectionInfo);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"An error occurred while saving the picture: {ex.Message}");
        //        }
        //    }
        //    return "picture and files added succsessfully";
        //}

        public async Task<NewPicture[]> addNewPicture(NewPicture[] newPictures)
        {
            foreach (var newPicture in newPictures)
            {
                try
                {
                    await savePicture(newPicture);
                    var collectionInfo = await getCollectionInfo(newPicture.CollectionSymbolizationID);
                    collectionInfo.NumOfPictures++;
                    await saveCollectionInfo(collectionInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while saving the picture: {ex.Message}");
                }
            }
            return newPictures;
        }

        public async Task savePicture(NewPicture newPicture)
        {
            using (var image = new Image<Rgba32>(800, 600))
            {
                string folderPath = getFolderPath(newPicture.CollectionSymbolizationID, "images");
                string imagePath = $"{folderPath}\\{newPicture.PictureName}.jpg";
                image.Save(imagePath, new JpegEncoder());
                if (newPicture.backName != null)
                {
                    string backImagePath = $"{folderPath}\\{newPicture.backName}.jpg";
                    image.Save(backImagePath, new JpegEncoder());
                }
                await updateRecordsFile(newPicture, imagePath);
            }
        }

        public string getFolderPath(string collectionSymbolization, string subfolder)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", subfolder, collectionSymbolization);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }

        public async Task updateRecordsFile(NewPicture newPicture, string pictureUrl)
        {
            string recordsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "records");
            if (!Directory.Exists(recordsFolderPath))
            {
                Directory.CreateDirectory(recordsFolderPath);
            }
            newPicture.PictureUrl = pictureUrl;
            string recordsFilePath = Path.Combine(recordsFolderPath, newPicture.RecordsFileName);

            // Append or create the records file and write the content
            var encoding = new UTF8Encoding(false);
            await File.AppendAllTextAsync(recordsFilePath, $"{newPicture.RecordsContent}{Environment.NewLine}", encoding);
        }

        public async Task<CollectionInfo> getCollectionInfo(string collectionSymbolization)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", $"{collectionSymbolization}.json");
            string jsonContent = await File.ReadAllTextAsync(filePath);
            var collectionInfo = JsonSerializer.Deserialize<CollectionInfo>(jsonContent);
            return collectionInfo;
        }

        public async Task saveCollectionInfo(CollectionInfo collectionInfo)
        {
            string jsonContent = JsonSerializer.Serialize(collectionInfo);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", $"{collectionInfo.CollectionSymbolization}.json");
            await File.WriteAllTextAsync(filePath, jsonContent);
        }

    }
}

