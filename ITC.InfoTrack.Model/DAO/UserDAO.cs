using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class UserDAO : IUser
    {

         private readonly string _imagePath;
        private readonly DatabaseConnection _connection;

        public UserDAO( DatabaseConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _imagePath = configuration["ImageStorage:TokenImagePath"];
        }

        public async Task<List<User>> getUserList()
        {
            try
            {
                var userlsit = await _connection.Users.Where(i => i.UserStatus == 1).ToListAsync();
                return userlsit;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool status)> saveUserData(CreateUserDto model)
        {
            try
            {
                
                if (model != null)
                {
                    string imageFileName = null;



                    if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                    {
                        // Unique filename
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string extension = Path.GetExtension(model.ProfileImage.FileName);
                        string firstName = model.FirstName.Replace(" ", ""); // remove spaces

                         imageFileName = $"{firstName}_{timestamp}{extension}";

                        // Full path
                        string fullPath = Path.Combine(_imagePath, imageFileName);

                        // Ensure directory exists
                        if (!Directory.Exists(_imagePath))
                            Directory.CreateDirectory(_imagePath);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await model.ProfileImage.CopyToAsync(stream);
                        }
                    }
                    var slotpass = PasswordHelper.GenerateSalt(9);
                    var encrepted = PasswordHelper.HashPassword(model.Password);

                    var savedata = new User
                    {
                        UserName = model.FirstName +" "+ model.LastName,
                        Email = model.Email,
                        LoginName = model.LoginName,
                        EncriptedPassword= encrepted,
                        Salt = encrepted,
                        Password = encrepted+"_"+model.Password +"_"+ slotpass,
                        LastPasswordChange=DateTime.Now,
                        LastLogin=DateTime.Now,
                        UserStatus=1,
                        PoolId=1,
                        CreatedBy=1,
                        CreateDate=DateTime.Now,
                        PhoneNumber=model.MobileNumber,
                        ImageUrl= imageFileName


                    };

                    await _connection.Users.AddRangeAsync(savedata);
                    await _connection.SaveChangesAsync();

                    return ("User Save Successfuly", true);

                }

                return ("Invalid Data List", false);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
