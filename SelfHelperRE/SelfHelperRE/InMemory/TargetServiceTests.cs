using NUnit.Framework;
using RepositoryForTests;
using SelfHelperRE;
using SelfHelperRE.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InMemory
{
    [TestFixture]
    public class TargetServiceTests
    {
        TargetService<TargetCatch> targetService;
        TargetService<TargetData> targetDataService;

        WorkingWithTargetForTest workingWithTargetForTest = new WorkingWithTargetForTest();

        [SetUp]
        public void Setup()
        {
            targetService = new TargetService<TargetCatch>(workingWithTargetForTest);
            targetDataService = new TargetService<TargetData>(workingWithTargetForTest);
        }

        [Test]
        [TestCase("Все")]
        [TestCase("Completed")]
        [TestCase("Failed")]
        [TestCase("Performed")]
        public async Task Load_Targets_Test(string status)
        {
            string login = "zoldik";

            TargetData targetData = new TargetData() {Status = status };

            List<TargetData> result = await targetDataService.LoadTargets(login, targetData);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase(0, "Failed")]
        [TestCase(3, "Completed")]
        [TestCase(0, "Performed")]
        public async Task Change_Status_Test(int id, string status)
        {

            TargetData targetData = new TargetData() { Id = id, Status = status };

            await targetDataService.ChangeStatus(targetData);

            Assert.IsTrue(targetDataService.CheckStatus(targetData));
        }

        [Test]
        public async Task Check_Time_Status_Test()
        {
            await targetService.CheckTimeStatus();

            Assert.IsFalse(targetService.CheckPerformedStatus());
        }

        [Test]
        public async Task Add_Target_Test()
        {
            TargetData targetData = new TargetData() { Text = "Text from test", DateTimeFirst = Convert.ToDateTime("01.01.2021 22:40:00"), DateTimeSecond = Convert.ToDateTime("03.01.2021 00:00:00"), Status = "Performed" };

            string login = "zoldik";

            await targetDataService.AddTarget(login, targetData);

            Assert.IsTrue(targetDataService.CheckAddTarget(targetData, login));
        }   
        
        [Test]
        public async Task Edit_Target_Test()
        {
            TargetData targetData = new TargetData()
            {
                Id = 0,
                Text = "Text from test for edit",
                DateTimeFirst = Convert.ToDateTime("01.01.2021 22:40:00"),
                DateTimeSecond = Convert.ToDateTime("03.01.2021 00:00:00"),
                Status = "Performed"
            };

            await targetDataService.EditTarget(targetData);

            Assert.IsTrue(targetDataService.CheckTarget(targetData));
        }       
        
        [Test]
        public async Task Delete_Target_Test()
        {
            TargetData targetData = new TargetData()
            {
                Id = 1
            };

            await targetDataService.DeleteTarget(targetData);

            Assert.IsFalse(targetDataService.CheckTarget(targetData));
        }
    }
}
