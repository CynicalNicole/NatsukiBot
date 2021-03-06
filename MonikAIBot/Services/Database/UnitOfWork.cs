﻿
using MonikAIBot.Services.Database.Repos;
using MonikAIBot.Services.Database.Repos.Impl;
using System;
using System.Threading.Tasks;

namespace MonikAIBot.Services.Database
{
    class UnitOfWork : IUnitOfWork
    {
        public DBContext _context { get; }
        private readonly MonikAIBotLogger logger = new MonikAIBotLogger();

        private IChannelsRepository _channels;
        public IChannelsRepository Channels => _channels ?? (_channels = new ChannelsRepository(_context));

        private IUserRepository _user;
        public IUserRepository User => _user ?? (_user = new UserRepository(_context));

        private IUserRateRepository _userRate;
        public IUserRateRepository UserRate => _userRate ?? (_userRate = new UserRateRepository(_context));

        private IGuildRepository _guild;
        public IGuildRepository Guild => _guild ?? (_guild = new GuildRepository(_context));

        private IGreetMessagesRepository _greetMessages;
        public IGreetMessagesRepository GreetMessages => _greetMessages ?? (_greetMessages = new GreetMessagesRepository(_context));

        private IBlockedLogsRepository _blockedLogs;
        public IBlockedLogsRepository BlockedLogs => _blockedLogs ?? (_blockedLogs = new BlockedLogsRepository(_context));

        private IAutoBanRepository _autoBan;
        public IAutoBanRepository AutoBan => _autoBan ?? (_autoBan = new AutoBanRepository(_context));

        private IWaifusRepository _waifus;
        public IWaifusRepository Waifus => _waifus ?? (_waifus = new WaifusRepository(_context));

        private IBotConfigRepository _botConfig;
        public IBotConfigRepository BotConfig => _botConfig ?? (_botConfig = new BotConfigRepository(_context));

        private IBotStatusesRepository _botStatuses;
        public IBotStatusesRepository BotStatuses => _botStatuses ?? (_botStatuses = new BotStatusesRepository(_context));

        public UnitOfWork(DBContext context)
        {
            _context = context;
        }

        public int Complete() =>
            _context.SaveChanges();

        public Task<int> CompleteAsync() =>
            _context.SaveChangesAsync();

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _context.Dispose();
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
