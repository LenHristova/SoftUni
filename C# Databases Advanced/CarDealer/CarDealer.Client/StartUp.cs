namespace CarDealer.Client
{
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();

            InitializeDatabase(context);

            var mapper = InitializeMapper();
            var importer = new Importer(context, mapper);
            importer.ImportSuppliers();
            importer.ImportParts();
            importer.ImportCars();
            importer.ImportCarsParts();
            importer.ImportCustomers();
            importer.ImportSales();

            var exporter = new Exporter(context);
            exporter.ExportCarsByDistance(2000000);
            exporter.ExportCarsByМake("Ferrari");
            exporter.ExportLocalSuppliers();
            exporter.ExportCarsWithParts();
            exporter.ExportSalesByCustomer();
            exporter.ExportSalesWithDiscounts();
        }

        private static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }

        private static void InitializeDatabase(CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
