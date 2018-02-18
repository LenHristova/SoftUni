namespace P02_CarsSalesman
{
    class StartUp
    {
        static void Main()
        {
            EnginesCatalog.LoadCatalog();
            CarsCatalog.LoadCatalog();
            CarsCatalog.DisplayCars();
        }
    }
}