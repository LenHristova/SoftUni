namespace ProductShop.App
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            InitializeDatabase(context);

            var importer = new Importer(context);
            var exporter = new Exporter(context);

            importer.ImportUsers();
            importer.ImporProducts();
            importer.ImportCategories();
            importer.ImportCategoryProducts();

            exporter.ExportProductsInRange(500, 1000);
            exporter.ExportSoldProducts();
            exporter.ExportCategoriesByProductsCount();
            exporter.ExportUsersAndProducts();
        }

        private static void InitializeDatabase(ProductShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
