using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Novell.Directory.Ldap.Utilclass;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure.Helper;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // returns all users as a list of DTOs
        public IEnumerable<UserListDto> GetAllUsers()
        {
            return _applicationDbContext.AppUser.Select(entry => entry.ToListDto());
        }

        // returns a specific user as a DTO
        public UserDetailDto GetUserById(Guid userId)
        {
            return _applicationDbContext.AppUser
                .Select(entry => entry.ToDetailDto())
                .SingleOrDefault(e => e.Id == userId);
        }

        // returns one user by its email
        public AppUser GetUserByEmail(string email)
        {
            return _applicationDbContext.AppUser.SingleOrDefault(e => e.Email == email);
        }

        // creates a new user
        public IActionResult CreateUser(string rzName, string rzPassword, UserCreateDto user)
        {
            var rzNameData = Convert.FromBase64String(rzName);
            var decodedRzname = Encoding.UTF8.GetString(rzNameData);

            var rzPasswordData = Convert.FromBase64String(rzPassword);
            var decodedPassword = Encoding.UTF8.GetString(rzPasswordData);

            var ldapCredentialsAreValid = CheckIfLdapCredentialsAreValid(decodedRzname, decodedPassword);
            if (!ldapCredentialsAreValid)
            {
                return new BadRequestObjectResult("Wrong Ldap Credentials");
            }

            var ldapUserAsDictionary = LdapHelper.GetLdapUser(decodedRzname);
            var ldapIdOfUser = Convert.ToInt32(ldapUserAsDictionary["uidNumber"]);

            var userAllreadyExists = CheckIfUserAllreadyExists(ldapIdOfUser, decodedRzname);

            if (userAllreadyExists)
            {
                return new BadRequestObjectResult("User is allready registered");
            }

            var newUser = user.ToModel();
            newUser.Firstname = ldapUserAsDictionary["givenName"];
            newUser.Lastname = ldapUserAsDictionary["sn"];
            newUser.EmployeeType = ldapUserAsDictionary["employeeType"];
            newUser.RzName = ldapUserAsDictionary["uid"];
            newUser.LdapId = Convert.ToInt32(ldapUserAsDictionary["uidNumber"]);

            _applicationDbContext.AppUser.Add(newUser);
            Save();

            return new CreatedResult($"/users/{newUser.Id}", newUser.ToDetailDto());
        }

        // removes a role from a user
        public IActionResult RemoveRoleFromUser(Guid userId, Guid roleId)
        {
            var userHasRole = _applicationDbContext.UserHasRole.SingleOrDefault(
                entry => entry.UserId == userId && entry.RoleId == roleId);

            if (userHasRole == null)
            {
                return new NotFoundObjectResult("User doesn't have given role");
            }

            _applicationDbContext.UserHasRole.Remove(userHasRole);

            return new OkObjectResult("Role successfully removed from user");
        }

        // adds a role to a user
        public IActionResult AddRoleToUser(Guid userId, RoleDto roleDto)
        {
            var userHasRole = _applicationDbContext.UserHasRole.SingleOrDefault(
                entry => entry.RoleId == roleDto.Id && entry.UserId == userId);

            if (userHasRole != null)
            {
                return new BadRequestObjectResult("User allready has that role");
            }

            _applicationDbContext.UserHasRole.Add(new UserHasRole()
            {
                UserId = userId,
                RoleId = roleDto.Id
            });

            Save();

            var updatedUser = GetUserById(userId);

            return new OkObjectResult(updatedUser);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        // checks if the user which should be created allready exists in the database
        private bool CheckIfUserAllreadyExists(int ldapId, string rzName)
        {
            var userToCreate = _applicationDbContext.AppUser.SingleOrDefault(user => user.LdapId == ldapId && user.RzName == rzName);
            return userToCreate != null;
        }

        // checks if the LDAP credentials of the user are valid
        private bool CheckIfLdapCredentialsAreValid(string rzName, string rzPassword)
        {
            return LdapHelper.ValidateCredentials(rzName, rzPassword);
        }
    }
}