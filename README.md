# Simulator Math Modeling  
A simulator program for students on mathematical modeling problems.  
There is also a [manual in Russian](https://github.com/OtterUnderwater/MathModelingSimulator/blob/master/README_RUS.md).  
Layout: https://www.figma.com/design/c4Qv9J8hsPYBfKaHDxltLG/MM-Simulator?node-id=0-1   
  
<img src="https://github.com/OtterUnderwater/MathModelingSimulator/blob/master/logo.png" width="200" alt="Simulator-MM"/>
  
# Getting started  
## Technical requirements  
Supported operating systems:
* Windows 7 and above
* mac OS 10.13 
* Linux
Processor:
* Minimum: 2 physical, 4 virtual cores and 2 GHz
* Recommended: 4 physical cores, 6 virtual cores and 2.6GHz or higher
Random access memory:
* Minimum: 8 GB
* Recommended: 16 GB or more
Disk space:
* Minimum: 4 gigabytes of free space
* Recommended: 8 GB of free space or more
  
## Software Requirements  
* IDE Visual Studio / Visual Studio Code / Rider  
* [.Net 8.0 and higher](https://dotnet.microsoft.com/en-us/)  
* [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)  
* [Avalonia templates for building a project](https://avaloniachina.github.io/avalonia-docs/ru/docs/get-started/install/)

## Installation using Visual Studio tools  
1. Open Visual Studio
2. Click "Clone Repository"
3. Paste the link to the repository
4. Start the project

## Instructions for working with the system  
Before working with the system, we recommend reading the [user's Guide](http://gogs.ngknn.ru:3000/TrioEducationalPractice/documentation/src/master/%d0%a0%d1%83%d0%ba%d0%be%d0%b2%d0%be%d0%b4%d1%81%d1%82%d0%b2%d0%be%d0%9f%d0%be%d0%bb%d1%8c%d0%b7%d0%be%d0%b2%d0%b0%d1%82%d0%b5%d0%bb%d1%8f.docx) 

A set of documentation:   
[Documentation](http://gogs.ngknn.ru:3000/TrioEducationalPractice/documentation)  
[UML](http://gogs.ngknn.ru:3000/TrioEducationalPractice/UML) 

### Notes  
At the moment, the program will not work in the Release version due to dependence on the LiveCharts 2 library.  
The program reads theoretical material from files located in the Assets folder.  
Program exceptions are recorded in the log file.  
If the application is launched from the local NGC computer, then in the Programm file you need to replace the line:  
```
public static string HostNpgsql = "Host=ngknn.ru;Port=5442;Database=trio_33p;Username=33P;Password=12345";
```
For the following:  
```
public static string HostNpgsql = "Host=edu.pg.ngknn.local;Port=5432;Database=trio_33p;Username=33P;Password=12345";
```  
### Introduction  
To test the program without having to register, you can use the following username and password:  
* 1 1 - Teacher  
* 2 2 - Student  

# Technologies used in the project  
The application is implemented in accordance with the MVVM and ReactiveUI patterns.  
* C# DotNETCore 8.0;   
* AvaloniaUI Framework;   
* EntityFrameworkCore;   
* PostgreSQL.  
### NuGet packages and dependencies  
* CommunityToolkit.Mvvm  
* LiveChartsCore  
* LiveChartsCore.SkiaSharpView.Avalonia  
* Microsoft.EntityFrameworkCore  
* Microsoft.EntityFrameworkCore.Design  
* Microsoft.EntityFrameworkCore.Tools  
* Npgsql.EntityFrameworkCore.PostgreSQL  

## Description of commits
| Name     | Description                                          |
| -------- | ---------------------------------------------------- |
| docs     | update documentation                                 |
| feat     | adding new functionality                             |
| layout   | adding a new layout without functionality            |
| style    | working with styles and design                       |
| fix      | error correction                                     |
| perf     | changes to improve the program                       |
| refactor | code edits without changing errors or functionality  |
| revert   | rollback to previous versions                        |
| merge    | merging branches                                     |
| test     | adding Tests                                         |
  
## Support  
If you have any difficulties or questions, create discussion in this repository.   
  
## How to help the project  
To improve the quality of the product, leave feedback and ratings. You can create a discussion in this repository for suggestions.  
  
## Authors
[ArrayKat](https://github.com/ArrayKat)  
[klmnvan](https://github.com/klmnvan)  
[OtterUnderwater](https://github.com/OtterUnderwater)  
