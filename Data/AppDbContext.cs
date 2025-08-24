using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorFluxorSqlDemo.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	public DbSet<Item> Items => Set<Item>();
}

public class Item
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
