# BlazorFluxorSqlDemo


Un demo simplu care arată cum să folosești **Blazor Server + Fluxor (state management) + SQL Server (EF Core)** pentru a gestiona o listă de itemi (`Items`).

## 📌 Funcționalități

- UI în **Blazor Server** (ASP.NET Core 8).
- **Fluxor** pentru state management (pattern Redux).
- **Entity Framework Core** pentru acces la SQL Server.
- Operații suportate:
  - 📥 Load items din DB
  - 🔄 Live refresh (polling)
  - ➕ Add item
  - ❌ Delete item
- Integrare cu **Redux DevTools** pentru debugging.

---

## 🚀 Cum rulezi proiectul

### 1. Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server 2019/2022 Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) sau localdb
- [Visual Studio 2022](https://visualstudio.microsoft.com/) cu workload **ASP.NET + web development**

### 2. Clone & Restore
```bash
git clone https://github.com/tu-user/BlazorFluxorSqlDemo.git
cd BlazorFluxorSqlDemo
dotnet restore


📂 Structura proiectului

Data/

AppDbContext.cs – EF Core DbContext

Item.cs – modelul DB

ItemsService.cs – serviciu pentru CRUD

Features/Items/

ItemsState.cs – Fluxor state

ItemsActions.cs – definiții acțiuni (Load, Add, Delete, Polling)

ItemsReducers.cs – reduceri (cum schimbăm state-ul)

ItemsEffects.cs – efecte (apelează DB și dispatchează succes/eșec)

Pages/

Items.razor – UI pentru listă

Program.cs – setup servicii, EF Core, Fluxor


🔍 Cum funcționează Fluxor aici

UI (Items.razor) → dispatchează o acțiune (ex. LoadItemsAction).

Effect (ItemsEffects) → interceptează acțiunea, apelează ItemsService pentru SQL și dispatchează fie LoadItemsSuccessAction, fie LoadItemsFailureAction.

Reducer (ItemsReducers) → actualizează state-ul (ex. IsLoading=false, Items=data).

UI → e legat la IState<ItemsState> și se re-randează automat când state-ul se schimbă.