using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class CriminalConstants
    {
        public static string criminalAdded = "Suç başarıyla eklendi.";
        public static string criminalDeleted = "Suç başarıyla silindi.";
        public static string criminalUpdated = "Suç bilgileri başarıyla güncellendi.";

        public static string criminalNotAdded= "Suç eklenemedi.";
        public static string criminalNotDeleted = "Suç silinemedi.";
        public static string criminalNotUpdated = "Suç güncellenemedi.";
        public static string criminalMoveUpdated = "Suç asıl listeye eklendi.";

        public static string AllcriminalGetted = "Tüm Suç bilgileri getirildi.";
        public static string AllcriminalNotGetted = "Suçların bilgileri getirilemedi.";

        public static string criminalGettedById = "Suç getirildi";
        public static string criminalNotGettedById = "Suç getirelemedi.";

        public static string excelCriminalNotAdded = "Excel formatını kontrol ediniz.";


    }
}
