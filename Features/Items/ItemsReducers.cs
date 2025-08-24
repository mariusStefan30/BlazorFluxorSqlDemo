using Fluxor;
using System;
using System.Linq;


namespace BlazorFluxorSqlDemo.Features.Items;

public static class ItemsReducers
{
    [ReducerMethod]
    public static ItemsState OnLoad(ItemsState s, LoadItemsAction _)
        => s with { IsLoading = true, Error = null };

    [ReducerMethod]
    public static ItemsState OnLoadSuccess(ItemsState s, LoadItemsSuccessAction a)
        => s with { IsLoading = false, Items = a.Items, Error = null };

    [ReducerMethod]
    public static ItemsState OnLoadFailure(ItemsState s, LoadItemsFailureAction a)
        => s with { IsLoading = false, Error = a.Error };

    [ReducerMethod]
    public static ItemsState OnAddSuccess(ItemsState s, AddItemSuccessAction a)
        => s with { Items = (new[] { a.Item }).Concat(s.Items ?? Array.Empty<ItemDto>()).ToList() };

    [ReducerMethod]
    public static ItemsState OnDeleteSuccess(ItemsState s, DeleteItemSuccessAction a)
        => s with { Items = (s.Items ?? Array.Empty<ItemDto>()).Where(i => i.Id != a.Id).ToList() };

    [ReducerMethod]
    public static ItemsState OnTogglePolling(ItemsState s, TogglePollingAction a)
        => s with { IsPolling = a.Enable };
}
