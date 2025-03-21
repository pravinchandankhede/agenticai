INSERT INTO Customer (FirstName, LastName, DateOfBirth, Email, PhoneNumber, Address, City, State, ZipCode, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('John', 'Doe', '1980-01-01', 'john.doe@example.com', '1234567890', '123 Main St', 'Anytown', 'Anystate', '12345', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Jane', 'Smith', '1985-02-02', 'jane.smith@example.com', '2345678901', '456 Elm St', 'Othertown', 'Otherstate', '23456', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Alice', 'Johnson', '1990-03-03', 'alice.johnson@example.com', '3456789012', '789 Oak St', 'Sometown', 'Somestate', '34567', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Bob', 'Brown', '1975-04-04', 'bob.brown@example.com', '4567890123', '101 Pine St', 'Yourtown', 'Yourstate', '45678', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Charlie', 'Davis', '1988-05-05', 'charlie.davis@example.com', '5678901234', '202 Maple St', 'Mytown', 'Mystate', '56789', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Diana', 'Miller', '1992-06-06', 'diana.miller@example.com', '6789012345', '303 Birch St', 'Hertown', 'Herstate', '67890', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Eve', 'Wilson', '1983-07-07', 'eve.wilson@example.com', '7890123456', '404 Cedar St', 'Histown', 'Hisstate', '78901', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Frank', 'Moore', '1979-08-08', 'frank.moore@example.com', '8901234567', '505 Spruce St', 'Thistown', 'Thisstate', '89012', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Grace', 'Taylor', '1995-09-09', 'grace.taylor@example.com', '9012345678', '606 Fir St', 'Thattown', 'Thatstate', '90123', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('Hank', 'Anderson', '1987-10-10', 'hank.anderson@example.com', '0123456789', '707 Redwood St', 'Thosetown', 'Thosestate', '01234', DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);

INSERT INTO Account (AccountNumber, AccountType, Balance, CustomerId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('ACC123456', 'Savings', 1000.00, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123457', 'Checking', 2000.00, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123458', 'Savings', 1500.00, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123459', 'Checking', 2500.00, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123460', 'Savings', 3000.00, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123461', 'Checking', 3500.00, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123462', 'Savings', 4000.00, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123463', 'Checking', 4500.00, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123464', 'Savings', 5000.00, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('ACC123465', 'Checking', 5500.00, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);

INSERT INTO CreditCard (CardNumber, CardType, ExpiryDate, CreditLimit, CustomerId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('CC1234567890', 'Visa', '2026-12-31', 5000.00, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567891', 'MasterCard', '2027-01-31', 6000.00, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567892', 'Amex', '2026-11-30', 7000.00, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567893', 'Discover', '2027-02-28', 8000.00, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567894', 'Visa', '2026-10-31', 9000.00, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567895', 'MasterCard', '2027-03-31', 10000.00, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567896', 'Amex', '2026-09-30', 11000.00, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567897', 'Discover', '2027-04-30', 12000.00, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567898', 'Visa', '2026-08-31', 13000.00, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('CC1234567899', 'MasterCard', '2027-05-31', 14000.00, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);


INSERT INTO CreditChecking (CheckDate, CreditScore, CustomerId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('2025-01-01', 700, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-02', 710, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-03', 720, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-04', 730, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-05', 740, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-06', 750, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-07', 760, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-08', 770, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-09', 780, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-10', 790, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);


INSERT INTO Policy (PolicyNumber, PolicyType, StartDate, EndDate, PremiumAmount, CustomerId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('POL123456', 'Health', '2025-01-01', '2026-01-01', 1000.00, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123457', 'Life', '2025-02-01', '2026-02-01', 2000.00, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123458', 'Auto', '2025-03-01', '2026-03-01', 1500.00, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123459', 'Home', '2025-04-01', '2026-04-01', 2500.00, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123460', 'Health', '2025-05-01', '2026-05-01', 3000.00, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123461', 'Life', '2025-06-01', '2026-06-01', 3500.00, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123462', 'Auto', '2025-07-01', '2026-07-01', 4000.00, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123463', 'Home', '2025-08-01', '2026-08-01', 4500.00, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123464', 'Health', '2025-09-01', '2026-09-01', 5000.00, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('POL123465', 'Life', '2025-10-01', '2026-10-01', 5500.00, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);


INSERT INTO LoanApplication (ApplicationNumber, LoanAmount, LoanType, ApplicationDate, CustomerId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('APP123456', 10000.00, 'Personal', '2025-01-01', 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123457', 20000.00, 'Home', '2025-01-02', 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123458', 15000.00, 'Auto', '2025-01-03', 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123459', 25000.00, 'Business', '2025-01-04', 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123460', 30000.00, 'Personal', '2025-01-05', 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123461', 35000.00, 'Home', '2025-01-06', 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123462', 40000.00, 'Auto', '2025-01-07', 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123463', 45000.00, 'Business', '2025-01-08', 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123464', 50000.00, 'Personal', '2025-01-09', 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('APP123465', 55000.00, 'Home', '2025-01-10', 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);


INSERT INTO Payment (PaymentDate, Amount, PaymentMethod, AccountId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('2025-01-01', 100.00, 'Credit Card', 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-02', 200.00, 'Debit Card', 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-03', 150.00, 'Bank Transfer', 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-04', 250.00, 'Cash', 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-05', 300.00, 'Credit Card', 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-06', 350.00, 'Debit Card', 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-07', 400.00, 'Bank Transfer', 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-08', 450.00, 'Cash', 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-09', 500.00, 'Credit Card', 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-10', 550.00, 'Debit Card', 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);


INSERT INTO [Transaction] (TransactionDate, Amount, TransactionType, AccountId, CustomerId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES
('2025-01-01', 100.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-02', 200.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-03', 150.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-04', 250.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-05', 300.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-06', 350.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-07', 400.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-08', 450.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-09', 500.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-10', 550.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-11', 600.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-12', 650.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-13', 700.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-14', 750.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-15', 800.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-16', 850.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-17', 900.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-18', 950.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-19', 1000.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-20', 1050.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-21', 1100.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-22', 1150.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-23', 1200.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-24', 1250.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-25', 1300.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-26', 1350.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-27', 1400.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-28', 1450.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-29', 1500.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-30', 1550.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-01-31', 1600.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-01', 1650.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-02', 1700.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-03', 1750.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-04', 1800.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-05', 1850.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-06', 1900.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-07', 1950.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-08', 2000.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-09', 2050.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-10', 2100.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-11', 2150.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-12', 2200.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-13', 2250.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-14', 2300.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-15', 2350.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-16', 2400.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-17', 2450.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-18', 2500.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-19', 2550.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-20', 2600.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-21', 2650.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-22', 2700.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-23', 2750.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-24', 2800.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-25', 2850.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-26', 2900.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-27', 2950.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-02-28', 3000.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-01', 3050.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-02', 3100.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-03', 3150.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-04', 3200.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-05', 3250.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-06', 3300.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-07', 3350.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-08', 3400.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-09', 3450.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-10', 3500.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-11', 3550.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-12', 3600.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-13', 3650.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-14', 3700.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-15', 3750.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-16', 3800.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-17', 3850.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-18', 3900.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-19', 3950.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-20', 4000.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-21', 4050.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-22', 4100.00, 'Deposit', 1, 1, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-23', 4150.00, 'Withdrawal', 2, 2, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-24', 4200.00, 'Deposit', 3, 3, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-25', 4250.00, 'Withdrawal', 4, 4, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-26', 4300.00, 'Deposit', 5, 5, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-27', 4350.00, 'Withdrawal', 6, 6, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-28', 4400.00, 'Deposit', 7, 7, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-29', 4450.00, 'Withdrawal', 8, 8, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-30', 4500.00, 'Deposit', 9, 9, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1),
('2025-03-31', 4550.00, 'Withdrawal', 10, 10, DEFAULT, 'admin', GETDATE(), 'admin', GETDATE(), 1);


INSERT INTO Invoice (InvoiceNumber, InvoiceType, Amount, CustomerId, RowVersion, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
VALUES 
('INV-1001', 'Service', 150.00, 1, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1002', 'Product', 200.00, 2, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1003', 'Service', 300.00, 3, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1004', 'Product', 450.00, 4, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1005', 'Service', 500.00, 5, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1006', 'Product', 600.00, 6, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1007', 'Service', 700.00, 7, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1008', 'Product', 800.00, 8, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1009', 'Service', 900.00, 9, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1),
('INV-1010', 'Product', 1000.00, 10, DEFAULT, 'System', GETDATE(), 'System', GETDATE(), 1);

