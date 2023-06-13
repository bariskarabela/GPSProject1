using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using ClosedXML.Excel;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.NLog;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Entites.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Features;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Filters;
using Twilio.Base;
using Twilio.TwiML.Messaging;
using Twilio.TwiML.Voice;

namespace Business.Concrete
{
    public class ExcelCriminalManager : IExcelCriminalService
    {
        private IExcelCriminalDal _excelCriminalDal;
        private ICriminalDal _criminalDal;



        public ExcelCriminalManager(IExcelCriminalDal excelCriminalDal, ICriminalDal criminalDal)
        {
            _excelCriminalDal = excelCriminalDal;
            _criminalDal = criminalDal;
        }
        [SecuredOperation("iladmin")]
        public IDataResult<ExcelCriminal> GetById(int id)
        {
            return new SuccessDataResult<ExcelCriminal>(_excelCriminalDal.Get(c => c.Id == id), CriminalConstants.criminalGettedById);
        }


        [SecuredOperation("iladmin")]
        public IResult Add(ExcelCriminal excelCriminal)

        {
            _excelCriminalDal.Add(excelCriminal);

            return new SuccessResult(CriminalConstants.criminalAdded);
        }

        [SecuredOperation("iladmin")]
        public IResult Delete(ExcelCriminal excelCriminal)
        {
            _excelCriminalDal.Delete(excelCriminal);
            return new SuccessResult(CriminalConstants.criminalDeleted);
        }

        [SecuredOperation("iladmin")]
        public IResult Update(ExcelCriminal excelCriminal)
        {

           
            _excelCriminalDal.Update(excelCriminal);
            return new SuccessResult(CriminalConstants.criminalUpdated);
        }
        [SecuredOperation("iladmin")]
        public IResult MoveData(int id)
        {
           
            var value = _excelCriminalDal.Get(c => c.Id == id);
         

            if (value != null)
            {
                var updatedCriminal = new Criminal
                {

                    CreatedDate = value.CreatedDate,
                    Event = value.Event,
                    EventType = value.EventType,
                    Description = value.Description,
                    Country = value.Country,
                    Town = value.Town,
                    District = value.District,
                    Street = value.Street,
                    AddressDescription = value.AddressDescription,
                    POIAddress = value.POIAddress,
                    LocationX = value.LocationX, 
                    LocationY = value.LocationY,
                    CallTime = value.CallTime,
                    CategoryId = 1,
                    UpdatedDate = DateTime.Now,
                    ImagePath  = "",


                };
                
                _criminalDal.Add(updatedCriminal);
                if (updatedCriminal != null )
                {
                    _excelCriminalDal.Delete(value);
                    return new SuccessResult(CriminalConstants.criminalMoveUpdated);
                }
               

            }

            return new ErrorResult("Taşınacak nesne bulunamadı.");
        }

        [SecuredOperation("iladmin")]
        public IDataResult<List<ExcelCriminal>> GetAll()
        {
            return new SuccessDataResult<List<ExcelCriminal>>(_excelCriminalDal.GetAll());
        }

        [SecuredOperation("iladmin")]
        public IResult Add(IFormFile file)
        {
            DataTable dt = new DataTable();


            // excel dosyamızı stream'e çeviriyoruz
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);

                // excel dosyamızı streamden okuyoruz
                using (var workbook = new XLWorkbook(ms))
                {

                    var worksheet = workbook.Worksheet(1); // sayfa 1

                    // sayfada kaç sütun kullanılmış onu buluyoruz ve sütunları DataTable'a ekliyoruz, ilk satırda sütun başlıklarımız var


                    var asd = worksheet.Cell(1, 1);


                    int i, n = worksheet.Columns().Count();
                    for (i = 1; i <= n; i++)
                    {
                        dt.Columns.Add(worksheet.Cell(1, i).Value.ToString());
                    }


                    List<ExcelCriminal> criminals = new List<ExcelCriminal>();
                    // sayfada kaç satır kullanılmış onu buluyoruz ve DataTable'a satırlarımızı ekliyoruz
                    n = worksheet.Rows().Count();


                    // Başlıkların doğrulama için bir dizi oluşturuluyor
                    string[] expectedHeaders = { "Oluşturma Tarihi", "Vaka Tanımı", "Olay Türü", "Açıklama", "İl", "İlçe", "Mahalle", "Cadde/Sokak", "Adres Açıklamsı", "POI Adresi", "Enlem", "Boylam", "Arama Zamanı" };


                    // Başlıkların doğruluğu kontrol ediliyor


                    for (int y = 0; y < expectedHeaders.Length; y++)
                    {
                        var headerValue = worksheet.Cell(1, y + 1).Value.ToString();
                        if (headerValue != expectedHeaders[y])
                        {
                            return new ErrorResult($"Geçersiz Excel dosyası. {y + 1}. sütun başlığı beklenen değerden farklı.");
                        }
                    }



                    // Başlıkların sayısı kontrol ediliyor
                    if (worksheet.Columns().Count() != expectedHeaders.Length)
                    {
                        return new ErrorResult("Geçersiz Excel dosyası. Başlıkların sayısı uyumsuz.");
                    }

                    // Başlıkların doğruluğu kontrol ediliyor
                   

                    for (i = 2; i <= n; i++)
                    {
                        DataRow dr = dt.NewRow();

                        int j, k = worksheet.Columns().Count();
                        for (j = 1; j <= k; j++)
                        {
                            // i= satır index, j=sütun index, closedXML worksheet için indexler 1'den başlıyor, ama datatable için 0'dan başladığı için j-1 diyoruz
                            dr[j - 1] = worksheet.Cell(i, j).Value;
                        }

                        dt.Rows.Add(dr);

                        var ExcelCriminal = new ExcelCriminal
                        {
                            CreatedDate = DateTime.Parse(dr[0].ToString()),
                            Event = dr[1].ToString(),
                            EventType = dr[2].ToString(),
                            Description = dr[3].ToString(),
                            Country = dr[4].ToString(),
                            Town = dr[5].ToString(),
                            District = dr[6].ToString(),
                            Street = dr[7].ToString(),
                            AddressDescription = dr[8].ToString(),
                            POIAddress = dr[9].ToString(),
                            LocationX = dr[10].ToString(),
                            LocationY = dr[11].ToString(),
                            CallTime = DateTime.Parse(dr[12].ToString())
                        };

                       


                        criminals.Add(ExcelCriminal);
                    }

                    _excelCriminalDal.AddRangeItems(criminals);
                }
            }

            return new SuccessResult(CriminalConstants.criminalAdded);
        }


        [SecuredOperation("iladmin")]
        public IDataResult<List<ExcelCriminalPaginationResult>> GetAllPages(int page, int pageSize, string param = null)
        {
            var query = _excelCriminalDal.GetAll();

            if (!string.IsNullOrEmpty(param))
            {
                query = query.Where(p => p.Description.Contains(param)
                    || p.Country.Contains(param)
                    || p.Town.Contains(param)
                    || p.District.Contains(param)
                    || p.Street.Contains(param)
                    || p.AddressDescription.Contains(param)
                    || p.POIAddress.Contains(param)
                    || p.LocationX.Contains(param)
                    || p.LocationY.Contains(param)).ToList();
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var perPage = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var paginationResult = new ExcelCriminalPaginationResult
            {
                ExcelPerPage = perPage,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            var resultList = new List<ExcelCriminalPaginationResult> { paginationResult };

            return new SuccessDataResult<List<ExcelCriminalPaginationResult>>(resultList);
        }

    }
}
