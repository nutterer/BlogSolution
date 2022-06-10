using BlogSolution.Models.Bussiness.Base;
using database.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogSolution.Models.Bussiness.Blog
{
    public class BlogBC : BaseBC
    {
        public List<tnBlog> getBlog(string BlogID = "")
        {
            var Blog = new List<tnBlog>();
            if (!string.IsNullOrEmpty(BlogID))
                Blog = qDB.tnBlogs.Where(w => w.BlogID == BlogID).ToList();
            else
                Blog = qDB.tnBlogs.ToList();

            return Blog;
        }

        public tnBlog bindSave(tnBlog model  )
        {
            
            var data = new tnBlog();
            data.BlogID = Guid.NewGuid().ToString();
            data.CreateByID = UserStatus.MemberID;
            data.BlogTitle = model.BlogTitle;
            data.BlogBannerURl = model.BlogBannerURl;
            data.BlogDetail = model.BlogDetail;
            data.CreateDate = model.CreateDate;
            
            saveDefault<tnBlog>(data);

            return data;
        }
        public tnBlog bindEdit(tnBlog model)
        {
            var data = qDB.tnBlogs.Where(w => w.BlogID == model.BlogID).FirstOrDefault();
            data.BlogID = model.BlogID;
           
            data.BlogTitle = model.BlogTitle;
            data.BlogBannerURl = model.BlogBannerURl;
            data.BlogDetail = model.BlogDetail;
            
            qDB.Entry(data).State = EntityState.Modified;
            qDB.SaveChanges();

            return data;
        }

        public bool bindDelete(string BlogID)
        {
            sqlCommandExcute("DELETE From tnBlog where BlogID = '" + BlogID + "'");

            return isResult;
        }
    }
}