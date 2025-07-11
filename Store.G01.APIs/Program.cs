using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.G01.APIs.Errors;
using Store.G01.APIs.Middlewares;
using Store.G01.Core;
using Store.G01.Core.Mapping.Products;
using Store.G01.Core.Services.Contract;
using Store.G01.Repository;
using Store.G01.Repository.Data;
using Store.G01.Repository.Data.Contexts;
using Store.G01.Service.Services.Products;
using Store.G01.APIs.Helper;

namespace Store.G01.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddDepenency(builder.Configuration);
            

			var app = builder.Build();

			await app.ConfigureMiddlewareAsync();

            app.Run();
		}
	}
}
