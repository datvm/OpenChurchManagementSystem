using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenChurchManagementSystem.WebApi.Models.Entities.Services
{
    public partial interface IChurchDomainService
    {

        ChurchDomain FindDomain(string hostName, int port);

    }

    public partial class ChurchDomainService
    {

        public ChurchDomain FindDomain(string hostName, int port)
        {
            return this.FirstOrDefaultActive(q => q.Hostname == hostName && q.Port == port);
        }

    }
}