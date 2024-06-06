-- Criar o banco de dados se ele não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PeckProductsDB')
BEGIN
    CREATE DATABASE PeckProductsDB;
END
GO

-- Usar o banco de dados criado
USE PeckProductsDB;
GO

-- Criar a tabela ProdutoPack
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProdutoPack')
BEGIN
    CREATE TABLE ProdutoPack (
        Id INT PRIMARY KEY IDENTITY,
        Nome NVARCHAR(100) NOT NULL,
        QuantidadeUnidades INT NOT NULL,
        Valor DECIMAL(10, 2) NOT NULL
    );
END
GO

-- Criar a tabela Sabor
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Sabor')
BEGIN
    CREATE TABLE Sabor (
        Id INT PRIMARY KEY IDENTITY,
        Nome NVARCHAR(100) NOT NULL,
        ProdutoPackId INT FOREIGN KEY REFERENCES ProdutoPack(Id)
    );
END
GO
