# Simulator Math Modeling  
Программа-тренажер для студентов по задачам математического моделирования.  
Макет: https://www.figma.com/design/c4Qv9J8hsPYBfKaHDxltLG/MM-Simulator?node-id=0-1  
  
<img src="https://github.com/OtterUnderwater/MathModelingSimulator/blob/master/logo.png" width="200" alt="Simulator-MM"/>

# Начало работы
## Технические требования
Необходимая операционная система:
* Windows 7 и выше
* macOS 10.13 
* Linux
Процессор:
* Минимально: 2 физических, 4 виртуальных ядра и 2 ГГц
* Рекомендуется: 4 физических ядра, 6 виртуальных ядер и 2.6 ГГц и выше
Оперативная память:
* Минимально: 8 гигабайта
* Рекомендуется: 16 гигабайт и больше
Место на диске:
* Минимально: 4 гигабайта свободного пространства
* Рекомендуется: 8 гигабайт свободного пространства и больше  
  
## Требования к ПО  
* IDE Visual Studio / Visual Studio Code / Rider  
* [.Net 8.0 и выше](https://dotnet.microsoft.com/en-us/)  
* [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)  
* [шаблоны Avalonia для сборки проекта](https://avaloniachina.github.io/avalonia-docs/ru/docs/get-started/install/)  
  
## Установка программы средствами Visual Studio  
1. Откройте Visual Studio  
2. Нажмите "Клонировать репозиторий"  
3. Вставьте ссылку на репозиторий  
4. Запустите проект  

# Инструкция по работе с системой   
Перед работой с системой советуем прочесть [руководство пользователя](http://gogs.ngknn.ru:3000/TrioEducationalPractice/documentation/src/master/%d0%a0%d1%83%d0%ba%d0%be%d0%b2%d0%be%d0%b4%d1%81%d1%82%d0%b2%d0%be%d0%9f%d0%be%d0%bb%d1%8c%d0%b7%d0%be%d0%b2%d0%b0%d1%82%d0%b5%d0%bb%d1%8f.docx)    
Набор документации:    
[Документация](http://gogs.ngknn.ru:3000/TrioEducationalPractice/documentation)    
[UML](http://gogs.ngknn.ru:3000/TrioEducationalPractice/UML)   
  
### Примечания  
На данный момент программа не будет работать в версии Релиз из-за зависимости от библиотеки LiveCharts2.    
Программа считывает теоретический материал из файлов, которые находятся в папке Assets.    
Исключения программы записываются в журнал логов.   
Если приложение запускается с локального компьютера НГК, то в файле Programm нужно заменить строку:   
```
public static string HostNpgsql = "Host=ngknn.ru;Port=5442;Database=trio_33p;Username=33P;Password=12345";  
```
На следующее:  
```
public static string HostNpgsql = "Host=edu.pg.ngknn.local;Port=5432;Database=trio_33p;Username=33P;Password=12345";  
```
### Ознакомление  
Для тестирования программы без необходимости регистрироваься можно использовать следующие логин и пароль:  
* 1 1 - Преподаватель  
* 2 2 - Студент  
  
# Используемые технологии в проекте  
Приложении реализовано в соответсвии с паттерном MVVM и ReactiveUI.  
* C# DotNETCore 8.0;   
* AvaloniaUI Framework;   
* EntityFrameworkCore;   
* PostgreSQL.  
### NuGet пакеты и зависимости   
* CommunityToolkit.Mvvm  
* [LiveChartsCore](https://github.com/beto-rodriguez/LiveCharts2/pkgs/nuget/LiveChartsCore?ysclid=m75t1zmsam347444569)  
* LiveChartsCore.SkiaSharpView.Avalonia  
* Microsoft.EntityFrameworkCore  
* Microsoft.EntityFrameworkCore.Design  
* Microsoft.EntityFrameworkCore.Tools  
* Npgsql.EntityFrameworkCore.PostgreSQL  
   
## Описание коммитов
| Название | Описание                                                             |
| -------- | -------------------------------------------------------------------- |
| docs     | обновление документации                                              |
| feat     | добавление нового функционала                                        |
| layout   | создание нового шаблона или представления без добавления функционала |
| style    | работа со стилем или дизайном                                        |
| fix      | исправление ошибок                                                   |
| perf     | изменения, направленные на улучшение программы                       |
| refactor | редактирование кода без изменения ошибок или добавления функционала  |
| revert   | откат на предыдущую версию                                           |
| merge    | слияние веток                                                        |
| test     | добавление тестов                                                    |
  
## Поддержка  
Если у вас возникнут какие-либо трудности или вопросы, создайте обсуждение в этом репозитории.  
  
## Как помочь проекту  
Для улучшения качества продукта оставляйте отзывы и оценки. Вы можете создать обсуждение в этом репозитории для предложений.  
  
## Авторы  
[ArrayKat](https://github.com/ArrayKat)  
[klmnvan](https://github.com/klmnvan)  
[OtterUnderwater](https://github.com/OtterUnderwater)  
