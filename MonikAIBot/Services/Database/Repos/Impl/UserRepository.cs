﻿using Microsoft.EntityFrameworkCore;
using MonikAIBot.Services.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonikAIBot.Services.Database.Repos.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User GetOrCreateUser(ulong UserID, bool Exemption = false, DateTime? dt = null)
        {
            if (dt == null)
                dt = DateTime.MinValue;

            User toReturn;
            toReturn = _set.FirstOrDefault(x => x.UserID == UserID);

            if (toReturn == null)
            {
                _set.Add(toReturn = new User()
                {
                    UserID = UserID,
                    IsExempt = Exemption,
                    DateOfBirth = (DateTime)dt,
                    MinecraftUsername = "none",
                    PersonalWaifu = "",
                    SteamID = 0
                });
                _context.SaveChanges();
            }

            return toReturn;
        }

        public int GetBotUserID(ulong UserDiscordID)
        {
            User u = GetOrCreateUser(UserDiscordID);
            return u.ID;
        }

        public bool GetExemptionStatus(ulong UserDiscordID)
        {
            User u = GetOrCreateUser(UserDiscordID);
            return u.IsExempt;
        }

        public void SetExemption(ulong UserDiscordID, bool Exemption)
        {
            User u = GetOrCreateUser(UserDiscordID);
            u.IsExempt = Exemption;

            _set.Update(u);
            _context.SaveChanges();
        }

        public List<User> GetAllBirthdays(DateTime date)
        {
            try
            {
                return _set.Where(x => x.DateOfBirth.Day == date.Day && x.DateOfBirth.Month == date.Month && !x.DateOfBirth.Equals(DateTime.MinValue)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetUserBirthday(ulong id, DateTime date)
        {
            User uTUpdate = GetOrCreateUser(id);
            uTUpdate.DateOfBirth = date;

            _set.Update(uTUpdate);
            _context.SaveChanges();
        }

        public void SetupAllBirthdays()
        {
            List<User> users = _set.ToList();
            List<User> update = new List<User>();
            foreach (User u in users)
            {
                u.DateOfBirth = DateTime.MinValue;
                update.Add(u);
            }

            _set.UpdateRange(update);
            _context.SaveChanges();
        }

        public List<User> GetNine(int page = 0)
        {
            int offset = page * 9;
            return _set.Where(x => !x.DateOfBirth.Equals(DateTime.MinValue)).OrderBy(x => x.ID).Skip(offset).Take(9).ToList();
        }

        public void SetMinecraftUsername(ulong UserDiscordID, string name)
        {
            User uTUpdate = GetOrCreateUser(UserDiscordID);
            uTUpdate.MinecraftUsername = name;

            _set.Update(uTUpdate);
            _context.SaveChanges();
        }

        public string GetMinecraftUsername(ulong UserDiscordID)
        {
            User u = GetOrCreateUser(UserDiscordID);
            return u.MinecraftUsername;
        }

        public string SetPersonalWaifu(ulong UserDiscordID, string waifu)
        {
            User u = GetOrCreateUser(UserDiscordID);
            u.PersonalWaifu = waifu;

            _set.Update(u);
            _context.SaveChanges();

            return u.PersonalWaifu;
        }

        public string GetPersonalWaifu(ulong UserDiscordID)
        {
            User u = GetOrCreateUser(UserDiscordID);
            return u.PersonalWaifu;
        }

        public ulong GetSteamID(ulong UserDiscordID)
        {
            User u = GetOrCreateUser(UserDiscordID);
            return u.SteamID;
        }

        public void SetSteamID(ulong UserDiscordID, ulong SteamID)
        {
            User u = GetOrCreateUser(UserDiscordID);
            u.SteamID = SteamID;

            _set.Update(u);
            _context.SaveChanges();
        }
    }
}
