using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.Constants
{
    public static class CoordinateImageConstants
    {
        public static string CoordinateImagesLimitExceded = "Bir koordinatın maksimum 5 adet fotoğrafı olabilir.";
        public static string CoordinateImagesNotFound = "Koordinata ait bir fotoğraf bulunamadı";
        public static string CoordinateImagesAdded = "Koordinat fotoğrafı başarıyla sisteme yüklendi";
        public static string CoordinateImagesUpdated = "Koordinat fotoğrafı başarı ile güncellendi";
        public static string CoordinateImagesDeleted = "Koordinat fotoğrafı başarı ile silindi";
        public static string CoordinateImagesGettedByCoordinateId = "Koordinata ait olan fotoğraflar başarıyla getirildi";
        public static string CoordinateImagesGettedById = "Koordinat fotoğrafı id ile getirildi";
        public static string AllCoordinateImagesGetted = "Tüm Koordinat fotoğrafları getirildi";
    }
}
