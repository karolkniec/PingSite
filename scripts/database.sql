CREATE DATABASE PingSite

USE PingSite

CREATE TABLE Buildings (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL
)

CREATE TABLE Rooms (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	BuildingId INT NOT NULL FOREIGN KEY REFERENCES Buildings(Id)
)

CREATE TABLE Categories (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	ImgUrl NVARCHAR(250) NOT NULL
)

CREATE TABLE Hosts (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	Address NVARCHAR(50) NOT NULL,
	LastStatus BIT,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	RoomId INT NOT NULL FOREIGN KEY REFERENCES Rooms(Id)
)