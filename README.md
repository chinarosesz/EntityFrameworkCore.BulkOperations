# Introduction
A simple Entity Framework extension to perform fast database insertion using SqlBulkCopy API and SQL Merge statement. When referencing this 
library, ensure your project is a .NET Core project, at least version of 3.1

# Downloads
Reference this nuget package in your project file https://www.nuget.org/packages/EntityFrameworkCore.BulkOperations/

# Requirements
In order to use this library, your codebase should have some usage of Entity Framework for the data layer manipulation. 
Your project should be using at least .NET Core 3.1 since this library was developed under this version. I intend to test 
this library to be compatible with .NET 5.0 in the near future. 

# Usage

List of available bulk operations against your database context.
<pre>
context.BulkInsert(employees)
context.BulkInsertOrUpdate(employees)
context.BulkUpdate(employees)
context.BulkDelete(employees)
</pre>

For more references of how to use this library, refer to <code>EntityFrameworkCore.BulkOperations.Tests</code> project.

# Performance
Bulk operations were run against a local machine of 16 GB of memory with an i7 CPU.

| Operation  | Records | Columns | Time (seconds) |
| ---------- | ------- | ------- | -------------- |
| BulkInsert | 50,000  | 10      | 12             |
| BulkUpdate | 50,000  | 10      | 3              |
| BulkDelete | 50,000  | 10      | 1              |

# Contribution
This is an open source library, feel free to create a PR to address new features or improvements. 


