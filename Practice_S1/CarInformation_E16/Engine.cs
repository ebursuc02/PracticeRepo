namespace CarInformation_E16;

public enum EngineType
{
    Electric,
    PlugInHybrid,
    Hybrid,
    Petrol,
    LPG,
    CNG,
    Diesel
}
public class Engine
{
    public EngineType Type { get; }
    public double Comsumption { get; }
    public double CO2 { get; }

    public Engine(EngineType type, double comsumption, double cO2)
    {
        Type = type;
        Comsumption = comsumption;
        CO2 = cO2;
    }
}
