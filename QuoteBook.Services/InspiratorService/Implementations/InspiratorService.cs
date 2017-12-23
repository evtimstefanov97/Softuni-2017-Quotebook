using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteBook.Data.Models;
using QuoteBook.Services.InspiratorService.Models;
using QuoteBook.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.IO;

namespace QuoteBook.Services.InspiratorService.Implementations
{
    public class InspiratorService : IInspiratorService
    {

        private readonly QuoteBookDbContext context;
        public InspiratorService(QuoteBookDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<InspiratorListingModel>> All()
        {
            return await this.context.Inspirators.ProjectTo<InspiratorListingModel>().OrderBy(c => c.TimesQuoted).ToListAsync();
        }

        public async Task<bool> CreateInspiratorAsync(InspiratorCreateEditServiceModel model)
        {
            if (model.Name == null
                || model.BirthDate < new DateTime(1900, 1, 1)
                || model.BirthDate > new DateTime(2017, 1, 1))
            {
                return false;
            }
            var inspirator = new Inspirator();
            inspirator.Name = model.Name;
            inspirator.Posts = new List<Post>();
            inspirator.BirthDate = model.BirthDate;

            if (model.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(memoryStream);
                    inspirator.Image = memoryStream.ToArray();
                }
            }
            await this.context.Inspirators.AddAsync(inspirator);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditInspiratorAsync(InspiratorCreateEditServiceModel model)
        {
            if (model.Name == null
             || model.BirthDate < new DateTime(1900, 1, 1)
             || model.BirthDate > new DateTime(2017, 1, 1))
            {
                return false;
            }

            var inspirator = await this.context.Inspirators.FindAsync(model.Id);
            inspirator.Name = model.Name;

            if (model.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(memoryStream);
                    inspirator.Image = memoryStream.ToArray();
                }
            }

            await this.context.SaveChangesAsync();

            return true;
        }
        public async Task<Inspirator> FindInspiratorAsync(string inspiratorId)
        {
            return await this.context.Inspirators.FindAsync(inspiratorId);
        }
    }
}
