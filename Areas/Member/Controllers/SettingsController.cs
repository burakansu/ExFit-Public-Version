using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using ExFit.Models;
using System.Threading.Tasks;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class SettingsController : _MemberControllerBase
    {
        public _HomeViewModel ViewModel()
        {
            _HomeViewModel VM = new _HomeViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager().GetMember(id);
            VM.Company = new CompanyManager().GetCompany(VM._Member.Company_ID);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
        public async Task<IActionResult> SaveMemberAsync(_HomeViewModel VM)
        {
            if (VM._Member.FileAvatarIMG != null)
            {
                string imageExtension = Path.GetExtension(VM._Member.FileAvatarIMG.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/ProfilePhotos/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await VM._Member.FileAvatarIMG.CopyToAsync(stream);
                VM._Member.IMG = $"/Member/ProfilePhotos/{imageName}";
            }
            else if (VM._Member.IMG == null) { VM._Member.IMG = "/Member/ProfilePhotos/AvatarNull.png"; }

            if (VM._Member.FileHealthReport != null)
            {
                string imageExtension = Path.GetExtension(VM._Member.FileHealthReport.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/HealthReports/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await VM._Member.FileHealthReport.CopyToAsync(stream);
                VM._Member.Health_Report = $"/Member/HealthReports/{imageName}";
            }
            else if (VM._Member.Health_Report == null) { VM._Member.Health_Report = "/Member/HealthReports/AvatarNull.png"; }

            if (VM._Member.FileIdentityCard != null)
            {
                string imageExtension = Path.GetExtension(VM._Member.FileIdentityCard.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/IdentityCards/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await VM._Member.FileIdentityCard.CopyToAsync(stream);
                VM._Member.Identity_Card = $"/Member/IdentityCards/{imageName}";
            }
            else if (VM._Member.Identity_Card == null) { VM._Member.Identity_Card = "/Member/ProfilePhotos/AvatarNull.png"; }

            new MemberManager().SaveMember(VM._Member);

            return Redirect("/Member/Home/Index");
        }
    }
}
