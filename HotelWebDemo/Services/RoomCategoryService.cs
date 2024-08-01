using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Json;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class RoomCategoryService : CrudService<RoomCategory>, IRoomCategoryService
{
    private readonly IRoomCategoryRepository repository;
    private readonly IHotelService hotelService;

    public RoomCategoryService(IRoomCategoryRepository repository, IHotelService hotelService)
        : base(repository)
    {
        this.repository = repository;
        this.hotelService = hotelService;
    }

    public override Table<RoomCategory> CreateListingTable(ListingModel<RoomCategory> listingModel, PaginatedList<RoomCategory> items)
    {
        return base.CreateListingTable(listingModel, items)
            .SetSearchable(false)
            .SetSelectableOptionsSource("Hotel", hotelService.GetAll());
    }

    public List<RoomCategory> GetAll(int hotelId)
    {
        return repository.GetAll(hotelId);
    }

    public List<RoomCategoryOption> GetAllAsOptions(int hotelId)
    {
        List<RoomCategoryOption> options = new();

        foreach (RoomCategory item in repository.GetAll(hotelId))
        {
            RoomCategoryOption option = new()
            {
                Id = item.Id,
                Name = item.Name,
                HotelId = item.HotelId,
            };

            options.Add(option);
        }

        return options;
    }
}
