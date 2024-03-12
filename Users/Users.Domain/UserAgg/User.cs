using Shared.Domain;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.UserAgg
{
    public class User : BaseEntity<int>
    {
        public string FullName { get; private set; }
        public string Mobile { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Avatar { get; private set; }
        public bool Active { get; private set; }
        public bool IsDelete { get; private set; }
        public Gender UserGender { get; private set; }
        public List<UserAddress> UserAddresses { get; private set; }
        public List<UserRole> UserRoles { get; private set; }
        private User()
        {
            
        }
        public User(string fullName, string mobile, string email, string password,
             string avatar, bool active, bool delete, Gender gender)
        {
            FullName = fullName;
            Mobile = mobile;
            Email = email;
            Password = password;
            Avatar = avatar;
            Active = active;
            IsDelete = delete;
            UserGender = gender;
        }
        public void Edit(string fullName, string mobile,
            string email, string password, string avatar, Gender gender)
        {
            FullName = fullName;
            Mobile = mobile;
            Email = email;

            if(!string.IsNullOrEmpty(password))
            Password = password;

            Avatar = avatar;
            UserGender = gender;
        }
        public void ActivationChange()
        {
            if (Active) Active = false;
            else Active = true;
        }
        public void DeleteChange()
        {
            if (IsDelete) IsDelete = false;
            else IsDelete = true;
        }
        public static User Register(string mobile, string password)
        {
            return new("",mobile,"",password,"default.png",false,false, Gender.نامشخص);
        }
        public void ChangePassword(string pass)
        {
            Password = pass;
        }
    }
}
