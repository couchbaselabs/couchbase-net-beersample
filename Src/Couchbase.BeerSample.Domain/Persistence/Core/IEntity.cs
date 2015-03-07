
namespace Couchbase.BeerSample.Domain.Persistence.Core
{
    public interface IEntity
    {
        string Id { get; set; }

        string Type { get; set; }

        ulong Cas { get; set; }
    }
}
