using DL;
using Entities;

namespace BL
{
    public interface ISavingPicturesBL
    {
        Task<CollectionInfo> getCollectionName(string collectionId);

        Task<NewPicture[]> addNewPicture(NewPicture[] newPicture);

    }
}