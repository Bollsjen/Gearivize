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

        public List<TemplateConnector> getAllInstruments()
        {
            return _localContext.TemplateConnector.ToList();
        }

        public TestTemplate? getById(Guid id)
        {
            return _localContext.TestTemplates.ToList().Find(i => i.Id == id);
        }

        public TestTemplate AddTemplate(TestTemplate testTemplate)
        {
            _localContext.TestTemplates.Add(testTemplate);
            _localContext.SaveChanges();
            return testTemplate;
        }

        public TemplateConnector AddInstrumentToTemplate(Guid templateId, string instrumentId)
        {
            TemplateConnector templateConnector = new TemplateConnector();
            templateConnector.TestTemplateId = templateId;
            templateConnector.InstrumentId = instrumentId;
            _localContext.TemplateConnector.Add(templateConnector);
            _localContext.SaveChanges();
            return templateConnector;
        }

        public TestTemplate UpdateTemplate(TestTemplate testTemplate)
        {
            _localContext.TestTemplates.Update(testTemplate);
            _localContext.SaveChanges();
            return testTemplate;
        }

        public TemplateConnector DeleteInstrumentFromTemplate(Guid templateId, string instrumentId)
        {
            TemplateConnector templateConnector = _localContext.TemplateConnector.Find(templateId, instrumentId);
            _localContext.TemplateConnector.Remove(templateConnector);
            _localContext.SaveChanges();
            return templateConnector;
        }

        public TestTemplate DeleteTemplate(Guid templateId)
        {
            Console.WriteLine(templateId);
            TestTemplate testTemplate = _localContext.TestTemplates.ToList().Find(t=>t.Id==templateId);
            _localContext.TestTemplates.Remove(testTemplate);
            _localContext.SaveChanges();
            return testTemplate;
        }
    }
}
