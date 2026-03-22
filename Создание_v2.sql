-- GreenCityManagement v2 — полная схема БД
-- Запустить в SQL Server Management Studio для базы GreenCityManagement
-- Пересоздаёт все таблицы с нуля

USE GreenCityManagement;
GO

-- Удаление старых таблиц (если есть)
IF OBJECT_ID('AppUser',       'U') IS NOT NULL DROP TABLE AppUser;
IF OBJECT_ID('Plant_Employee','U') IS NOT NULL DROP TABLE Plant_Employee;
IF OBJECT_ID('Maintenance',   'U') IS NOT NULL DROP TABLE Maintenance;
IF OBJECT_ID('PlantPassport', 'U') IS NOT NULL DROP TABLE PlantPassport;
IF OBJECT_ID('Plant',         'U') IS NOT NULL DROP TABLE Plant;
IF OBJECT_ID('Role',          'U') IS NOT NULL DROP TABLE [Role];
IF OBJECT_ID('WorkType',      'U') IS NOT NULL DROP TABLE WorkType;
IF OBJECT_ID('Employee',      'U') IS NOT NULL DROP TABLE Employee;
IF OBJECT_ID('PlantType',     'U') IS NOT NULL DROP TABLE PlantType;
IF OBJECT_ID('District',      'U') IS NOT NULL DROP TABLE District;
IF OBJECT_ID('__EFMigrationsHistory','U') IS NOT NULL DROP TABLE __EFMigrationsHistory;
GO

-- Районы
CREATE TABLE District (
    ID_district  INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(200) NOT NULL,
    Description  NVARCHAR(500) NULL
);

-- Типы растений
CREATE TABLE PlantType (
    ID_plant_type INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(200) NOT NULL,
    Description   NVARCHAR(500) NULL
);

-- Сотрудники
CREATE TABLE Employee (
    ID_employee INT IDENTITY(1,1) PRIMARY KEY,
    FirstName   NVARCHAR(100) NOT NULL,
    LastName    NVARCHAR(100) NOT NULL,
    Surname     NVARCHAR(100) NOT NULL,
    Position    NVARCHAR(200) NOT NULL,
    Phone       NVARCHAR(50)  NOT NULL
);

-- Виды работ
CREATE TABLE WorkType (
    ID_work_type INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(200) NOT NULL,
    Description  NVARCHAR(500) NULL
);

-- Роли пользователей
CREATE TABLE [Role] (
    ID_role     INT IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(100) NOT NULL,
    Description NVARCHAR(300) NULL
);

-- Растения (N:1 к PlantType и District)
CREATE TABLE Plant (
    ID_plant      INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(200) NOT NULL,
    ID_plant_type INT NOT NULL REFERENCES PlantType(ID_plant_type),
    ID_district   INT NOT NULL REFERENCES District(ID_district),
    Planting_date DATETIME2     NOT NULL,
    Health_status NVARCHAR(100) NOT NULL
);

-- Паспорта растений (1:1 к Plant)
CREATE TABLE PlantPassport (
    ID_passport          INT IDENTITY(1,1) PRIMARY KEY,
    ID_plant             INT UNIQUE NOT NULL REFERENCES Plant(ID_plant) ON DELETE CASCADE,
    Height               DECIMAL(5,2) NOT NULL,
    Age                  INT          NOT NULL,
    Last_inspection_date DATETIME2    NOT NULL
);

-- Обслуживание (N:1 к Plant, WorkType, Employee)
CREATE TABLE Maintenance (
    ID_maintenance INT IDENTITY(1,1) PRIMARY KEY,
    ID_plant       INT           NOT NULL REFERENCES Plant(ID_plant),
    ID_work_type   INT           NOT NULL REFERENCES WorkType(ID_work_type),
    ID_employee    INT           NOT NULL REFERENCES Employee(ID_employee),
    Work_date      DATETIME2     NOT NULL,
    Result         NVARCHAR(500) NOT NULL
);

-- Растение-Сотрудник (связь назначенных сотрудников)
CREATE TABLE Plant_Employee (
    ID_plant_employee INT IDENTITY(1,1) PRIMARY KEY,
    ID_plant          INT NOT NULL REFERENCES Plant(ID_plant)    ON DELETE CASCADE,
    ID_employee       INT NOT NULL REFERENCES Employee(ID_employee) ON DELETE CASCADE
);

-- Пользователи приложения
CREATE TABLE AppUser (
    ID_user      INT IDENTITY(1,1) PRIMARY KEY,
    Login        NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(200) NOT NULL,
    FullName     NVARCHAR(300) NOT NULL,
    ID_role      INT NOT NULL REFERENCES [Role](ID_role)
);

-- Таблица для EF миграций
CREATE TABLE __EFMigrationsHistory (
    MigrationId    NVARCHAR(150) NOT NULL PRIMARY KEY,
    ProductVersion NVARCHAR(32)  NOT NULL
);
INSERT INTO __EFMigrationsHistory VALUES ('20260316000000_FullSchema', '9.0.0');
GO

-- Начальные данные: роли
SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role] (ID_role, Name, Description) VALUES
(1, N'Администратор',     N'Полный доступ ко всем функциям'),
(2, N'Работник',          N'Просмотр и добавление обслуживания'),
(3, N'Менеджер по учёту', N'Справочники, растения и отчёты');
SET IDENTITY_INSERT [Role] OFF;

-- Начальный пользователь: admin / admin123
-- PasswordHash = SHA-256("admin123")
SET IDENTITY_INSERT AppUser ON;
INSERT INTO AppUser (ID_user, Login, PasswordHash, FullName, ID_role) VALUES
(1, 'admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
 N'Администратор системы', 1);
SET IDENTITY_INSERT AppUser OFF;
GO

PRINT 'Схема GreenCityManagement v2 успешно создана.';
PRINT 'Логин: admin | Пароль: admin123';
GO
