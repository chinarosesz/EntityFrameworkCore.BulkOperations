using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace EntityFrameworkCore.BulkOperations.Tests
{
    [TestClass]
    public class BulkInsertTests
    {
        [TestMethod]
        public void Insert_50K_Records_With_Large_Data()
        {
            ILogger logger = Helper.RedirectLoggerToConsole();
            List<EmployeeWithDataEntity> entities = new List<EmployeeWithDataEntity>();

            StringBuilder largeText = new StringBuilder(25000);
            for (int i = 0; i < 25000; i++)
            {
                largeText.Append("a");
            };
            string largeTextString = largeText.ToString();

            for (int i = 0; i < 50000; i++)
            {
                EmployeeWithDataEntity employeeEntity = new EmployeeWithDataEntity
                {
                    EmailAddress = "aaaaaaa@hotmail.com",
                    HiredDate = DateTime.Now,
                    Address = "5555 1st NE, Redmond WA 98052",
                    First = "Bob",
                    Last = "Jones",
                    Gender = "Male",
                    EmployeeId = $"employee{i}",
                    Organization = "Technology",
                    Team = "VisionCore",
                    UniqueName = "bobjones",
                    Data = largeTextString,
                };
                entities.Add(employeeEntity);
            }

            logger.LogInformation("Ingesting data...");
            using EmployeeDbContext context = new EmployeeDbContext(null, logger);
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            int insertedResult = context.BulkInsertOrUpdate(entities);
            transaction.Commit();
            logger.LogInformation($"Succesfully inserted {insertedResult} records");

            logger.LogInformation("Ingesting data...");
            using IDbContextTransaction transaction2 = context.Database.BeginTransaction();
            int insertedResult2 = context.BulkInsertOrUpdate(entities);
            transaction2.Commit();
            logger.LogInformation($"Succesfully inserted {insertedResult2} records");
        }

        [TestMethod]
        public void Insert_50K_Records_With_Large_Compressed_Data()
        {
            ILogger logger = Helper.RedirectLoggerToConsole();
            List<EmployeeWithCompressedDataEntity> entities = new List<EmployeeWithCompressedDataEntity>();

            StringBuilder largeText = new StringBuilder(25000);
            for (int i = 0; i < 25000; i++)
            {
                largeText.Append("a");
            };
            byte[] compressedData = this.Compress(largeText.ToString());

            for (int i = 0; i < 50000; i++)
            {
                EmployeeWithCompressedDataEntity employeeEntity = new EmployeeWithCompressedDataEntity
                {
                    EmailAddress = "aaaaaaa@hotmail.com",
                    HiredDate = DateTime.Now,
                    Address = "5555 1st NE, Redmond WA 98052",
                    First = "Bob",
                    Last = "Jones",
                    Gender = "Male",
                    EmployeeId = $"employee{i}",
                    Organization = "Technology",
                    Team = "VisionCore",
                    UniqueName = "bobjones",
                    Data = compressedData,
                };
                entities.Add(employeeEntity);
            }

            logger.LogInformation("Ingesting data...");
            using EmployeeDbContext context = new EmployeeDbContext(null, logger);
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            int insertedResult = context.BulkInsertOrUpdate(entities);
            transaction.Commit();
            logger.LogInformation($"Succesfully inserted {insertedResult} records");

            logger.LogInformation("Ingesting data...");
            using IDbContextTransaction transaction2 = context.Database.BeginTransaction();
            int insertedResult2 = context.BulkInsertOrUpdate(entities);
            transaction2.Commit();
            logger.LogInformation($"Succesfully inserted {insertedResult2} records");
        }
      
        private byte[] Compress(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            using MemoryStream msi = new MemoryStream(bytes);
            using MemoryStream mso = new MemoryStream();
            using (GZipStream gs = new GZipStream(mso, CompressionMode.Compress))
            {
                msi.CopyTo(gs);
            }
            return mso.ToArray();
        }
    }
}
