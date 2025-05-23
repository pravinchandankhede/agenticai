﻿namespace BankServices.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Account")]
public class Account
{
    [Key]
    public Int32 AccountId { get; set; }

    [Required]
    [MaxLength(50)]
    public required String AccountNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public required String AccountType { get; set; }

    [Required]
    public Decimal Balance { get; set; }

    [Required]
    public Int32 CustomerId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("CustomerId")]
    public required Customer Customer { get; set; }
}

[Table("CreditCard")]
public class CreditCard
{
    [Key]
    public Int32 CreditCardId { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CardNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CardType { get; set; }

    [Required]
    public DateTime ExpiryDate { get; set; }

    [Required]
    public Decimal CreditLimit { get; set; }

    [Required]
    public Int32 CustomerId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("CustomerId")]
    public required Customer Customer { get; set; }
}

[Table("Policy")]
public class Policy
{
    [Key]
    public Int32 PolicyId { get; set; }

    [Required]
    [MaxLength(50)]
    public required String PolicyNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public required String PolicyType { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public Decimal PremiumAmount { get; set; }

    [Required]
    public Int32 CustomerId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("CustomerId")]
    public required Customer Customer { get; set; }
}

[Table("LoanApplication")]
public class LoanApplication
{
    [Key]
    public Int32 LoanApplicationId { get; set; }

    [Required]
    [MaxLength(50)]
    public required String ApplicationNumber { get; set; }

    [Required]
    public Decimal LoanAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public required String LoanType { get; set; }

    [Required]
    public DateTime ApplicationDate { get; set; }

    [Required]
    public Int32 CustomerId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("CustomerId")]
    public required Customer Customer { get; set; }
}

[Table("CreditChecking")]
public class CreditChecking
{
    [Key]
    public Int32 CreditCheckingId { get; set; }

    [Required]
    public DateTime CheckDate { get; set; }

    [Required]
    public Int32 CreditScore { get; set; }

    [Required]
    public Int32 CustomerId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("CustomerId")]
    public required Customer Customer { get; set; }
}

[Table("Payment")]
public class Payment
{
    [Key]
    public Int32 PaymentId { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    [Required]
    public Decimal Amount { get; set; }

    [Required]
    [MaxLength(50)]
    public required String PaymentMethod { get; set; }

    [Required]
    public Int32 AccountId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("AccountId")]
    public required Account Account { get; set; }
}

[Table("Customer")]
public class Customer
{
    [Key]
    public Int32 CustomerId { get; set; }

    [Required]
    [MaxLength(50)]
    public required String FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public required String LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(100)]
    public required String Email { get; set; }

    [MaxLength(15)]
    public required String PhoneNumber { get; set; }

    [MaxLength(255)]
    public required String Address { get; set; }

    [MaxLength(50)]
    public required String City { get; set; }

    [MaxLength(50)]
    public required String State { get; set; }

    [MaxLength(10)]
    public required String ZipCode { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }
}

[Table("Transaction")]
public class Transaction
{
    [Key]
    public Int32 TransactionId { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [Required]
    public Decimal Amount { get; set; }

    [Required]
    [MaxLength(50)]
    public required String TransactionType { get; set; }

    [Required]
    public Int32 AccountId { get; set; }

    [Required]
    public Int32 CustomerId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("AccountId")]
    public required Account Account { get; set; }

    [ForeignKey("CustomerId")]
    public required Customer Customer { get; set; }
}

[Table("Invoice")]
public class Invoice
{
    [Key]
    public Int32 InvoiceId { get; set; }

    [Required]
    [MaxLength(50)]
    public required String InvoiceNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public required String InvoiceType { get; set; }

    [Required]
    public Decimal Amount { get; set; }

    [Required]
    public Int32 CustomerId { get; set; }

    [Timestamp]
    public required Byte[] RowVersion { get; set; }

    [Required]
    [MaxLength(50)]
    public required String CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength(50)]
    public required String ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    [Required]
    public Boolean IsActive { get; set; }

    [ForeignKey("CustomerId")]
    public required Customer Customer { get; set; }
}