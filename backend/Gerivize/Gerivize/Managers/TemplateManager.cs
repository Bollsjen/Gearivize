using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using Gerivize.Models;
using Gerivize.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Gerivize.Managers
{
    public class TemplateManager
    {
        private readonly TemplateRepository _templateRepository;

        public TemplateManager()
        {
            _templateRepository = new TemplateRepository();
        }

        public List<TemplateConnector> GetAllInstruments()
        {
            return _templateRepository.getAllInstruments();
        }

        public List<TestTemplate> GetAllTemplates()
        {
            return _templateRepository.getAll();
        }

        public TestTemplate? GetTemplateById(Guid id)
        {
            return _templateRepository.getById(id);
        }

        public TestTemplate AddTemplate(TestTemplate testTemplate)
        {
            testTemplate.Id = Guid.NewGuid();
            _templateRepository.AddTemplate(testTemplate);
            return testTemplate;
        }

        public TemplateConnector AddInstrumentToTemplate(Guid templateId, string instrumentId)
        {
            _templateRepository.AddInstrumentToTemplate(templateId, instrumentId);
            return new TemplateConnector();
        }

        public TestTemplate UpdateTemplate(TestTemplate testTemplate)
        {
            _templateRepository.UpdateTemplate(testTemplate);
            return testTemplate;
        }

        public TemplateConnector DeleteInstrumentFromTemplate(Guid templateId, string instrumentId)
        {
            return _templateRepository.DeleteInstrumentFromTemplate(templateId, instrumentId);
        }

        public TestTemplate DeleteTemplate(Guid templateId)
        {
            return _templateRepository.DeleteTemplate(templateId);
        }
    }
}