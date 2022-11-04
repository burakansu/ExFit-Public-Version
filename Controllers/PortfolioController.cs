using BussinesLayer;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ObjectLayer;
using ExFit.Models;

namespace ExFit.Controllers
{
    //[Area("Portfolio")]
    public class PortfolioController : Controller
    {
        BlogViewModel Model = new BlogViewModel();
        BlogManager blogManager = new BlogManager();
        public IActionResult Blog()
        {
            Model.Posts = blogManager.GetPosts();
            Model.RecentPosts = blogManager.GetPosts(reverse: true);
            Model.PopularPosts = blogManager.GetPosts(popular: true);
            return View(Model);
        }
        public IActionResult OpenedPost(int id)
        {
            BlogViewModel Model = new BlogViewModel();
            BlogManager blogManager = new BlogManager();
            Model.Post = blogManager.GetPosts(id, true)[0];
            return View(Model);
        }
        public IActionResult NewPost(int id = 0)
        {
            Model.Posts = blogManager.GetPosts();
            List<ObjBlog> objPosts = blogManager.GetPosts(id, true);
            if (objPosts.Count > 0)
            {
                Model.Post = objPosts[0];
            }
            else
            {
                Model.Post = new ObjBlog();
            }
            return View(Model);
        }
        public async Task<IActionResult> SavePostAsync(BlogViewModel Model)
        {
            if (Model.Post.FileAvatarIMG != null)
            {
                string imageExtension = Path.GetExtension(Model.Post.FileAvatarIMG.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Member/ProfilePhotos/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await Model.Post.FileAvatarIMG.CopyToAsync(stream);
                Model.Post.IMG = $"/Member/ProfilePhotos/{imageName}";
            }
            else if (Model.Post.IMG == null) { Model.Post.IMG = "/Member/ProfilePhotos/BlogNull.png"; }
            blogManager.AddDatabasePost(Model.Post);
            return RedirectToAction("NewPost", "Portfolio", blogManager.GetPosts());
        }
        public IActionResult DeletePost(int id)
        {
            blogManager.DeletePost(id);
            return RedirectToAction("NewPost", "Portfolio", blogManager.GetPosts());
        }
        public IActionResult Hakkimda()
        {
            return View();
        }
        public IActionResult ExFit()
        {
            return View();
        }
        public IActionResult ExFitCore()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult LocationManager()
        {
            return View();
        }
        public IActionResult MyProjects()
        {
            return View();
        }
        public IActionResult References()
        {
            return View();
        }
        public IActionResult TaskManager()
        {
            return View();
        }
    }
}
