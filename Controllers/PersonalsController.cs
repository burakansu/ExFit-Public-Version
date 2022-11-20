﻿using BussinesLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExFit.Controllers
{
    public class PersonalsController : MemberControllerBase
    {
        private Context context;
        public PersonalsController(Context _context)
        {
            context = _context;
        }
        public PersonalsViewModel ViewModel(int id = 0)
        {
            PersonalsViewModel personalsViewModel = new PersonalsViewModel();
            personalsViewModel.User = new UserManager(context).GetUser((int)HttpContext.Session.GetInt32("ID"));
            personalsViewModel.Users = new UserManager(context).GetUsers();
            personalsViewModel.Tasks = new TaskManager(context).GetLastFiveTask();
            personalsViewModel.TodayTasks = new TaskManager(context).GetLastFiveTask(1);
            if (id != 0)
            {
                personalsViewModel.SelectedUser = new UserManager(context).GetUser(id);
                personalsViewModel.UsersTasks = new UserManager(context).GetUserTasks(id);
            }
            return personalsViewModel;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
        public IActionResult UserSettings(int id)
        {
            return View(ViewModel(id));
        }
        public IActionResult AllActivitiesToday()
        {
            return View(ViewModel());
        }
        public IActionResult AddPersonal()
        {
            return View(ViewModel());
        }
        public IActionResult GetUser(int id)
        {
            return PartialView("Partial/_PersonalsDetails", ViewModel(id));
        }
        public IActionResult Delete(int id)
        {
            new UserManager(context).DeleteUser(id);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> RegistryingAsync(PersonalsViewModel personalsViewModel)
        {
            if (personalsViewModel.file != null)
            {
                string imageExtension = Path.GetExtension(personalsViewModel.file.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Personal/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await personalsViewModel.file.CopyToAsync(stream);
                personalsViewModel.SelectedUser.IMG = $"/Personal/{imageName}";
            }
            else if (personalsViewModel.SelectedUser.IMG == null) { personalsViewModel.SelectedUser.IMG = $"/Personal/AvatarNull.png"; }

            new UserManager(context).SaveUser(personalsViewModel.SelectedUser);
            new TaskManager(context).SaveTask(TaskBuilder(4, 0));

            personalsViewModel.User = new UserManager(context).GetUser((int)HttpContext.Session.GetInt32("ID"));
            return RedirectToAction("Index", "Home");
        }
    }
}
