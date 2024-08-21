using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class RoomReservationService : CrudService<RoomReservation>, IRoomReservationService
{
    private readonly IRoomReservationRepository repository;

    public RoomReservationService(IRoomReservationRepository repository)
        : base(repository)
    {
        this.repository = repository;
    }

    public async Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation)
    {
        return await repository.GetOrLoadCurrentCheckinInfo(roomReservation);
    }

    public async Task PrepareNewCheckin(RoomReservation roomReservation)
    {
        await GetOrLoadCurrentCheckinInfo(roomReservation);

        roomReservation.CurrentCheckin ??= new();
        roomReservation.CurrentCheckin.CustomerCheckinInfos ??= new();

        for (int i = 0; i < roomReservation.Room.Capacity; i++)
        {
            CustomerCheckinInfo cch = new()
            {
                Customer = new()
                {
                    CustomerIdentity = new()
                }
            };
            roomReservation.CurrentCheckin.CustomerCheckinInfos.Add(cch);
        }
    }

    public async Task<bool> Checkout(RoomReservation roomReservation)
    {
        return await repository.Checkout(roomReservation) > 0;
    }
}
