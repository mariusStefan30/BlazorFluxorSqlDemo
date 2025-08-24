using BlazorFluxorSqlDemo.Data;
using Fluxor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BlazorFluxorSqlDemo.Features.Items;

public class ItemsEffects : System.IDisposable
{
	private readonly ItemsService _svc;
	private readonly IDispatcher _dispatcher;
	private System.Threading.Timer? _timer;
	//constructor with two parameters
	public ItemsEffects(ItemsService svc, IDispatcher dispatcher)
	{
		_svc = svc;
		_dispatcher = dispatcher;
	}

	[EffectMethod]
	public async Task HandleLoad(LoadItemsAction action, IDispatcher dispatcher)
	{
		try
		{
			var data = await _svc.GetAllAsync();
			var dto = data.Select(i => new ItemDto(i.Id, i.Name, i.CreatedAt)).ToList();
			dispatcher.Dispatch(new LoadItemsSuccessAction(dto));
            Debug.WriteLine("LoadItemsAction received");
        }
		catch (Exception ex)
		{
			dispatcher.Dispatch(new LoadItemsFailureAction(ex.Message));
		}
	}

	[EffectMethod]
	public async Task HandleAdd(AddItemAction action, IDispatcher dispatcher)
	{
		try
		{
			var created = await _svc.AddAsync(action.Name);
			dispatcher.Dispatch(new AddItemSuccessAction(new ItemDto(created.Id, created.Name, created.CreatedAt)));
			// Re-încarcă întreaga listă pentru consistență
			dispatcher.Dispatch(new LoadItemsAction());
		}
		catch (Exception ex)
		{
			dispatcher.Dispatch(new AddItemFailureAction(ex.Message));
		}
	}

	[EffectMethod]
	public async Task HandleDelete(DeleteItemAction action, IDispatcher dispatcher)
	{
		try
		{
			await _svc.DeleteAsync(action.Id);
			dispatcher.Dispatch(new DeleteItemSuccessAction(action.Id));
		}
		catch (Exception ex)
		{
			dispatcher.Dispatch(new DeleteItemFailureAction(ex.Message));
		}
	}

	// Pornește/oprește un polling simplu (ex: la 2 sec) ca să vezi update-urile din DB
	[EffectMethod]
	public Task HandleTogglePolling(TogglePollingAction action, IDispatcher dispatcher)
	{
		if (action.Enable)
		{
			_timer ??= new System.Threading.Timer(_ =>
			{
				try { _dispatcher.Dispatch(new LoadItemsAction()); }
				catch { /* ignore */ }
			}, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
		}
		else
		{
			_timer?.Change(Timeout.Infinite, Timeout.Infinite);
			_timer?.Dispose();
			_timer = null;
		}
		return Task.CompletedTask;
	}

	public void Dispose()
	{
		_timer?.Dispose();
	}
}
