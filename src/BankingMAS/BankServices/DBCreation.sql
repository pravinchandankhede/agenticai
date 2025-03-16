CREATE DATABASE BankingDB
GO

USE BankingDB
GO

-- Account Table
CREATE TABLE Account (
    AccountId INT PRIMARY KEY IDENTITY(1,1),
    AccountNumber NVARCHAR(50) NOT NULL,
    AccountType NVARCHAR(50) NOT NULL,
    Balance DECIMAL(18, 2) NOT NULL,
    CustomerId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Credit Card Table
CREATE TABLE CreditCard (
    CreditCardId INT PRIMARY KEY IDENTITY(1,1),
    CardNumber NVARCHAR(50) NOT NULL,
    CardType NVARCHAR(50) NOT NULL,
    ExpiryDate DATE NOT NULL,
    CreditLimit DECIMAL(18, 2) NOT NULL,
    CustomerId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Policy Table
CREATE TABLE Policy (
    PolicyId INT PRIMARY KEY IDENTITY(1,1),
    PolicyNumber NVARCHAR(50) NOT NULL,
    PolicyType NVARCHAR(50) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    PremiumAmount DECIMAL(18, 2) NOT NULL,
    CustomerId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Loan Application Table
CREATE TABLE LoanApplication (
    LoanApplicationId INT PRIMARY KEY IDENTITY(1,1),
    ApplicationNumber NVARCHAR(50) NOT NULL,
    LoanAmount DECIMAL(18, 2) NOT NULL,
    LoanType NVARCHAR(50) NOT NULL,
    ApplicationDate DATE NOT NULL,
    CustomerId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Credit Checking Table
CREATE TABLE CreditChecking (
    CreditCheckingId INT PRIMARY KEY IDENTITY(1,1),
    CheckDate DATE NOT NULL,
    CreditScore INT NOT NULL,
    CustomerId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Payment Table
CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY IDENTITY(1,1),
    PaymentDate DATE NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    AccountId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Customer Table
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(15),
    Address NVARCHAR(255),
    City NVARCHAR(50),
    State NVARCHAR(50),
    ZipCode NVARCHAR(10),
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Transaction Table
CREATE TABLE [Transaction] (
    TransactionId INT PRIMARY KEY IDENTITY(1,1),
    TransactionDate DATE NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    TransactionType NVARCHAR(50) NOT NULL,
    AccountId INT NOT NULL,
    CustomerId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (AccountId) REFERENCES Account(AccountId),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

CREATE TABLE Invoice (
    InvoiceId INT PRIMARY KEY IDENTITY(1,1),
    InvoiceNumber NVARCHAR(50) NOT NULL,
    InvoiceType NVARCHAR(50) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    CustomerId INT NOT NULL,
    RowVersion ROWVERSION,
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedOn DATETIME,
    IsActive BIT NOT NULL DEFAULT 1
);