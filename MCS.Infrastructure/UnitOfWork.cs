// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Infrastructure.Repositories;
using MCS.Infrastructure.Repositories.Interfaces;

namespace MCS.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;
        IEventHistoryRepository _eventHistory;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEventHistoryRepository EventHistory
        {
            get
            {
                if (_eventHistory == null)
                    _eventHistory = new EventHistoryRepository(_context);

                return _eventHistory;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
