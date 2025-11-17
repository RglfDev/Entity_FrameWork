
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/30/2025 11:38:33
-- Generated from EDMX file: C:\Users\LOBEZNO\Desktop\Developer\Tajamar\C#\EntityFrameWork\pruebaEF\pruebaEF\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Base_framework];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id_Client] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id_Product] int IDENTITY(1,1) NOT NULL,
    [ProductName] nvarchar(max)  NOT NULL,
    [UnitPrice] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id_Order] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Client] int  NOT NULL,
    [Product] int  NOT NULL,
    [Products_Id_Product] int  NOT NULL,
    [Clients_Id_Client] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id_Client] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id_Client] ASC);
GO

-- Creating primary key on [Id_Product] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id_Product] ASC);
GO

-- Creating primary key on [Id_Order] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id_Order] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Products_Id_Product] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_ProductsOrders]
    FOREIGN KEY ([Products_Id_Product])
    REFERENCES [dbo].[Products]
        ([Id_Product])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductsOrders'
CREATE INDEX [IX_FK_ProductsOrders]
ON [dbo].[Orders]
    ([Products_Id_Product]);
GO

-- Creating foreign key on [Clients_Id_Client] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_ClientsOrders]
    FOREIGN KEY ([Clients_Id_Client])
    REFERENCES [dbo].[Clients]
        ([Id_Client])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientsOrders'
CREATE INDEX [IX_FK_ClientsOrders]
ON [dbo].[Orders]
    ([Clients_Id_Client]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------