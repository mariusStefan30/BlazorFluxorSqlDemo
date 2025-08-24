using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BlazorFluxorSqlDemo.Data;

public class ItemsService
{
	private readonly AppDbContext _db;
	public ItemsService(AppDbContext db) => _db = db;

	public async Task<List<Item>> GetAllAsync(CancellationToken ct = default) =>
		await _db.Items.OrderByDescending(i => i.CreatedAt).ToListAsync(ct);

	public async Task<Item> AddAsync(string name, CancellationToken ct = default)
	{
		var entity = new Item { Name = name, CreatedAt = DateTime.UtcNow };
		_db.Items.Add(entity);
		await _db.SaveChangesAsync(ct);
		return entity;
	}

	public async Task DeleteAsync(int id, CancellationToken ct = default)
	{
		var entity = await _db.Items.FindAsync(new object?[] { id }, ct);
		if (entity is null) return;
		_db.Items.Remove(entity);
		await _db.SaveChangesAsync(ct);
	}
}
