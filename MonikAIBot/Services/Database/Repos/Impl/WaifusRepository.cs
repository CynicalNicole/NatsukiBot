﻿using MonikAIBot.Services.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonikAIBot.Services.Database.Repos.Impl
{
    public class WaifusRepository : Repository<Waifus>, IWaifusRepository
    {
        public WaifusRepository(DBContext context) : base(context)
        {
        }

        public void AddWaifu(string waifu)
        {
            _set.Add(new Waifus()
            {
                Waifu = waifu
            });
            _context.SaveChanges();
        }

        public string GetRandomWaifu()
        {
            return _set.RandomItem().Waifu;
        }

        public List<Waifus> GetWaifus()
        {
            return _set.ToList();
        }
    }
}
