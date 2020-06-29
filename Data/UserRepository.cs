using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using PWSZ_Plan_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ICodeGenerator _generator;

        public UserRepository(DataContext context, ICodeGenerator generator ) : base(context)
        {
            _generator = generator;
        }

        /// <summary>
        /// testing value added
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public void Test()
        {
            if(_context.Set<User>().FirstOrDefault(x => x.ID == 1) == null)
            {
                _context.Add(new User
                {
                    Name = "SuperUser",
                    HashCode = _generator.HashingCode("Admin1"),
                    Permission = Permission.SUPERUSER,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 0
                })
           .Context.Add(new Institute
           {
               Name = "Institute 1",
               Description = "Opis",
               LastUpdate = DateTime.Now,
               LastUpdateBy = 1
           })
           .Context.Add(new Institute
           {
               Name = "Institute 2",
               Description = "Opis",
               LastUpdate = DateTime.Now,
               LastUpdateBy = 1
           });

                _context.SaveChanges();

                _context.Add(new User
                {
                    Name = "User for Institute 1",
                    HashCode = _generator.HashingCode("test"),
                    InstituteID = 1,
                    Permission = Permission.INSTITUTE,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new User
                {
                    Name = "User for Institute 2",
                    HashCode = _generator.HashingCode("test"),
                    Permission = Permission.INSTITUTE,
                    InstituteID = 2,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new User
                {
                    Name = "Lecturer 1",
                    HashCode = _generator.HashingCode("test"),
                    Permission = Permission.LECTURER,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new User
                {
                    Name = "Lecturer 2",
                    HashCode = _generator.HashingCode("test"),
                    Permission = Permission.LECTURER,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new User
                {
                    Name = "Lecturer 3",
                    HashCode = _generator.HashingCode("test"),
                    Permission = Permission.LECTURER,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Major
                {
                    Name = "Major 1 for Institute 1",
                    Description = "Opis",
                    InstituteID = 1,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Major
                {
                    Name = "Major 2 for Institute 1",
                    Description = "Opis",
                    InstituteID = 1,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Major
                {
                    Name = "Major 1 for Institute 2",
                    Description = "Opis",
                    InstituteID = 2,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Major
                {
                    Name = "Major 2 for Institute 2",
                    Description = "Opis",
                    InstituteID = 2,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                });

                _context.SaveChanges();

                _context.Add(new Specialization
                {
                    Name = "Specializations 1",
                    Description = "Institute 1 > Major 1 > Specializations 1",
                    MajorID = 1,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Specialization
                {
                    Name = "Specializations 2",
                    Description = "Institute 1 > Major 1 > Specializations 2",
                    MajorID = 1,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Specialization
                {
                    Name = "Specializations 3",
                    Description = "Institute 1 > Major 2 > Specializations 3",
                    MajorID = 2,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Specialization
                {
                    Name = "Specializations 4",
                    Description = "Institute 1 > Major 2 > Specializations 4",
                    MajorID = 2,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Specialization
                {
                    Name = "Specializations 5",
                    Description = "Institute 2 > Major 1 > Specializations 5",
                    MajorID = 3,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Specialization
                {
                    Name = "Specializations 6",
                    Description = "Institute 2 > Major 1 > Specializations 6",
                    MajorID = 3,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Specialization
                {
                    Name = "Specializations 7",
                    Description = "Institute 2 > Major 2 > Specializations 7",
                    MajorID = 4,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Specialization
                {
                    Name = "Specializations 8",
                    Description = "Institute 2 > Major 2 > Specializations 8",
                    MajorID = 4,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                });

                _context.SaveChanges();

                _context.Add(new Year
                {
                    Name = "Year 1",
                    Description = "Institute 1 > Major 1 > Specializations 1 > Year 1",
                    SpecializationID = 1,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 2",
                    Description = "Institute 1 > Major 1 > Specializations 1 > Year 2",
                    SpecializationID = 1,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 3",
                    Description = "Institute 1 > Major 1 > Specializations 2 > Year 3",
                    SpecializationID = 2,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 4",
                    Description = "Institute 1 > Major 1 > Specializations 2 > Year 4",
                    SpecializationID = 2,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 5",
                    Description = "Institute 1 > Major 2 > Specializations 3 > Year 5",
                    SpecializationID = 3,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 6",
                    Description = "Institute 1 > Major 2 > Specializations 3 > Year 6",
                    SpecializationID = 3,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 7",
                    Description = "Institute 1 > Major 2 > Specializations 4 > Year 7",
                    SpecializationID = 4,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 8",
                    Description = "Institute 1 > Major 2 > Specializations 4 > Year 8",
                    SpecializationID = 4,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 9",
                    Description = "Institute 2 > Major 1 > Specializations 5 > Year 9",
                    SpecializationID = 5,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 10",
                    Description = "Institute 2 > Major 1 > Specializations 5 > Year 10",
                    SpecializationID = 5,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 11",
                    Description = "Institute 2 > Major 1 > Specializations 6 > Year 11",
                    SpecializationID = 6,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 12",
                    Description = "Institute 2 > Major 1 > Specializations 6 > Year 12",
                    SpecializationID = 6,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 13",
                    Description = "Institute 2 > Major 2 > Specializations 7 > Year 13",
                    SpecializationID = 7,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 14",
                    Description = "Institute 2 > Major 2 > Specializations 7 > Year 14",
                    SpecializationID = 7,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 15",
                    Description = "Institute 2 > Major 2 > Specializations 8 > Year 15",
                    SpecializationID = 8,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Year
                {
                    Name = "Year 16",
                    Description = "Institute 2 > Major 2 > Specializations 8 > Year 16",
                    SpecializationID = 8,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Room
                {
                    Name = "Room 001",
                    Description = "Opis",
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Room
                {
                    Name = "Room 002",
                    Description = "Opis",
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Room
                {
                    Name = "Room 003",
                    Description = "Opis",
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Subject
                {
                    Name = "Subject 1",
                    Description = "Opis",
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Subject
                {
                    Name = "Subject 2",
                    Description = "Opis",
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                })
                .Context.Add(new Subject
                {
                    Name = "Subject 3",
                    Description = "Opis",
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = 1
                });

                _context.SaveChanges();
            }
        }



        public async Task<User> Auth(string code)
        {
            var userList = await _context.Set<User>()
                .Select(x => new { id = x.ID, hash = x.HashCode })
                .ToListAsync();

            foreach(var item in userList)
            {
                if (_generator.VeryfingCode(code, item.hash))
                {
                    var userToSend = await _context.Set<User>().FirstAsync(x => x.ID == item.id);
                    return userToSend;
                }
            }

            return null;
        }

        
    }
}
