using QuoteBook.Data.Models;
using QuoteBook.Services.InspiratorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteBook.Services.InspiratorService
{
    public interface IInspiratorService
    {

        Task<IEnumerable<InspiratorListingModel>> All();

        Task<Inspirator> FindInspiratorAsync(string inspiratorId);


        Task EditInspiratorAsync(InspiratorCreateEditServiceModel model);

        Task CreateInspiratorAsync(InspiratorCreateEditServiceModel model);
    }
}
