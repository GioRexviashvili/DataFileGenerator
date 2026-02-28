DataFileGenerator

Overview

DataFileGenerator is a .NET console application designed to extract structured data from a relational database and generate a formatted data file output.
The application reads data from database tables (Products and Categories) and transforms the selected fields into a tab-separated values (TSV) file.
---

Database Structure

The application works with the following tables:

Categories
	•	CategoryId
	•	CategoryName

Products
	•	ProductId
	•	ProductName
	•	UnitPrice
	•	UnitsInStock
	•	Discontinued
	•	CategoryId (Foreign Key)

The data is joined using the foreign key relationship between Products and Categories.
---

Output Format

The exported TSV file contains structured columns in the following order:
	1.	CategoryName
	2.	CategoryIsActive
	3.	ProductCode
	4.	ProductName
	5.	Price
	6.	Quantity
	7.	ProductIsActive

Each column is separated by a tab character (\t).

example:
Beverages  1  1  Chai  18.00  39  1

Seafood  1  12  Ikura  31.00  20  1

---

Technologies Used
	•	.NET
	•	C#
	•	SQL Server
	•	ADO.NET
	•	Git
---
