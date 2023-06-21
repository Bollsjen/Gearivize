using Gerivize.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Gerivize.Repositories;

namespace Gerivize.Repositories
{
    public class TemplateRepository
    {
        private readonly GearivizeLocalContext _localContext;

        public TemplateRepository()
        {
            _localContext = new GearivizeLocalContext();
        }

        public List<TestTemplate> getAll()
        {
            return _localContext.TestTemplates.ToList();
        }

        public TestTemplate? getById(Guid id)
        {
            return _localContext.TestTemplates.ToList().Find(i => i.Id == id);
        }
    }
}
