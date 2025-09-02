using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;
using Calculators;

TollCalculator tollCalculator = new();

Car car = new();
Taxi taxi = new();
Bus bus = new();
DeliveryTruck truck = new();

Console.WriteLine($"The toll for a car is {tollCalculator.CalculateToll(car)}");
Console.WriteLine($"The toll for a taxi is {tollCalculator.CalculateToll(taxi)}");
Console.WriteLine($"The toll for a bus is {tollCalculator.CalculateToll(bus)}");
Console.WriteLine($"The toll for a truck is {tollCalculator.CalculateToll(truck)}");

try
{
    tollCalculator.CalculateToll("This will fail");
}
catch (ArgumentException e)
{
    Console.WriteLine("Caught an argument exception when using the wrong type");
}

try
{
    tollCalculator.CalculateToll(null!);
}
catch (ArgumentNullException e)
{
    Console.WriteLine("Caught an argument exception when using null");
}
