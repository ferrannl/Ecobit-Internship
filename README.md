# Ecobit Internship – 3rd Year Project

> _“3e jaars stageopdracht bij Ecobit Software (Nijmegen)”_  
> A multi-project C#/.NET solution built during a third-year internship at **Ecobit B.V.**, focused on exploring layered architectures, domain-driven design and database access in a realistic business setting.

---

## 🧩 Solution Overview

This repository contains a Visual Studio solution with multiple projects that separate concerns between:

- **Application / UI layer**
- **Domain / business logic**
- **Database / persistence**

The solution file:

- `EcobitStage.sln` – root solution that ties all projects together.

Main projects:

- `EcobitStage`  
  The main application project (UI / API / entry point), referencing the domain layer and database layer.

- `Ecobit.Domain`  
  Contains the **core business logic and domain models**.  
  This is where entities, value objects, domain services and business rules live.

- `Ecobit.Database`  
  Contains **database access logic** (e.g. repositories, context, queries) and configuration for the underlying database (typically SQL Server / LocalDB).

- `Entities.Database`  
  Defines the **database entities and mappings** that are persisted in the database.  
  These classes usually correspond closely to database tables and are used by the `Ecobit.Database` project.

All projects are written in **C#** and target the .NET ecosystem, using a classic Visual Studio `.sln` setup.

---

## ✨ Goals of the Internship Project

This project was created to:

- Learn and apply **layered architecture** in a real-world scenario.
- Separate **domain logic** from **infrastructure and persistence**.
- Work with **relational databases** from C#.
- Collaborate in a professional setting using **Git**, **GitHub** and Visual Studio.

---

## 🗂 Project Structure

```text
Ecobit-Internship/
├─ EcobitStage/           # Main application project (UI / entry point)
├─ Ecobit.Domain/         # Domain models, interfaces & business logic
├─ Ecobit.Database/       # Data access / repository implementations
├─ Entities.Database/     # Database entity classes & mappings
├─ EcobitStage.sln        # Visual Studio solution
├─ .gitignore
└─ .gitattributes
