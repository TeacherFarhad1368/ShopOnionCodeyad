using PostModule.Application.Contract.UserPostApplication.Command;
using PostModule.Domain.UserPostAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    internal class PackageApplication : IPackageApplication
    {
        private readonly IPackageRepository _packageRepository;

        public PackageApplication(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public bool ActivationChange(int id)
        {
            var package = _packageRepository.GetById(id);
            package.ActivationChange();
            return _packageRepository.Save();
        }

        public OperationResult Create(CreatePackage command)
        {
            if (_packageRepository.ExistBy(p => p.Title.Trim() == command.Title.Trim()))
                return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));

            Package package = new(command.Title, command.Description, command.Count, command.Price);
            if (_packageRepository.Create(package))
            {
                return new(true);
            }
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
        }

        public OperationResult Edit(EditPackage command)
        {
            var package = _packageRepository.GetById(command.Id);
            if (_packageRepository.ExistBy(p => p.Title.Trim() == command.Title.Trim() && p.Id != package.Id))
                return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));

            package.Edit(command.Title, command.Description, command.Count, command.Price);
            if (_packageRepository.Save()) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
        }

        public EditPackage GetForEdit(int id) =>
            _packageRepository.GetForEdit(id);
    }
}
