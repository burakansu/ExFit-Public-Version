using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class BlogViewModel
    {
        public ObjBlog Post { get; set; }
        public List<ObjBlog> Posts { get; set; }
        public List<ObjBlog> RecentPosts { get; set; }
        public List<ObjBlog> PopularPosts { get; set; }
    }
}
