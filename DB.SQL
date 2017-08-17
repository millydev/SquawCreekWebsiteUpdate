-- ----------------------------------- 
-- Schema squawCreek
--------------------------------------
DROP SCHEMA IF EXISTS squawcreek;
---------------------------------------
-- Schema squawCreek
----------------------------------------
CREATE DATABASE IF NOT EXISTS squawcreek;
--------------------------------------------------------
-- DROP TABLES
--------------------------------------------------------
DROP TABLE IF EXISTS squawcreek.customers;
DROP TABLE IF EXISTS squawcreek.mailingAddress;
DROP TABLE IF EXISTS squawcreek.customerPaymentMethods;
DROP TABLE IF EXISTS squawcreek.billingAddress;
DROP TABLE IF EXISTS squawcreek.transactionType;
DROP TABLE IF EXISTS squawcreek.shippers;
DROP TABLE IF EXISTS squawcreek.orders;
DROP TABLE IF EXISTS squawcreek.financialTransactions;
DROP TABLE IF EXISTS squawcreek.vendors;
DROP TABLE IF EXISTS squawcreek.products;
DROP TABLE IF EXISTS squawcreek.orderDetails;
DROP TABLE IF EXISTS squawcreek.calendarEvents;

--------------------------------------------------------
-- Create Tables
--------------------------------------------------------
-- Create Customers

CREATE TABLE IF NOT EXISTS squawcreek.customers (
 	customerID INT NOT NULL AUTO_INCREMENT,
  lastName VARCHAR(30) NULL,
  firstName VARCHAR(30) NULL,
  phone VARCHAR(15) NULL,
  email VARCHAR(50) NULL,
  PRIMARY KEY (customerID));

-- Create mailing address
CREATE TABLE IF NOT EXISTS squawcreek.mailingAddress (
  mailingAddressID INT NOT NULL AUTO_INCREMENT,
  customerID INT NULL,
  addressLine1 VARCHAR(100) NULL,
  addressLine2 VARCHAR(100) NULL,
 	city VARCHAR(30) NULL,
  state VARCHAR(20) NULL,
  postalCode VARCHAR(15) NULL,
  country VARCHAR(30) NULL,
  PRIMARY KEY (mailingAddressID),
  INDEX customerID_IDX (customerID ASC),
  CONSTRAINT customerID
  	FOREIGN KEY(customerID)
  	REFERENCES squawcreek.customers (customerID));        

-- Create Payment Methods
CREATE TABLE IF NOT EXISTS squawcreek.paymentMethods (
	paymentMethodID INT NOT NULL AUTO_INCREMENT,
	paymentMethodDescription VARCHAR(20) NOT NULL,
	PRIMARY KEY (paymentMethodID));

-- Create Customer Payment Methods
CREATE TABLE IF NOT EXISTS squawcreek.customerPaymentMethods (
	customerID INT NOT NULL,
	paymentMethodID INT NOT NULL,
	cardNumber VARCHAR(16) NULL,
	cardType VARCHAR(15) NULL,
	securityCode INT NULL,
  expirationDate DATE NULL,
	PRIMARY KEY (customerID, paymentMethodID));

 -- Create Table Billing Addres
CREATE TABLE IF NOT EXISTS squawcreek.billingAddress (
 	billingAddressID INT NOT NULL AUTO_INCREMENT,
	paymentMethodID INT,
  addressLine1 VARCHAR(100) NULL,
  addressLine2 VARCHAR(100) NULL,
  city VARCHAR(30) NULL,
  state VARCHAR(20) NULL,
  postalCode VARCHAR(15) NULL,
  country VARCHAR(30) NULL,
  PRIMARY KEY (billingAddressID),
 	INDEX paymentMethodID_IDX (paymentMethodID ASC),
  CONSTRAINT paymentMethodID
    FOREIGN KEY (paymentMethodID)
    REFERENCES squawCreek.paymentMethods (paymentMethodID));

-- Create Table Transaction Type
CREATE TABLE IF NOT EXISTS squawcreek.transactionType (
  transactionTypeID INT NOT NULL AUTO_INCREMENT,
  transactionTypeDescription VARCHAR(20) NULL,
  PRIMARY KEY (transactionTypeID));

-- Create Table Shippers
CREATE TABLE IF NOT EXISTS squawcreek.shippers (
  shipperID INT NOT NULL AUTO_INCREMENT,
  companyName VARCHAR(30) NULL,
  phone VARCHAR(15) NULL,
  website VARCHAR(255) NULL,
  PRIMARY KEY (shipperID));

