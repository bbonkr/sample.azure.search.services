using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.Services.Models;

namespace Sample.Services
{
    public interface ISearchService
    {
        IEnumerable<HotelModel> SearchByHotelName(string keyword = "");
    }
}
