using Microsoft.AspNetCore.Http;
using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class PersonalsViewModel
    {
        public ObjUser User { get; set; }
        public ObjCompany Company { get; set; }
        public ObjUser SelectedUser { get; set; }
        public List<ObjUser> Users { get; set; }
        public List<ObjTask> Tasks { get; set; }
        public List<ObjTask> UsersTasks { get; set; }
        public List<ObjTask> TodayTasks { get; set; }
        public IFormFile file { get; set; }
    }
}
