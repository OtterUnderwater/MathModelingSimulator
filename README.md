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
Log in or register.  
![image](https://github.com/user-attachments/assets/36e02a81-b699-4850-afbe-6fc94ad6831f)  
Depending on your role, you can perform several actions after authorization.  
Any user can change the account information in the profile. The "Log out" button allows you to return to the authorization page.  
![image](https://github.com/user-attachments/assets/139be28f-c554-4cfe-86e5-b4c5ba61eecf)   
  
### The teacher can:  
* edit an existing task: to do this, select the task number in the list and click the "Edit" button.  
* delete an existing task: to do this, select the task number in the list and click the "Delete" button.  
  ![image](https://github.com/user-attachments/assets/53642fc4-b4f0-4620-a674-b6e865e15fd7)  
* to add a task: to do this, on the "create a task for the simulator" tab, select the creation method: attach a file – this method suggests selecting a file, the file must be in the format.txt, .csv, or other text separated by a space, comma, or semicolon. Or enter a matrix – generates fields for the n*m matrix, where n and m are the values entered by the user.  
  ![image](https://github.com/user-attachments/assets/3c608edd-67fb-475c-8667-977eddcb7798)  
* view statistics with all students on their activity in simulators. The teacher's account has a menu item called "Student Statistics", which contains a chart on the number of simulators completed by students for each day. Below the diagram is a table containing all the data on the passage of simulators.    
  ![image](https://github.com/user-attachments/assets/ea334396-251f-4562-be7a-78d0be728ef0)     
  When you hover over a point, the date and number of completed simulators are displayed on that day by the student.   
  ![image](https://github.com/user-attachments/assets/713f59d8-00c3-422c-85ec-fd73a8f0ebf9)   
  The test results can be sorted. To do this, just click on the column you want to sort and the results will be sorted by or against the alphabet.   
  ![image](https://github.com/user-attachments/assets/2cae7394-68ba-455b-9224-b2d0ba043fd2)    
  
### The student can:   
* solve the problem – it is enough to select the simulator in the menu item, when selected, the task page opens. Just enter the answer and click reply to find out the results of the task.  
  ![image](https://github.com/user-attachments/assets/b5de5d87-1639-4344-9455-66e4f3c35837)  
  ![image](https://github.com/user-attachments/assets/f31f7819-7445-46d0-993b-ecf13d0ae936)  
* view your statistics on the number of correct and incorrect answers in the "Statistics" menu item, which contains a pie chart with the results of passing all simulators.  
  ![image](https://github.com/user-attachments/assets/3c9ff379-2561-47b6-b938-499914130b6a)  
  
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
