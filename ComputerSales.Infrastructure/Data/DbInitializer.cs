using ComputerSalesAPI.Core.Entities;
using ComputerSalesAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ComputerSalesAPI.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 0. Ensure database is created/migrated
            await context.Database.MigrateAsync();

            // 1. Create Roles
            string[] roleNames = { "Admin", "Customer" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Create Admin User
            var adminEmail = "admin@ankhecomputer.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // 3. Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Laptop" },
                    new Category { Name = "PC - Màn hình" },
                    new Category { Name = "Linh kiện" },
                    new Category { Name = "Phụ kiện" }
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            // 4. Seed Products
            if (!context.Products.Any())
            {
                var firstCategory = context.Categories.First();
                var products = new List<Product>
                {
                    new Product 
                    { 
                        Name = "Laptop MacBook Air M2 2022", 
                        Description = "Chip Apple M2, RAM 8GB, SSD 256GB",
                        Price = 26990000, 
                        CategoryId = firstCategory.Id,
                        IsHot = true
                    },
                    new Product 
                    { 
                        Name = "Laptop ASUS TUF Gaming F15", 
                        Description = "Core i5-11400H, RTX 3050, 8GB RAM",
                        Price = 18490000, 
                        CategoryId = firstCategory.Id,
                        IsHot = true
                    }
                };
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            // 5. Seed News Categories
            if (!context.NewsCategories.Any())
            {
                var newsCategories = new List<NewsCategory>
                {
                    new NewsCategory { Name = "Tin khuyến mãi", OrderBy = 1 },
                    new NewsCategory { Name = "Tin công nghệ", OrderBy = 2 }
                };
                context.NewsCategories.AddRange(newsCategories);
                await context.SaveChangesAsync();
            }

            // 6. Seed News
            if (!context.News.Any())
            {
                var firstNewsCategory = context.NewsCategories.First();
                var news = new List<News>
                {
                    new News
                    {
                        Title = "Chào mừng An Khê Computer chính thức khai trương",
                        Description = "Hệ thống bán lẻ máy tính hàng đầu đã có mặt tại An Khê với nhiều ưu đãi hấp dẫn.",
                        Content = "<p>Chào mừng quý khách đến với lễ khai trương An Khê Computer vào ngày 25/03/2026. Hàng ngàn phần quà đang chờ đón!</p>",
                        ImageUrl = "https://cdn.tgdd.vn/Files/2023/08/21/1543305/le-khai-truong-dien-may-xanh-the-gioi-di-dong-1.jpg",
                        CategoryId = firstNewsCategory.Id,
                        CreatedDate = DateTime.Now
                    },
                    new News
                    {
                        Title = "Top 5 Laptop Gaming đáng mua nhất 2026",
                        Description = "Khám phá danh sách những mẫu laptop gaming mạnh mẽ và giá tốt nhất hiện nay.",
                        Content = "<p>Năm 2026 chứng kiến sự bùng nổ của các dòng laptop gaming với card đồ họa RTX 50 series...</p>",
                        ImageUrl = "https://cdn.tgdd.vn/Files/2023/07/04/1536773/laptop-gaming-duoi-20-trieu-dang-mua-7.jpg",
                        CategoryId = context.NewsCategories.Skip(1).First().Id,
                        CreatedDate = DateTime.Now.AddDays(-1)
                    }
                };
                context.News.AddRange(news);
                await context.SaveChangesAsync();
            }

            // 7. Seed Banners
            if (!context.Banners.Any())
            {
                var banners = new List<Banner>
                {
                    new Banner
                    {
                        Name = "SIÊU SALE LAPTOP GAMING",
                        Info = "Giảm giá lên đến 30% cho các dòng Laptop Gaming hot nhất 2026.",
                        ImageUrl = "https://cdn.tgdd.vn/2024/03/banner/Laptop-Gaming-Moi-Gia-Re-800x450.jpg",
                        LinkUrl = "/laptop",
                        Status = "Active"
                    },
                    new Banner
                    {
                        Name = "AN KHÊ COMPUTER",
                        Info = "Chào mừng bạn đến với hệ thống máy tính chuyên nghiệp nhất.",
                        ImageUrl = "https://cdn.tgdd.vn/2024/03/banner/Build-PC-Moi-800x450.jpg",
                        LinkUrl = "/pc",
                        Status = "Active"
                    },
                    new Banner
                    {
                        Name = "MACBOOK M3 SERIES MỚI NHẤT",
                        Info = "Trải nghiệm sức mạnh đột phá từ chip M3. Trả góp 0% lãi suất.",
                        ImageUrl = "https://cdn.tgdd.vn/2024/03/banner/MacBook-M3-Moi-800x450.jpg",
                        LinkUrl = "/products",
                        Status = "Active"
                    }
                };
                context.Banners.AddRange(banners);
                await context.SaveChangesAsync();
            }
        }
    }
}
