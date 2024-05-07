using Shared;
using Shared.Application;
using Shared.Application.Security;
using Shared.Application.Services;
using Shared.Application.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Contract.UserApplication.Command;
using Users.Domain.UserAgg;
using Users.Domain.UserAgg.Repository;

namespace Users.Application.Services
{
    internal class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IFileService _fileService;
        public UserApplication(IUserRepository userRepository,
            IAuthService authService, IFileService fileService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _fileService = fileService;
        }

        public bool ActivationChange(int id)
        {
            var user = _userRepository.GetById(id);
            user.ActivationChange();
            return _userRepository.Save();
        }

        public OperationResult ChangePassword(ChangeUserPassword command)
        {
            var user = _userRepository.GetById(command.UserId);
            var hashPass = Sha256Hasher.Hash(command.OldPassword);
            if (user.Password != hashPass) 
                return new(false, ValidationMessages.OldPasswordErrorMessage, "OldPassword");

            var hashNewPass = Sha256Hasher.Hash(command.NewPassword);
            user.ChangePassword(hashNewPass);
            if (_userRepository.Save()) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, "OldPassword");
        }

        public OperationResult Create(CreateUser command)
        {
            if (_userRepository.ExistBy(u => u.Mobile.Trim() == command.Mobile.Trim()))
                return new(false, ValidationMessages.DuplicatedMessage, "Mobile");
            if (!string.IsNullOrEmpty(command.Email) && 
                _userRepository.ExistBy(u => u.Email.ToLower().Trim() == command.Email.ToLower().Trim()))
                return new(false, ValidationMessages.DuplicatedMessage, "Email");

            string imageName = "default.png";
            if(command.AvatarFile != null)
            {
                if(!command.AvatarFile.IsImage())
                    return new(false, ValidationMessages.ImageErrorMessage, "AvatarFile");

                imageName = _fileService.UploadImage(command.AvatarFile, FileDirectories.UserImageFolder);
                _fileService.ResizeImage(imageName, FileDirectories.UserImageFolder, 100);
            }
            string hashPass = Sha256Hasher.Hash(command.Password);
            var key = GenerateRandomCode.GenerateUserRegisterCode().ToString();
            User user = new(command.FullName, command.Mobile.Trim(), command.Email.ToLower().Trim(),
               key, imageName, true, false, command.UserGender);
            if (_userRepository.Create(user)) return new(true);
            if (command.AvatarFile != null)
            {
                _fileService.DeleteImage(FileDirectories.UserImageDirectory + imageName);
                _fileService.DeleteImage(FileDirectories.UserImageDirectory100 + imageName);
            }
            return new(false, ValidationMessages.SystemErrorMessage, "FullName");
        }

        public bool DeleteChange(int id)
        {
            var user = _userRepository.GetById(id);
            user.DeleteChange();
            return _userRepository.Save();
        }

        public OperationResult Edit(EditUserByAdmin command)
        {
            var user = _userRepository.GetById(command.Id);
            if (_userRepository.ExistBy(u => u.Mobile.Trim() == command.Mobile.Trim() && u.Id != command.Id))
                return new(false, ValidationMessages.DuplicatedMessage, "Mobile");
            if (!string.IsNullOrEmpty(command.Email) &&
                _userRepository.ExistBy(u => u.Email.ToLower().Trim() == command.Email.ToLower().Trim() && u.Id != command.Id))
                return new(false, ValidationMessages.DuplicatedMessage, "Email");

            string imageName = command.AvatarName;
            string oldImageName = command.AvatarName;
            if (command.AvatarFile != null)
            {
                if (!command.AvatarFile.IsImage())
                    return new(false, ValidationMessages.ImageErrorMessage, "AvatarFile");

                imageName = _fileService.UploadImage(command.AvatarFile, FileDirectories.UserImageFolder);
                _fileService.ResizeImage(imageName, FileDirectories.UserImageFolder, 100);
            }
            var pass = user.Password;
            if(string.IsNullOrEmpty(command.Password))
             pass = Sha256Hasher.Hash(command.Password);

            user.Edit(command.FullName, command.Mobile.Trim(), command.Email.ToLower().Trim(),
                pass, imageName, command.UserGender);

            if (_userRepository.Save())
            {
                if (command.AvatarFile != null)
                {
                    if(oldImageName != "default.png")
                    {
                        _fileService.DeleteImage(FileDirectories.UserImageDirectory + oldImageName);
                        _fileService.DeleteImage(FileDirectories.UserImageDirectory100 + oldImageName);
                    }
                }
                    return new(true);
            }
            if (command.AvatarFile != null)
            {
                _fileService.DeleteImage(FileDirectories.UserImageDirectory + imageName);
                _fileService.DeleteImage(FileDirectories.UserImageDirectory100 + imageName);
            }
            return new(false, ValidationMessages.SystemErrorMessage, "FullName");
        }

        public OperationResult EditByUser(EditUserByUser command, int userId)
        {
            var user = _userRepository.GetById(userId);
            if (_userRepository.ExistBy(u => u.Mobile.Trim() == command.Mobile.Trim() && u.Id != userId))
                return new(false, ValidationMessages.DuplicatedMessage, "Mobile");
            if (!string.IsNullOrEmpty(command.Email) &&
                _userRepository.ExistBy(u => u.Email.ToLower().Trim() == command.Email.ToLower().Trim() && 
                u.Id != userId))
                return new(false, ValidationMessages.DuplicatedMessage, "Email");
            if(command.AvatarFile != null && !command.AvatarFile.IsImage())
                return new(false, ValidationMessages.ImageErrorMessage, "AvatarFile");
            string imageName = command.AvatarName;
            string oldImageName = command.AvatarName;
            if (command.AvatarFile != null)
            {
                if (!command.AvatarFile.IsImage())
                    return new(false, ValidationMessages.ImageErrorMessage, "AvatarFile");

                imageName = _fileService.UploadImage(command.AvatarFile, FileDirectories.UserImageFolder);
                _fileService.ResizeImage(imageName, FileDirectories.UserImageFolder, 100);
            }
            user.Edit(command.FullName, command.Mobile, command.Email, user.Password, imageName, command.UserGender);
            if (_userRepository.Save())
            {
                if(command.AvatarFile != null && oldImageName != "default.png")
                {
                    _fileService.DeleteImage(FileDirectories.UserImageDirectory + oldImageName);
                    _fileService.DeleteImage(FileDirectories.UserImageDirectory100 + oldImageName);
                }
                return new(true);
            }
            if (command.AvatarFile != null)
            {
                _fileService.DeleteImage(FileDirectories.UserImageDirectory + imageName);
                _fileService.DeleteImage(FileDirectories.UserImageDirectory100 + imageName);
            }
            return new(false, ValidationMessages.SystemErrorMessage, "FullName");
        }

        public EditUserByUser GetForEditByUser(int userId)
        {
            return _userRepository.GetForEditByUser(userId);
        }

        public OperationResult Login(LoginUser command)
        {
            User user = _userRepository.GetByMobile(command.Mobile.Trim());
            if (user == null)
                return new(false, ValidationMessages.UserNotFound, nameof(command.Mobile));

            var hashPass = Sha256Hasher.Hash(command.Password.Trim());

            //if(user.Password != hashPass)
            if(user.Password != command.Password.Trim())
                return new(false, ValidationMessages.PasswordLoginError, nameof(command.Password));

            _authService.Login(new()
            {
                Avatar=user.Avatar,
                FullName=string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName, 
                Mobile=user.Mobile,
                UserId=user.Id
            });
            return new(true);
        }

        public void Logout()
        {
            _authService.Logout();
        }

        public bool Register(RegisterUser command)
        {
            var key = GenerateRandomCode.GenerateUserRegisterCode().ToString();
            var pass = Sha256Hasher.Hash(key);
            try
            {
                var user = _userRepository.GetByMobile(command.Mobile.Trim());
                if (user == null)
                {
                    user = User.Register(command.Mobile.Trim(), key);
                    return _userRepository.Create(user);
                    // send sms active code
                }
                else
                {
                    user.ChangePassword(key);
                    return _userRepository.Save();
                    // send sms active code
                }
            }
            catch 
            {
                return false;
            }
            
        }
    }
}
