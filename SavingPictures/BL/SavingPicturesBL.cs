using DL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SavingPicturesBL : ISavingPicturesBL
    {
        private readonly ISavingPicturesDL _savingPicturesDL;
        public SavingPicturesBL(ISavingPicturesDL savingPicturesDL)
        {
            _savingPicturesDL = savingPicturesDL;
        }

        public async Task<CollectionInfo> getCollectionName(string collectionId)
        {
            return await _savingPicturesDL.getCollectionName(collectionId);
        }

        public async Task<NewPicture[]> addNewPicture(NewPicture[] newPicture)
        {
            return await _savingPicturesDL.addNewPicture(newPicture);
        }

    }
}
