using CarInformation_E16;

Engine engineElectric = new Engine(EngineType.Electric, 0, 0);
Engine engineDiesel = new Engine(EngineType.Diesel, 10.2, 150);
Engine engineHybrid = new Engine(EngineType.Hybrid, 5.6, 90);

Car carElectric = new Car("Tesla 3", 2020, engineElectric);
Car carDiesel = new Car("BMW series 3", 2012, engineDiesel);
Car carHybrid = new Car("BMW series 5", 2024, engineHybrid);

carElectric.DisplayInfo();
Console.WriteLine();

carDiesel.DisplayInfo();
Console.WriteLine();

carHybrid.DisplayInfo();