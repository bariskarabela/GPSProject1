using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.Constants
{
    public static class ImageConstants
    {
        public static string criminalImagesLimitExceded = "Bir suç kaydının maksimum 5 adet fotoğrafı olabilir.";
        public static string criminalImagesNotFound = "Suça ait bir fotoğraf bulunamadı";
        public static string criminalImagesAdded = "Suç fotoğrafı başarıyla sisteme yüklendi";
        public static string criminalImagesUpdated = "Suç fotoğrafı başarı ile güncellendi";
        public static string criminalImagesDeleted = "Suç fotoğrafı başarı ile silindi";
        public static string criminalImagesGettedBycriminalId = "Suça ait olan fotoğraflar başarıyla getirildi";
        public static string criminalImagesGettedById = "Suç fotoğrafı id ile getirildi";
        public static string AllcriminalImagesGetted = "Suç fotoğrafları getirildi";
    }
}
