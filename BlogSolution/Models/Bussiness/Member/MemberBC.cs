
using BlogSolution.Models.Bussiness.Base;
using BlogSolution.Models.ModelApp.Base;
using database.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogSolution.Models.Bussiness.Member
{
    public class MemberBC :BaseBC
    {
        public List<tnMember> getMember(string MemberID = "")
        {
            var Member = new List<tnMember>();
            if (!string.IsNullOrEmpty(MemberID))
                Member = qDB.tnMembers.Where(w => w.MemberID == MemberID).ToList();
            else
                Member = qDB.tnMembers.ToList();

            return Member;
        }
        public UserCookiesModel bindlogin(string UserName, string Password)
        {
            var user = new UserCookiesModel();
            var dataUser = qDB.tnMembers.Where(w => w.UserName == UserName && w.Password == Password).FirstOrDefault();
            if (dataUser != null)
            {
                user.MemberID = dataUser.MemberID;
                user.UserName = dataUser.UserName;
                user.MemberType = Convert.ToBoolean(dataUser.MemberType);
                user.FirstName = dataUser.FirstName;
                user.LastName = dataUser.LastName;
            }
            else
            {
                user.MemberID = null;
                user.UserName = null;
                return user = null;
            }

            return user;
        }
        public tnMember bindSave(tnMember model)
        {
            var data = new tnMember();
            data.MemberID = Guid.NewGuid().ToString();
            data.MemberType = model.MemberType;
            data.UserName = model.UserName;
            data.Password = model.Password;
            data.FirstName = model.FirstName;
            data.LastName = model.LastName;
            data.Email = model.Email;
            saveDefault<tnMember>(data);

            return data;
        }
        public tnMember bindEdit(tnMember model)
        {
            var data = qDB.tnMembers.Where(w => w.MemberID == model.MemberID).FirstOrDefault();
            data.MemberID = model.MemberID;
            data.MemberType = model.MemberType;
            data.UserName = model.UserName;
            data.Password = model.Password;
            qDB.Entry(data).State = EntityState.Modified;
            qDB.SaveChanges();
            return data;
        }
        public bool bindDelete(string MemberID)
        {
            sqlCommandExcute("DELETE From tnMember where MemberID = '" + MemberID + "'");

            return isResult;
        }

    }
}