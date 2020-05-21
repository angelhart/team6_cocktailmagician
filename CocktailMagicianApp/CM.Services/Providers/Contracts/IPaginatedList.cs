namespace CM.Services.Providers.Contracts
{
    public interface IPaginatedList
    {
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        int PageNumber { get; }
        int TotalPages { get; }
    }
}