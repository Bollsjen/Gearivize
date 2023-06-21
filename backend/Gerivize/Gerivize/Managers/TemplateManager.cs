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
    }
}