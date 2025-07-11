using Store.G01.Core.Entities;
using Store.G01.Core.Entities.Order;
using Store.G01.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G01.Repository.Data
{
	public class StoreDbContextSeed
	{
		public async static Task SeedAsync(StoreDbContext _context)
		{
			if(_context.Brands.Count()==0)
			{
				// Brand
				// 1.Read Data From Json File
				var brandsData = File.ReadAllText(@"..\Store.G01.Repository\Data\DataSeed\brands.json");
				//C:\Users\20110\OneDrive\Documents\Store.G01\Store.G01.Repository\Data\DataSeed\Brands.json

				// 2.Convert Json String To List<T>
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				// 3.Seed Data To DB
				if (brands is not null && brands.Count() > 0)
				{
					await _context.Brands.AddRangeAsync(brands);
					await _context.SaveChangesAsync();
				}
			}

			if (_context.Types.Count() == 0)
			{
				// Types
				// 1.Read Data From Json File
				var typesData = File.ReadAllText(@"..\Store.G01.Repository\Data\DataSeed\types.json");
				//C:\Users\20110\OneDrive\Documents\Store.G01\Store.G01.Repository\Data\DataSeed\types.json

				// 2.Convert Json String To List<T>
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

				// 3.Seed Data To DB
				if (types is not null && types.Count() > 0)
				{
					await _context.Types.AddRangeAsync(types);
					await _context.SaveChangesAsync();
				}
			}

			if (_context.Products.Count() == 0)
			{
				// Product
				// 1.Read Data From Json File
				var productsData = File.ReadAllText(@"..\Store.G01.Repository\Data\DataSeed\products.json");
				//C:\Users\20110\OneDrive\Documents\Store.G01\Store.G01.Repository\Data\DataSeed\Brands.json

				// 2.Convert Json String To List<T>
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				// 3.Seed Data To DB
				if (products is not null && products.Count() > 0)
				{
					await _context.Products.AddRangeAsync(products);
				}
			}

            if (_context.DeliveryMethods.Count() == 0)
            {
                // Product
                // 1.Read Data From Json File
                var deliveryData = File.ReadAllText(@"..\Store.G01.Repository\Data\DataSeed\delivery.json");
                //C:\Users\20110\OneDrive\Documents\Store.G01\Store.G01.Repository\Data\DataSeed\Brands.json

                // 2.Convert Json String To List<T>
                var deliveryMethodes = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                // 3.Seed Data To DB
                if (deliveryMethodes is not null && deliveryMethodes.Count() > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethodes);
                }
            }


            await _context.SaveChangesAsync();
		}
	}
}
