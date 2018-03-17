namespace Forum.App.Controllers.Contracts
{
    interface IPaginationableController
    {
        PaginationController PaginationController { get; }
    }
}
