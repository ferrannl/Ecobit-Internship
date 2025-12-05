# Ecobit Internship – 3rd Year Internship Project

A C#/.NET multi-project solution created as part of a **third-year internship at Ecobit (Nijmegen)**.  
The project showcases a layered architecture with separation between **UI**, **Domain**, and **Database** logic.

---

## 📌 Table of Contents
- [Overview](#overview)
- [Project Structure](#project-structure)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Database Setup](#database-setup)
- [Why This Project Matters](#why-this-project-matters)
- [Project Status](#project-status)
- [Contact](#contact)

---

## ⭐ Overview

This repository contains a classic Visual Studio solution with multiple C# projects.  
The goal of the internship was to create a structured, maintainable, and realistic business application using proper **layer separation** and **clean architecture principles**.

The solution includes:

- A **UI / application entry** project  
- A **Domain** layer for business rules  
- A **Database** layer for persistence  
- An **Entities** layer that maps to actual database tables  

---

## 📂 Project Structure

Ecobit-Internship/ ├─ EcobitStage/           # Main UI / Application entry point ├─ Ecobit.Domain/         # Business logic, domain models, interfaces ├─ Ecobit.Database/       # Data-access, repositories, SQL handling ├─ Entities.Database/     # Database entity classes & mappings ├─ EcobitStage.sln        # Solution file ├─ .gitignore └─ .gitattributes

### **Project Descriptions**

### **EcobitStage**
- The main application project.
- Handles UI + program startup.
- Connects the domain layer with the database layer.

### **Ecobit.Domain**
- Pure business logic.
- Contains domain models, rules, and interfaces.
- Does **not** depend directly on the database.

### **Ecobit.Database**
- Implements repositories and data-access logic.
- Knows *how* data is stored (SQL, LocalDB, etc.).

### **Entities.Database**
- Classes representing database tables (POCOs).
- Structure should match your SQL schema.

---

## 🧱 Architecture

The solution follows a **layered architecture**:

UI  →  Domain  →  Database  →  SQL Server

### **Benefits**
- Easier maintenance  
- Testable business logic  
- Loose coupling  
- Realistic enterprise-style structure  

---

## 🛠 Technologies Used

- **Language:** C#
- **Framework:** .NET (classic desktop-style)
- **IDE:** Visual Studio 2019/2022
- **Database:** SQL Server / LocalDB
- **Version Control:** Git + GitHub

---

## 🚀 Getting Started

### **Requirements**
- Windows
- Visual Studio 2019/2022  
  → Workload: *".NET desktop development"*
- SQL Server or LocalDB

### **Clone the Repository**
```bash
git clone https://github.com/ferrannl/Ecobit-Internship.git
cd Ecobit-Internship

Open the Solution

1. Launch Visual Studio


2. Open EcobitStage.sln


3. Set EcobitStage as the Startup Project


4. Build the solution


5. Run the application (F5)




---

🗄 Database Setup

1. Create a database

Make a SQL Server / LocalDB database (e.g. EcobitStage)



2. Update the connection string

Found in configuration files (e.g. App.config)

Update server + database name to match your local setup



3. Create tables

Tables should match the classes in Entities.Database

Make sure primary keys & relationships match



4. Run the application

If there are errors → check connection string + schema





---

🎓 Why This Project Matters

This repository is useful for:

Students learning architecture in .NET

Developers who want a clean, layered example

Anyone exploring how UI, domain logic, and data persistence fit together

A base for experimenting with:

Dependency Injection

Testing

New UI frameworks

Improved database handling




---

📌 Project Status

This is a student internship project, not an actively maintained production application.

No open-source license is currently included

Intended as learning/reference material

For commercial usage, contact the repository owner



---

📧 Contact

Repository Owner: @ferrannl

Feel free to open an issue or fork the repo if you'd like to experiment or contribute.


---