-- Create Table Vendors
CREATE TABLE IF NOT EXISTS squawcreek.vendors (
	vendorID INT(15) NOT NULL AUTO_INCREMENT,
	vendorName VARCHAR(50) NULL,
  contactName VARCHAR(50) NULL,
  contactTitle VARCHAR(30) NULL,
  addressLine1 VARCHAR(100) NULL,
  addressLine2 VARCHAR(100) NULL,
  city VARCHAR(30) NULL,
  postalCode VARCHAR(15) NULL,
  country VARCHAR(30) NULL,
  phone VARCHAR(15) NULL,
  email VARCHAR(50) NULL,
  website VARCHAR(255) NULL,
  PRIMARY KEY (vendorID));
  
 -- Create Table Orders
CREATE TABLE IF NOT EXISTS squawcreek.orders(
orderID INT(15) NOT NULL AUTO_INCREMENT,
ocustomerID INT(15) NULL,
shipperID INT(15) NULL,
orderDate DATE NULL,	
requiredDate DATE NULL,	
shippedDate DATE NULL,	
orderTotal DECIMAL(8,2) NULL,
orderComment VARCHAR(255) NULL,
PRIMARY KEY (orderID),
INDEX ocustomerID_IDX (ocustomerID ASC),
INDEX shipperID_IDX (shipperID ASC),
CONSTRAINT ocustomerID
	FOREIGN KEY (ocustomerID)
	REFERENCES squawcreek.customers (customerID),
CONSTRAINT shipperID
	FOREIGN KEY (shipperID)
	REFERENCES squawcreek.shippers (shipperID));

-- Create Table Financial Transactions
CREATE TABLE IF NOT EXISTS squawcreek.financialTransactions (
  financialTransactionsID INT(15) NOT NULL AUTO_INCREMENT,
  forderID INT,
  fcustomerID INT,
  transactionTypeID INT,
  authorizationNumber VARCHAR(15) NULL,
  transactionDate DATETIME NULL,
  transactionAmount INT NULL,
  financialTransactionscol VARCHAR(45) NULL,
  PRIMARY KEY (financialTransactionsID),
  INDEX forderID_IDX (forderID ASC),
  INDEX fcustomerID_IDX (fcustomerID ASC),
  INDEX transactionTypeID_IDX (transactionTypeID ASC),
  CONSTRAINT forderID
    FOREIGN KEY (forderID)
    REFERENCES squawcreek.orders (orderID),
  CONSTRAINT fcustomerID
    FOREIGN KEY (fcustomerID)
    REFERENCES squawcreek.customers (customerID),
  CONSTRAINT transactionTypeID
    FOREIGN KEY (transactionTypeID)
    REFERENCES squawcreek.transactionType (transactionTypeID));

-- Create Table Products
CREATE TABLE IF NOT EXISTS squawcreek.products (
  productID INT(15) NOT NULL AUTO_INCREMENT,
  vendorID INT,
  productName VARCHAR(45) NULL,
  buyPrice VARCHAR(45) NULL,
  MSRP VARCHAR(45) NULL,
  salesPrice VARCHAR(45) NULL,
  quanityInStock VARCHAR(45) NULL,
  reorderLevel VARCHAR(45) NULL,
  productDescription VARCHAR(45) NULL,
  htmlDescription VARCHAR(45) NULL,
  image VARCHAR(45) NULL,
  PRIMARY KEY (productID),
  CONSTRAINT vendorID
    FOREIGN KEY (vendorID)
    REFERENCES squawcreek.vendors(vendorID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- Create Table Order Details
CREATE TABLE IF NOT EXISTS squawcreek.orderDetails(
  orderLineID INT(15) NOT NULL AUTO_INCREMENT,
  productID INT,
  orderId INT,
  quanityOrdered INT NULL,
  priceEach DECIMAL(8,2) NULL,
  orderDetailscol VARCHAR(45) NULL,
  PRIMARY KEY (orderLineID),
  CONSTRAINT productID
    FOREIGN KEY (productID)
    REFERENCES squawcreek.products (productID),
  CONSTRAINT orderID
    FOREIGN KEY (orderID)
    REFERENCES squawcreek.orders (orderID));
    
-- Create Table Calendar Events
CREATE TABLE IF NOT EXISTS squawcreek.calendarEvents(
  calendarEventsId INT(15) NOT NULL AUTO_INCREMENT,
  eventName VARCHAR(200) NULL,
  startDate DATETIME NULL,
  endDate DATETIME NULL,
  PRIMARY KEY (calendarEventsId));
