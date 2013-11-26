using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Application.Core;
using Application.Core.Data;
using Application.Infrastructure.AccountManagement;
using Application.Infrastructure.StudentManagement;

namespace LMPlatform.UI.ViewModels.AccountViewModels
{
    using System.Globalization;
    using System.Web.Mvc;

    using Application.Infrastructure.UserManagement;

    using LMPlatform.Data.Repositories.RepositoryContracts;
    using LMPlatform.Models;

    using WebMatrix.WebData;

    public class RegisterViewModel
    {
        private readonly LazyDependency<IAccountManagementService> _accountRegistrationService = new LazyDependency<IAccountManagementService>();
        private readonly LazyDependency<IGroupsRepository> _groupsRepository = new LazyDependency<IGroupsRepository>();
        private readonly LazyDependency<IStudentsRepository> _studentsRepository = new LazyDependency<IStudentsRepository>();
        private readonly LazyDependency<IStudentManagementService> _studentManagementService = new LazyDependency<IStudentManagementService>();
        private readonly LazyDependency<IUsersManagementService> _usersManagementService = new LazyDependency<IUsersManagementService>();

        public IStudentsRepository StudentsRepository
        {
            get
            {
                return _studentsRepository.Value;
            }
        }

        public IStudentManagementService StudentManagementService
        {
            get
            {
                return _studentManagementService.Value;
            }
        }

        public IGroupsRepository GroupsRepository
        {
            get
            {
                return _groupsRepository.Value;
            }
        }

        public IAccountManagementService AccountRegistrationService
        {
            get
            {
                return _accountRegistrationService.Value;
            }
        }

        public IUsersManagementService UsersManagementService
        {
            get
            {
                return _usersManagementService.Value;
            }
        }

        [StringLength(50, ErrorMessage = "��� �� ����� ����� ������ ������ 50 ��������")]
        [DataType(DataType.Text)]
        [Display(Name = "���")]
        public string Name
        {
            get;
            set;
        }

        [StringLength(50, ErrorMessage = "������� �� ����� ����� ������ ������ 50 ��������")]
        [DataType(DataType.Text)]
        [Display(Name = "�������")]
        public string Surname
        {
            get;
            set;
        }

        [StringLength(50, ErrorMessage = "�������� �� ����� ����� ������ ������ 50 ��������")]
        [DataType(DataType.Text)]
        [Display(Name = "��������")]
        public string Patronymic
        {
            get;
            set;
        }

        [Required(ErrorMessage = "���� ����� ����������� ��� ����������")]
        [Display(Name = "�����")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "���� ������ ����������� ��� ����������")]
        [StringLength(100, ErrorMessage = "T{0} ������ ���� �� ����� {2} ��������.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "������")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "������������� ������")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "������ � �������������� ������ �� ���������.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "������")]
        public string Group
        {
            get;
            set;
        }

        public IList<SelectListItem> GetGroups()
        {
            var groups = GroupsRepository.GetAll();

            return groups.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public void RegistrationUser(IList<string> roles)
        {
            AccountRegistrationService.CreateAccount(UserName, Password, roles);
            SaveStudent();
        }

        private void SaveStudent()
        {
            var user = UsersManagementService.GetUser(UserName);
            StudentsRepository.SaveStudent(new Student
            {
                Id = user.Id,
                FirstName = Name,
                LastName = Surname,
                MiddleName = Patronymic,
                GroupId = int.Parse(Group)
            });
        }
    }
}