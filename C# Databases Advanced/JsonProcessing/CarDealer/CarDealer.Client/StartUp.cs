namespace CarDealer.Client
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();

            InitializeDatabase(context);

            var importer = new Importer(context);
            importer.ImportSuppliers();
            importer.ImportParts();
            importer.ImportCars();
            importer.ImportCustomers();

            var exporter = new Exporter(context);
            exporter.ExportOrderedCustomers();
            exporter.ExportCarsByMake("Toyota");
            exporter.ExportLocalSuppliers();
            exporter.ExportCarsAndParts();
            exporter.ExportTotalSalesByCustomer();
            exporter.ExportSalesWithAppliedDiscount();
        }

        private static void InitializeDatabase(CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
