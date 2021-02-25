using AutoMapper;
using Mapping;
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

        WorkingWithTargetForTest workingWithTargetForTest = new WorkingWithTargetForTest();

        [SetUp]
        public void Setup()
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingTarget<TargetCatch>());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            targetService = new TargetService<TargetCatch>(workingWithTargetForTest, mapper);
        }

        [Test]
        [TestCase("Все")]
        [TestCase("Completed")]
        [TestCase("Failed")]
        [TestCase("Performed")]
        public async Task Load_Targets_Test(string status)
        {
            string login = "zoldik";

            TargetCatch targetData = new TargetCatch() {Status = status };

            List<TargetCatch> result = await targetService.LoadTargets(login, targetData);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("0", "Failed")]
        [TestCase("3", "Completed")]
        [TestCase("0", "Performed")]
        public async Task Change_Status_Test(string id, string status)
        {
            TargetCatch targetData = new TargetCatch() { Id = id, Status = status };

            Assert.AreEqual(1, await targetService.ChangeStatus(targetData));
        }

        [Test]
        public async Task Check_Time_Status_Test()
        {
            Assert.AreEqual(1, await targetService.CheckTimeStatus());
        }

        [Test]
        public async Task Add_Target_Test()
        {
            TargetCatch targetData = new TargetCatch() { Text = "Text from test", DateTimeFirst = "01.01.2021 22:40:00", DateTimeSecond = "03.01.2021 00:00:00", Status = "Performed" };

            string login = "zoldik";

            Assert.AreEqual(1, await targetService.AddTarget(login, targetData));
        }   
        
        [Test]
        public async Task Edit_Target_Test()
        {
            TargetCatch targetData = new TargetCatch()
            {
                Id = "0",
                Text = "Text from test for edit",
                DateTimeFirst = "01.01.2021 22:40:00",
                DateTimeSecond = "03.01.2021 00:00:00",
                Status = "Performed"
            };

            Assert.AreEqual(1, await targetService.EditTarget(targetData));
        }       
        
        [Test]
        public async Task Delete_Target_Test()
        {
            TargetCatch targetData = new TargetCatch()
            {
                Id = "1"
            };

            Assert.AreEqual(1, await targetService.DeleteTarget(targetData));
        }
    }
}
