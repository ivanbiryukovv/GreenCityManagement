IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GreenCityManagement')
BEGIN
    CREATE DATABASE GreenCityManagement;
END
GO

USE GreenCityManagement;
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Plant_Employee')
    DROP TABLE Plant_Employee;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Maintenance')
    DROP TABLE Maintenance;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PlantPassport')
    DROP TABLE PlantPassport;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Plant')
    DROP TABLE Plant;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Employee')
    DROP TABLE Employee;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'WorkType')
    DROP TABLE WorkType;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'District')
    DROP TABLE District;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PlantType')
    DROP TABLE PlantType;
GO

CREATE TABLE District
(
    ID_district INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200) NULL
);
GO

CREATE TABLE PlantType
(
    ID_plant_type INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200) NULL
);
GO

CREATE TABLE Plant
(
    ID_plant INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    ID_plant_type INT NOT NULL,
    ID_district INT NOT NULL,
    Planting_date DATE NOT NULL,
    Health_status NVARCHAR(20) NOT NULL
        CHECK (Health_status IN ('Отличное','Хорошее','Удовлетворительное','Плохое')),
    Latitude DECIMAL(9,6) NOT NULL,
    Longitude DECIMAL(9,6) NOT NULL,

    CONSTRAINT FK_Plant_PlantType FOREIGN KEY (ID_plant_type)
        REFERENCES PlantType(ID_plant_type),

    CONSTRAINT FK_Plant_District FOREIGN KEY (ID_district)
        REFERENCES District(ID_district)
);
GO

CREATE TABLE PlantPassport
(
    ID_passport INT IDENTITY(1,1) PRIMARY KEY,
    ID_plant INT NOT NULL UNIQUE,
    Height DECIMAL(5,2) NOT NULL
        CHECK (Height > 0),
    Age INT NOT NULL
        CHECK (Age >= 0),
    Last_inspection_date DATE NOT NULL,

    CONSTRAINT FK_PlantPassport_Plant FOREIGN KEY (ID_plant)
        REFERENCES Plant(ID_plant)
        ON DELETE CASCADE
);
GO

CREATE TABLE Employee
(
    ID_employee INT IDENTITY(1,1) PRIMARY KEY,
    Full_name NVARCHAR(100) NOT NULL,
    Position NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(11) NOT NULL UNIQUE
        CHECK (LEN(Phone) = 11)
);
GO

CREATE TABLE WorkType
(
    ID_work_type INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200) NULL
);
GO

CREATE TABLE Maintenance
(
    ID_maintenance INT IDENTITY(1,1) PRIMARY KEY,
    ID_plant INT NOT NULL,
    ID_work_type INT NOT NULL,
    Work_date DATE NOT NULL,
    Result NVARCHAR(100) NOT NULL,

    CONSTRAINT FK_Maintenance_Plant FOREIGN KEY (ID_plant)
        REFERENCES Plant(ID_plant)
        ON DELETE CASCADE,

    CONSTRAINT FK_Maintenance_WorkType FOREIGN KEY (ID_work_type)
        REFERENCES WorkType(ID_work_type)
);
GO

CREATE TABLE Plant_Employee
(
    ID_plant_employee INT IDENTITY(1,1) PRIMARY KEY,
    ID_plant INT NOT NULL,
    ID_employee INT NOT NULL,

    CONSTRAINT FK_PlantEmployee_Plant FOREIGN KEY (ID_plant)
        REFERENCES Plant(ID_plant)
        ON DELETE CASCADE,

    CONSTRAINT FK_PlantEmployee_Employee FOREIGN KEY (ID_employee)
        REFERENCES Employee(ID_employee)
        ON DELETE CASCADE,

    CONSTRAINT UQ_PlantEmployee UNIQUE (ID_plant, ID_employee)
);
GO
