using BussinesLayer;
using DatabaseLayer.ExFit_Database;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExFit.Controllers
{
    public class MembersController : ExFitControllerBase
    {
        UserManager userManager = new UserManager();
        MemberManager memberManager = new MemberManager();
        TaskManager taskManager = new TaskManager();
        DietManager dietManager = new DietManager();
        Excersize_Manager excersizeManager = new Excersize_Manager();
        private MembersViewModel ViewModel(int last, int passive, int id = 0)
        {
            MembersViewModel viewModelMembers = new MembersViewModel();
            if (id != 0)
            {
                viewModelMembers.Member = (ObjectLayer.ObjMember)memberManager.GetMember(id);
                viewModelMembers.MemberMeazurements = memberManager.GetMemberMeazurements(id);
                viewModelMembers.MemberWeightArray = memberManager.GetMemberWeightsArray(id);
                viewModelMembers.MemberDiet = dietManager.GetDiets(viewModelMembers.Member.Diet_ID, true)[0]; 
                viewModelMembers.MemberExcersize = excersizeManager.GetExcersizes(viewModelMembers.Member.Excersize_ID, true)[0]; 
            }
            else
            {
                viewModelMembers.Members = memberManager.GetMembers(last, passive);
                viewModelMembers.Member = new ObjectLayer.ObjMember();
            }
            viewModelMembers.Tasks = taskManager.GetLastFiveTask();
            viewModelMembers.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            return viewModelMembers;
        }
        public async Task<IActionResult> SaveMemberAsync(MembersViewModel Model)
        {
            if (Model.Member.FileAvatarIMG != null)
            {
                string imageExtension = Path.GetExtension(Model.Member.FileAvatarIMG.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/ProfilePhotos/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await Model.Member.FileAvatarIMG.CopyToAsync(stream);
                Model.Member.IMG = $"/Member/ProfilePhotos/{imageName}";
            }
            else if (Model.Member.IMG == null) { Model.Member.IMG = "/Member/ProfilePhotos/AvatarNull.png"; }

            if (Model.Member.FileHealthReport != null)
            {
                string imageExtension = Path.GetExtension(Model.Member.FileHealthReport.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/HealthReports/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await Model.Member.FileHealthReport.CopyToAsync(stream);
                Model.Member.Health_Report = $"/Member/HealthReports/{imageName}";
            }
            else if (Model.Member.Health_Report == null) { Model.Member.Health_Report = "/Member/HealthReports/AvatarNull.png"; }

            if (Model.Member.FileIdentityCard != null)
            {
                string imageExtension = Path.GetExtension(Model.Member.FileIdentityCard.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/IdentityCards/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await Model.Member.FileIdentityCard.CopyToAsync(stream);
                Model.Member.Identity_Card = $"/Member/IdentityCards/{imageName}";
            }
            else if (Model.Member.Identity_Card == null) { Model.Member.Identity_Card = "/Member/ProfilePhotos/AvatarNull.png"; }

            memberManager.SaveMember(Model.Member);

            if (Model.Member.Member_ID == 0)
            {
                TaskBuilder(0, Model.Member.Member_ID);            
            }
            TaskBuilder(1, Model.Member.Member_ID);
            Model.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            return RedirectToAction("AllMembers", "Members", Model);
        }
        public IActionResult SaveMemberMeazurements(MembersViewModel Model)
        {
            Model.MemberMeazurement.Member_ID = Model.Member.Member_ID;
            memberManager.SaveMemberMeazurements(Model.MemberMeazurement);
            return RedirectToAction("MemberAddMeazurements", "Members", new { id = Model.Member.Member_ID });
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
            memberManager.DeleteMemberMeazurements(id);
            return RedirectToAction("MemberAddMeazurements", "Members", new { id = member_id });
        }
        public IActionResult PassiveMember(int id)
        {
            memberManager.DeleteMember(id);
            TaskBuilder(2, id);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMember(int id)
        {
            memberManager.DeleteMember(id, true);
            TaskBuilder(7, 0);
            return RedirectToAction("AllPassivedMembers", "Members");
        }
        public IActionResult ActiveMember(int id)
        {
            memberManager.ActiveMember(id);
            TaskBuilder(3, id);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMemberExcersize(int id)
        {
            excersizeManager.DeleteExcersize(id, true);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMemberDiet(int id)
        {
            dietManager.DeleteDiet(id, true);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
    }
}
