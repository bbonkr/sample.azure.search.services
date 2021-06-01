using System.Threading.Tasks;

using Azure.Search.Documents.Indexes.Models;

namespace Sample.Services
{
    public interface ISearchIndexService
    {
        Task<SearchIndex> CreateIndex();
    }
}