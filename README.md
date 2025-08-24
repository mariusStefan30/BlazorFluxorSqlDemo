# BlazorFluxorSqlDemo

<img width="1912" height="1025" alt="image" src="https://github.com/user-attachments/assets/2a71e4c5-0006-4027-98d9-77831b133af5" />


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

git clone https://github.com/tu-user/BlazorFluxorSqlDemo.git
cd BlazorFluxorSqlDemo
dotnet restore

---

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

--- Screenshots

<img width="1917" height="1024" alt="image" src="https://github.com/user-attachments/assets/d9f91a9e-0ca8-43e6-9b0b-56f8f4a03ed8" />

Before loading from db:

<img width="1909" height="1013" alt="image" src="https://github.com/user-attachments/assets/47d4872d-1ed3-40c5-9690-4d22ac821c92" />


After loading:

<img width="1918" height="1026" alt="image" src="https://github.com/user-attachments/assets/72bc242e-57b7-412d-ac37-91632b72c7ac" />



