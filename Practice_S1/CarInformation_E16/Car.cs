namespace CarInformation_E16;

public class Car
{
    public string Model { get; }
    public int Year { get; }
    public Engine Engine { get; } 

    public Car(string model, int year, Engine engine)
    {
        Model = model;
        Year = year;
        Engine = engine;
    }

    private (bool eligible, string pack) GetRablaEligibility()
    {
        if (Engine.Type == EngineType.Diesel)
            return (false, "Not eligible: diesel");

        if (Engine.Type == EngineType.Electric)
            return (true, "Rabla Plus (EV)");

        if (Engine.Type == EngineType.PlugInHybrid)
            return Engine.CO2 < 80
                ? (true, "Rabla Plus (PHEV < 80 g/km)")
                : (false, "PHEV not eligible: CO₂ must be < 80 g/km");

        bool isClasicType = Engine.Type is EngineType.Hybrid or EngineType.Hybrid or EngineType.LPG or EngineType.CNG;
        if (isClasicType)
        {
            if (Engine.CO2 <= 145)
            {
                // eco-bonus hints (<120g and/or LPG/CNG)
                string extras = Engine.CO2 < 120
                    ? " (+eco-bonus <120 g/km)"
                    : "";

                if (Engine.Type is EngineType.LPG or EngineType.CNG)
                    extras += " (+eco-bonus LPG/CNG)";

                return (true, $"Rabla Clasic{extras}");
            }
            return (false, "Rabla Clasic not eligible: CO₂ must be ≤ 145 g/km");
        }

        return (false, "Unknown powertrain");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"** {Model}, {Year} **");
        Console.WriteLine($"Engine Type: {Engine.Type.ToString()}");
        Console.WriteLine($"Consumption: {Engine.Comsumption}");
        Console.WriteLine($"CO2 Emition: {Engine.CO2}");
        var rabla = GetRablaEligibility();
        if (rabla.eligible) Console.WriteLine($"** Eligible for Rabla program: {rabla.pack} **");
    }
}
