namespace Calculators;

using ConsumerVehicleRegistration;
using CommercialRegistration;
using LiveryRegistration;

public class TollCalculator
{
    public decimal CalculateToll(object vehicle) => vehicle switch
    {
        Car c => c.Passengers switch
        {
            0 => 2.00m + 0.50m,
            1 => 2.00m,
            2 => 2.00m - 0.50m,
            _ => 2.00m - 1.00m
        },

        Taxi t => t.Fares switch
        {
            0 => 3.50m + 1.00m,
            1 => 3.50m,
            2 => 3.50m - 0.50m,
            _ => 3.50m - 1.00m
        },

        Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => 5.00m + 2.00m,
        Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => 5.00m - 1.00m,
        Bus => 5.00m,

        DeliveryTruck t when t.GrossWeightClass > 5000 => 10.00m + 5.00m,
        DeliveryTruck t when t.GrossWeightClass < 3000 => 10.00m - 2.00m,
        DeliveryTruck => 10.00m,

        { } => throw new ArgumentException("Not a known vehicle type", nameof(vehicle)),
        null => throw new ArgumentNullException(nameof(vehicle))
    };

    public decimal PeakTimePremiumIfElse(DateTime timeOfToll, bool inbound)
    {
        if (timeOfToll.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return 1.0m;
        }
        else
        {
            int hour = timeOfToll.Hour;
            if (hour < 6)
            {
                return 0.75m;
            }
            else if (hour < 10)
            {
                if (inbound)
                {
                    return 2.0m;
                }
                else
                {
                    return 1.0m;
                }
            }
            else if (hour < 16)
            {
                return 1.5m;
            }
            else if (hour < 20)
            {
                if (inbound)
                {
                    return 1.0m;
                }
                else
                {
                    return 2.0m;
                }
            }
            else
            {
                return 0.75m;
            }
        }
    }

    public decimal PeakTimePremium(DateTime timeOfToll, bool inbound) => (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
    {
        (true, TimeBand.Overnight, _) => 0.75m,
        (true, TimeBand.Daytime, _) => 1.5m,
        (true, TimeBand.MorningRush, true) => 2.00m,
        (true, TimeBand.EveningRush, false) => 2.00m,
        _ => 1.0m
    };

    private static bool IsWeekDay(DateTime timeOfToll) => timeOfToll.DayOfWeek switch
    {
        DayOfWeek.Saturday => false,
        DayOfWeek.Sunday => false,
        _ => true
    };

    private static TimeBand GetTimeBand(DateTime timeOfToll) => timeOfToll.Hour switch
    {
        < 6 or > 19 => TimeBand.Overnight,
        < 10 => TimeBand.MorningRush,
        < 16 => TimeBand.Daytime,
        _ => TimeBand.EveningRush
    };

    private enum TimeBand
    {
        MorningRush,
        Daytime,
        EveningRush,
        Overnight
    }
}
