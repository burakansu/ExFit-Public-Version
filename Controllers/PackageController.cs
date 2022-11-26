using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class PackageController : Controller
    {
        public PackageViewModel ViewModel(int id = 0)
        {
            PackageViewModel VM = new PackageViewModel();

            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM.Company = new CompanyManager().GetCompany(VM.User.Company_ID);
            VM.Packages = new PackageManager().GetPackages(VM.Company.Company_ID);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
        public IActionResult SavePackage(PackageViewModel VM)
        {
            VM.Package.Company_ID = VM.Company.Company_ID;
            new PackageManager().AddDatabasePackage(VM.Package);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeletePackage(int id)
        {
            new PackageManager().DeletePackage(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
