using Fluxor;
using System;                       // pentru DateTime
using System.Collections.Generic;   // pentru IReadOnlyList<T>


namespace BlazorFluxorSqlDemo.Features.Items;

public record ItemDto(int Id, string Name, DateTime CreatedAt);

[FeatureState]
public record ItemsState
{
    public bool IsLoading { get; init; }
    public bool IsPolling { get; init; }
    public IReadOnlyList<ItemDto>? Items { get; init; }
    public string? Error { get; init; }

    // ✅ Necesat de Fluxor pentru starea inițială
    private ItemsState()
    {
        IsLoading = false;
        IsPolling = false;
        Items = null;
        Error = null;
    }

    // (opțional) ctor complet dacă îl folosești în reducere/efecte
    public ItemsState(bool isLoading, bool isPolling,
                      IReadOnlyList<ItemDto>? items, string? error)
    {
        IsLoading = isLoading;
        IsPolling = isPolling;
        Items = items;
        Error = error;
    }
}
