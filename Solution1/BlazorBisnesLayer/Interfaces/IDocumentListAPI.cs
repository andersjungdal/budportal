using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBusinessLogic.Models.General;

namespace BlazorBusinessLogic.Interfaces
{
    public interface IDocumentListAPI
    {
        Task<List<BidDateAndVersion>> GetRawBidDatesByTheCompany(Guid companyPublicIdentifier);
    }
}