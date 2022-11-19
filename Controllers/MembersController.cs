using BussinesLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ExFit.Controllers
{
    public class MembersController : MemberControllerBase
    {
        private Context context;
        public MembersController(Context _context)
        {
            context = _context;
        }
        private MembersViewModel ViewModel(int last, int passive, int id = 0)
        {
            MembersViewModel VM = new MembersViewModel();
            if (id != 0)
            {
                VM.Member = (ObjectLayer.ObjMember)new MemberManager(context).GetMember(id);
                VM.MemberMeazurements = new MemberManager(context).GetMemberMeazurements(id);
                VM.MemberWeightArray = new MemberManager(context).GetMemberWeightsArray(id);
                VM.MemberDiet = new DietManager(context).GetDiets(VM.Member.Diet_ID, true)[0];
                VM.MemberExcersize = new ExcersizeManager(context).GetExcersizes(VM.Member.Excersize_ID, true)[0];
                VM.ExcersizeArray = new ExcersizeManager(context).GetExcersizes();
                VM.DietArray = new DietManager(context).GetDiets();
            }
            else
            {
                VM.Members = new MemberManager(context).GetMembers(last, passive);
                VM.Member = new ObjectLayer.ObjMember();
            }
            VM.Tasks = new TaskManager(context).GetLastFiveTask();
            VM.User = new UserManager(context).GetUser((int)HttpContext.Session.GetInt32("ID"));
            return VM;
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

            new MemberManager(context).SaveMember(Model.Member);

            if (Model.Member.Member_ID == 0)
            {
                new TaskManager(context).SaveTask(TaskBuilder(0, Model.Member.Member_ID));
            }
            new TaskManager(context).SaveTask(TaskBuilder(1, Model.Member.Member_ID));
            Model.User = new UserManager(context).GetUser((int)HttpContext.Session.GetInt32("ID"));
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SaveMemberMeazurements(MembersViewModel Model)
        {
            Model.MemberMeazurement.Member_ID = Model.Member.Member_ID;
            new MemberManager(context).SaveMemberMeazurements(Model.MemberMeazurement);
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
            new MemberManager(context).DeleteMemberMeazurements(id);
            return RedirectToAction("MemberAddMeazurements", "Members", new { id = member_id });
        }
        public IActionResult PassiveMember(int id)
        {
            new MemberManager(context).DeleteMember(id);
            new TaskManager(context).SaveTask(TaskBuilder(2, id));
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMember(int id)
        {
            new MemberManager(context).DeleteMember(id, true);
            new TaskManager(context).SaveTask(TaskBuilder(7, id));
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ActiveMember(int id)
        {
            new MemberManager(context).ActiveMember(id);
            new TaskManager(context).SaveTask(TaskBuilder(3, id));
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMemberExcersize(int id)
        {
            new ExcersizeManager(context).DeleteExcersize(id, true);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
        public IActionResult DeleteMemberDiet(int id)
        {
            new DietManager(context).DeleteDiet(id, true);
            return RedirectToAction("OpenedMember", "Members", new { id = id });
        }
    }
}
