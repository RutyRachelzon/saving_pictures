using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NewPicture
    {

        public string CollectionSymbolizationID { get; set; }

        public string CollectionTitle { get; set; }


        public string PictureName { get; set; }

        public string? PictureUrl { get; set; }

        public string? backName { get; set; }

        public string? backUrl { get; set; }

        public string RecordsContent
        {
            get
            {
                // Using string.Join to concatenate non-null properties with a separator
                string pictureInfo = $"collectionID: {CollectionSymbolizationID},\n title: {CollectionTitle},\n picture name: {PictureName},\n picture url: {PictureUrl}";

                // Concatenate backName and backUrl only if they are not null
                if (backName != null)
                {
                    pictureInfo += $",\n back name: {backName}";
                }

                if (backUrl != null)
                {
                    pictureInfo += $",\n back url: {backUrl}";
                }

                return pictureInfo;
            }
        }

        //public string RecordsContent => $"collectionID: {CollectionSymbolizationID},\n title: {CollectionTitle},\n picture name:{PictureName},\n picture url:{PictureUrl}";
        public string RecordsFileName => $"{CollectionSymbolizationID}_{PictureName}_records.txt"; // Adjust the file extension as neede


    }
}
