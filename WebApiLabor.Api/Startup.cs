﻿using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApiLabor.Bll.Services;
using WebApiLabor.DAL;

namespace WebApiLabor.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(
                    json => json.SerializerSettings.ReferenceLoopHandling 
                            = ReferenceLoopHandling.Ignore);

            services.AddDbContext<NorthwindContext>(o =>
                o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"])
                .ConfigureWarnings(c => c.Throw(RelationalEventId.QueryClientEvaluationWarning)));

            services.AddTransient<IProductService, ProductService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Entities.Product, Dtos.Product>()
                    .ForMember(dto => dto.Orders, opt => opt.Ignore())
                    .AfterMap((p, dto, ctx) =>
                        dto.Orders = p.ProductOrders.Select(po =>
                        ctx.Mapper.Map<Dtos.Order>(po.Order)).ToList()).ReverseMap();                    
                cfg.CreateMap<Entities.Order, Dtos.Order>().ReverseMap();                    
                cfg.CreateMap<Entities.Category, Dtos.Category>().ReverseMap();
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
