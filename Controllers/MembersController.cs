using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ExFit.Controllers
{
    public class MembersController : MemberControllerBase
    {
        private MembersViewModel ViewModel(int last, int passive, int id = 0)
        {
            MembersViewModel VM = new MembersViewModel();
            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM.Company = new CompanyManager().GetCompany(VM.User.Company_ID);
            VM.ExcersizeArray = new ExcersizeManager().GetExcersizes(VM.Company.Company_ID);
            List<ObjPackage> objPackages = new List<ObjPackage>();
            objPackages.Add(new ObjPackage { Name = "Paket Yok" });
            objPackages.AddRange(new PackageManager().GetPackages(VM.Company.Company_ID));
            VM.PackageArray = objPackages;
            VM.DietArray = new DietManager().GetDiets(VM.Company.Company_ID);
            VM.Tasks = new TaskManager().GetLastFiveTask(VM.Company.Company_ID);
            if (id != 0)
            {
                VM.Member = (ObjectLayer.ObjMember)new MemberManager().GetMember(id);
                VM.MemberMeazurements = new MemberManager().GetMemberMeazurements(id);
                VM.MemberWeightArray = new MemberManager().GetMemberWeightsArray(id);
                if (new DietManager().GetDiets(VM.Company.Company_ID, VM.Member.Diet_ID, true).Count == 0)
                    VM.MemberDiet = new ObjDiet();
                else
                    VM.MemberDiet = new DietManager().GetDiets(VM.Company.Company_ID, VM.Member.Diet_ID, true)[0];
                if (new ExcersizeManager().GetExcersizes(VM.Company.Company_ID, VM.Member.Excersize_ID, true).Count == 0)
                    VM.MemberExcersize = new ObjExcersize();
                else
                    VM.MemberExcersize = new ExcersizeManager().GetExcersizes(VM.Company.Company_ID, VM.Member.Excersize_ID, true)[0];
            }
            else
            {
                VM.Members = new MemberManager().GetMembers(VM.Company.Company_ID,last, passive);
                VM.Member = new ObjectLayer.ObjMember();
            }

            return VM;
        }
        public async Task<IActionResult> SaveMemberAsync(MembersViewModel VM)
        {
            if (VM.Member.FileAvatarIMG != null)
            {
                string imageExtension = Path.GetExtension(VM.Member.FileAvatarIMG.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/ProfilePhotos/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await VM.Member.FileAvatarIMG.CopyToAsync(stream);
                VM.Member.IMG = $"/Member/ProfilePhotos/{imageName}";
            }
            else if (VM.Member.IMG == null) { VM.Member.IMG = "/Member/ProfilePhotos/AvatarNull.png"; }

            if (VM.Member.FileHealthReport != null)
            {
                string imageExtension = Path.GetExtension(VM.Member.FileHealthReport.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/HealthReports/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await VM.Member.FileHealthReport.CopyToAsync(stream);
                VM.Member.Health_Report = $"/Member/HealthReports/{imageName}";
            }
            else if (VM.Member.Health_Report == null) { VM.Member.Health_Report = "/Member/HealthReports/AvatarNull.png"; }

            if (VM.Member.FileIdentityCard != null)
            {
                string imageExtension = Path.GetExtension(VM.Member.FileIdentityCard.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/IdentityCards/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await VM.Member.FileIdentityCard.CopyToAsync(stream);
                VM.Member.Identity_Card = $"/Member/IdentityCards/{imageName}";
            }
            else if (VM.Member.Identity_Card == null) { VM.Member.Identity_Card = "/Member/ProfilePhotos/AvatarNull.png"; }

            int Before = VM.Member.Member_ID;
            int Count = new MemberManager().SaveMember(VM.Member);

            if (Count > 0)
                new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.Member.Company_ID, 8, VM.Member.Member_ID, (int)HttpContext.Session.GetInt32("ID")));
            else if (Before == 0)
                new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.Member.Company_ID, 0, VM.Member.Member_ID, (int)HttpContext.Session.GetInt32("ID")));
            else
                new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.Member.Company_ID, 1, VM.Member.Member_ID, (int)HttpContext.Session.GetInt32("ID")));

            return RedirectToAction("Index", "Home");
        }
        public IActionResult SaveMemberMeazurements(MembersViewModel VM)
        {
            VM.MemberMeazurement.Member_ID = VM.Member.Member_ID;
            new MemberManager().SaveMemberMeazurements(VM.MemberMeazurement);
            return RedirectToAction("MemberAddMeazurements", "Members", new { id = VM.Member.Member_ID });
        }
        public IActionResult AllMembers()
        {
            return View(ViewModel(0, 0));
        }
        public IActionResult AllPassivedMembers()
        {
            return View(ViewModel(0, 1));
        }
        public IActionResult OpenedMember(int id)
        {
            return View(ViewModel(0, 0, id));
        }
        public IActionResult CheckInMember(int id)
        {
            return PartialView(ViewModel(0, 0, id));
        }
        public IActionResult MemberAddMeazurements(int id)
        {
            return View(ViewModel(0, 0, id));
        }
        public IActionResult DeleteMemberMeazurements(int member_id, int id)
        {
            new MemberManager().DeleteMemberMeazurements(id);
            return RedirectToAction("MemberAddMeazurements", "Members", new { id = member_id });
        }
        public IActionResult PassiveMember(int id)
        {
            new MemberManager().DeleteMember(id);
            ObjUser objUser = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));

            new TaskManager().SaveTask(new TaskManager().TaskBuilder(objUser.Company_ID, 2, id, objUser.User_ID));
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMember(int id)
        {
            new MemberManager().DeleteMember(id, true);
            ObjUser objUser = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));

            new TaskManager().SaveTask(new TaskManager().TaskBuilder(objUser.Company_ID, 7, id, objUser.User_ID));
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteMemberExcersize(int id)
        {
            new ExcersizeManager().DeleteExcersize(id, true);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMemberDiet(int id)
        {
            new DietManager().DeleteDiet(id, true);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
    }
}
