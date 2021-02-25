using AutoMapper;
using DbModels;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelfHelperRE.Controllers
{
    public class TargetController : Controller
    {
        TargetService<TargetCatch> targetService;

        public TargetController(ITarget<Target> service, IMapper targetMapper)
        {
            targetService = new TargetService<TargetCatch>(service, targetMapper);
        }

        [HttpPost]
        public async Task<List<TargetCatch>> LoadTargets([FromBody] TargetCatch targetCatch)
        {       
            return await targetService.LoadTargets(User.Identity.Name, targetCatch);
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
            if (targetCatch.Text != "" && targetCatch.Text != null && targetCatch.DateTimeFirst != "" && targetCatch.DateTimeFirst != null && targetCatch.DateTimeSecond != "" && targetCatch.DateTimeSecond != null)
            {
                await targetService.AddTarget(User.Identity.Name, targetCatch);
            }
        }

        [HttpPost]
        public async Task EditTarget([FromBody] TargetCatch targetCatch)
        {
            if (targetCatch.Text != "" && targetCatch.Text != null && targetCatch.DateTimeFirst != "" && targetCatch.DateTimeFirst != null && targetCatch.DateTimeSecond != "" && targetCatch.DateTimeSecond != null && targetCatch.Id != "" && targetCatch.Id != null)
            {
                await targetService.EditTarget(targetCatch);
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
