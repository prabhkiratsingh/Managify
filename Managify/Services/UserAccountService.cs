using Managify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Managify.Services
{
    public class UserAccountService
    {
        private ApplicationDbContext db;

        public UserAccountService(ApplicationDbContext context)
        {
            db = context;
        }

        // Creates a new instance of UserAccount.
        public void CreateUserAccount(string firstName, string lastName, UserAccount.Genders gender, string course, 
            string branch, string collegeName, string userId)
        {
            var userAccount = new UserAccount
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Course = course,
                Branch = branch,
                CollegeName = collegeName,
                ApplicationUserId = userId
            };
            db.UserAccounts.Add(userAccount);
            db.SaveChanges();
        }
    }
}