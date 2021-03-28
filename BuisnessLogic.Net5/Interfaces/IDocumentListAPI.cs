using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBuisnessLogic.Net5.Models.General;

namespace BlazorBuisnessLogic.Net5.Interfaces
{
    public interface IDocumentListAPI
    {
        Task<List<BidDateAndVersion>> GetRawBidDatesByTheCompany(Guid companyPublicIdentifier);
    }
}