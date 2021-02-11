using Microsoft.AspNetCore.Mvc;
using SelfHelperRE.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelfHelperRE.Controllers
{
    public class TargetController : Controller
    {
        TargetService<TargetCatch> targetService;
        TargetService<TargetData> targetDataService;

        public TargetController()
        {
            targetService = new TargetService<TargetCatch>();
            targetDataService = new TargetService<TargetData>();
        }

        [HttpPost]
        public async Task<List<TargetData>> LoadTargets([FromBody] TargetCatch targetCatch)
        {
            TargetData targetData = new TargetData();

            targetData.Status = targetCatch.Status;
            
            return await targetDataService.LoadTargets(User.Identity.Name, targetData);
        }

        [HttpPost]
        public async Task ChangeStatus([FromBody] TargetCatch targetCatch)
        {
            if (targetCatch.Id != "" && targetCatch.Id != null && targetCatch.Status != "" && targetCatch.Status != null)
            {
                await targetService.ChangeStatus(targetCatch);
            }
        }

        public async Task CheckStatus()
        {
            await targetService.CheckTimeStatus();
        }

        [HttpPost]
        public async Task AddTarget([FromBody] TargetCatch targetCatch)
        {
            if (targetCatch.Text != "" && targetCatch.Text != null && targetCatch.DateTimeStart != "" && targetCatch.DateTimeStart != null && targetCatch.DateTimeEnd != "" && targetCatch.DateTimeEnd != null)
            {
                TargetData targetData = new TargetData();

                targetData.Status = targetCatch.Status;
                targetData.DateTimeFirst = Convert.ToDateTime(targetCatch.DateTimeStart);
                targetData.DateTimeSecond = Convert.ToDateTime(targetCatch.DateTimeEnd);
                targetData.Text = targetCatch.Text;

                await targetDataService.AddTarget(User.Identity.Name, targetData);
            }
        }

        [HttpPost]
        public async Task EditTarget([FromBody] TargetCatch targetCatch)
        {
            if (targetCatch.Text != "" && targetCatch.Text != null && targetCatch.DateTimeStart != "" && targetCatch.DateTimeStart != null && targetCatch.DateTimeEnd != "" && targetCatch.DateTimeEnd != null && targetCatch.Id != "" && targetCatch.Id != null)
            {
                TargetData targetData = new TargetData();

                targetData.Id = Convert.ToInt32(targetCatch.Id);
                targetData.Status = targetCatch.Status;
                targetData.DateTimeFirst = Convert.ToDateTime(targetCatch.DateTimeStart);
                targetData.DateTimeSecond = Convert.ToDateTime(targetCatch.DateTimeEnd);
                targetData.Text = targetCatch.Text;

                await targetDataService.EditTarget(targetData);
            }
        }

        [HttpPost]
        public async Task DeleteTarget([FromBody] TargetCatch targetCatch)
        {
            if (targetCatch.Id != "" && targetCatch.Id != null)
            {

                await targetService.DeleteTarget(targetCatch);
            }
        }
    }
}
