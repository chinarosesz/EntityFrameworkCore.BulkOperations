using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkCore.BulkOperations.Tests
{
    [TestClass]
    public class BulkOperationsTests
    {
        [TestMethod]
        public void Insert_Update_Delete_50K_Records()
        {
            ILogger logger = Helper.RedirectLoggerToConsole();
            Stopwatch stopWatch = new Stopwatch();

            // Create 50K employee records
            List<EmployeeEntity> entities = new List<EmployeeEntity>();
            for (int i = 0; i < 50000; i++)
            {
                string employeeId = $"employee{i}";

                EmployeeEntity employeeEntity = new EmployeeEntity
                {
                    EmailAddress = "aaaaaaa@hotmail.com",
                    HiredDate = DateTime.Now,
                    Address = "5555 1st NE, Redmond WA 98052",
                    First = "Bob",
                    Last = "Jones",
                    Gender = "Male",
                    EmployeeId = employeeId,
                    Organization = "Technology",
                    Team = "VisionCore",
                    UniqueName = "bobjones",
                };
                entities.Add(employeeEntity);
            }

            // Insert data
            logger.LogInformation("Inserting data...");
            stopWatch.Start();
            using EmployeeDbContext context = new EmployeeDbContext(null, logger);
            using IDbContextTransaction insertTransaction = context.Database.BeginTransaction();
            int insertResult = context.BulkInsert(entities);
            insertTransaction.Commit();
            stopWatch.Stop();
            logger.LogInformation($"Operation took {stopWatch.Elapsed.TotalSeconds} seconds");
            logger.LogInformation($"Succesfully inserted {insertResult} employee records");

            // Update data
            logger.LogInformation("Updating data...");
            stopWatch.Restart();
            using IDbContextTransaction updateTransaction = context.Database.BeginTransaction();
            int updateResult = context.BulkUpdate(entities);
            updateTransaction.Commit();
            stopWatch.Stop();
            logger.LogInformation($"Operation took {stopWatch.Elapsed.TotalSeconds} seconds");
            logger.LogInformation($"Succesfully Updated {updateResult} employee records");

            // Deleting data
            logger.LogInformation("Deleting data...");
            stopWatch.Restart();
            using IDbContextTransaction deleteTransaction = context.Database.BeginTransaction();
            int deletedResult = context.BulkDelete(context.EmployeeEntities);
            deleteTransaction.Commit();
            stopWatch.Stop();
            logger.LogInformation($"Operation took {stopWatch.Elapsed.TotalSeconds} seconds");
            logger.LogInformation($"Succesfully insert or updated {deletedResult} employee records");
        }
    }
}
