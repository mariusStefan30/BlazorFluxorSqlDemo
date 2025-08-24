using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorFluxorSqlDemo.Features.Items;

public record LoadItemsAction;
public record LoadItemsSuccessAction(IReadOnlyList<ItemDto> Items);
public record LoadItemsFailureAction(string Error);

public record AddItemAction(string Name);
public record AddItemSuccessAction(ItemDto Item);
public record AddItemFailureAction(string Error);

public record DeleteItemAction(int Id);
public record DeleteItemSuccessAction(int Id);
public record DeleteItemFailureAction(string Error);

// Pornește/Oprește auto-refresh (polling)
public record TogglePollingAction(bool Enable);
