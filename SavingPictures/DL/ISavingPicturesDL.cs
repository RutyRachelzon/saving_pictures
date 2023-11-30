using Entities;

namespace DL
{
    public interface ISavingPicturesDL
    {
        Task<CollectionInfo> getCollectionName(string collectionId);

        Task<NewPicture[]> addNewPicture(NewPicture[] newPictures);

        Task saveCollectionInfo(CollectionInfo collectionInfo);

        Task<CollectionInfo> getCollectionInfo(string collectionSymbolization);


        Task updateRecordsFile(NewPicture newPicture, string pictureUrl);

        string getFolderPath(string collectionSymbolization, string subfolder);

        Task savePicture(NewPicture newPicture);


    }
}