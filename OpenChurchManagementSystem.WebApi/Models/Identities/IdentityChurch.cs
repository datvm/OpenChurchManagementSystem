using AutoMapper;
using OpenChurchManagementSystem.WebApi.Models.Entities;
using OpenChurchManagementSystem.WebApi.Models.Entities.Services;
using OpenChurchManagementSystem.WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenChurchManagementSystem.WebApi.Models.Identities
{

    public class IdentityChurch
    {

        public ChurchViewModel Church { get; set; }
        public ChurchDomainViewModel ChurchDomain { get; set; }

        public IdentityChurch(IChurchDomainService service, IMapper mapper)
        {
            var url = HttpContext.Current.Request.Url;

            var churchDomain = service.FindDomain(url.Scheme, url.Host, url.Port);

            if (churchDomain == null) { return; }

            this.ChurchDomain = mapper.Map<ChurchDomainViewModel>(churchDomain);
            this.Church = mapper.Map<ChurchViewModel>(churchDomain.Church);
        }

    }

}