using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class BookingTotalsService : CrudService<BookingTotals>, IBookingTotalsService
{
    private readonly IBookingTotalsRepository repository;

    public BookingTotalsService(IBookingTotalsRepository repository)
        : base(repository)
    {
        this.repository = repository;
    }

    public BookingTotals CalculateTotals(BookingViewModel viewModel)
    {
        return new BookingTotals
        {
            Nights = viewModel.Nights,
            CustomGrandTotal = viewModel.HasCustomGrandTotal ? viewModel.CustomGrandTotal : null,
            Modifiers = GenerateRoomTotals(viewModel) //TODO: calculate tax and discounts as well
        };
    }

    public List<TotalsModifier> GenerateRoomTotals(BookingViewModel viewModel)
    {
        List<TotalsModifier> totals = new();

        foreach (Room room in viewModel.ReservedRooms ?? new())
        {
            if (room.Category == null)
            {
                throw new Exception("Room category not loaded.");
            }

            totals.Add(new TotalsCategoryModifier()
            {
                Category = room.Category,
                CategoryId = room.CategoryId,
                Name = room.Category.Name,
                Price = room.Category.Price,
                IsPricePerNight = true,
            });

            if (room.Features == null)
            {
                throw new Exception("Room features not loaded.");
            }

            foreach (RoomFeature feature in room.Features)
            {
                totals.Add(new TotalsFeatureModifier()
                {
                    Feature = feature,
                    FeatureId = feature.Id,
                    Name = feature.Name,
                    Price = feature.Price,
                    IsPricePerNight = feature.IsPricePerNight,
                });
            }
        }

        return totals;
    }

    public decimal GetRoomPrice(Room room, int nights)
    {
        if (room.Category == null)
        {
            throw new Exception($"The category must be loaded in order to calculate the total price.");
        }

        return room.Category.Price * nights;
    }

    public decimal GetRoomFeaturesPrice(Room room, int nights)
    {
        if (room.Features == null)
        {
            throw new Exception($"The features must be loaded in order to calculate the total price.");
        }

        return room.Features.Sum(f => CalculatePriceOfChargeable(f, nights));
    }

    public decimal GetTotalPriceForPeriod(Room room, int nights)
    {
        return GetRoomPrice(room, nights) + GetRoomFeaturesPrice(room, nights);
    }

    public decimal CalculatePriceOfChargeable(IChargeable chargeable, int nights)
    {
        decimal price = chargeable.Price;

        if (!chargeable.IsPricePerNight) return price;

        return price * nights;
    }

    public Task<List<TotalsModifier>> GetOrLoadModifiers(BookingTotals totals)
    {
        return repository.GetOrLoadModifiers(totals);
    }

    public Task<List<TTotalsModifier>> GetOrLoadModifiers<TTotalsModifier>(BookingTotals totals)
        where TTotalsModifier : TotalsModifier
    {
        return repository.GetOrLoadModifiers<TTotalsModifier>(totals);
    }

    public async Task<decimal> CalculateTotals<TTotalsModifier>(BookingTotals totals)
        where TTotalsModifier : TotalsModifier
    {
        List<TTotalsModifier> modifiers = await GetOrLoadModifiers<TTotalsModifier>(totals);

        return modifiers.Sum(m => CalculatePriceOfChargeable(m, totals.Nights));
    }

    public Task<decimal> CalculateGrandTotal(BookingTotals totals)
    {
        return CalculateTotals<TotalsModifier>(totals);
    }
}
