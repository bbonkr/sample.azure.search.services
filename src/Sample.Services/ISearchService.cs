using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Azure.Search.Documents.Models;

using Sample.Services.Models;

namespace Sample.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<HotelModel>> SearchByHotelNameAsync(string keyword = "", CancellationToken token = default);

        Task<IEnumerable<IndexingResult>> UploadAsync(IEnumerable<HotelModel> models, CancellationToken cancellationToken = default);

        Task MergeAsync(IEnumerable<HotelModel> models, CancellationToken cancellationToken = default);

        Task DeleteAsync(IEnumerable<HotelModel> models, CancellationToken cancellationToken = default);
    }
}
