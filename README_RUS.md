# Simulator Math Modeling  
Программа-тренажер для студентов по задачам математического моделирования.  
Макет: https://www.figma.com/design/c4Qv9J8hsPYBfKaHDxltLG/MM-Simulator?node-id=0-1  
  
<img src="http://gogs.ngknn.ru:3000/TrioEducationalPractice/MathModelingSimulator/raw/35b563e213f543ec9638d7d30edaaff81a6f371b/logo.png" width="200" alt="Simulator-MM"/>
  
## Установка программы средствами Visual Studio  
1. Откройте Visual Studio  
2. Нажмите "Клонировать репозиторий"  
3. Вставьте ссылку на репозиторий  
4. Запустите проект  
  
## Инструкция по работе с системой  
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
  
### NuGet пакеты и зависимости   
* CommunityToolkit.Mvvm  
* LiveChartsCore  
* LiveChartsCore.SkiaSharpView.Avalonia  
* Microsoft.EntityFrameworkCore  
* Microsoft.EntityFrameworkCore.Design  
* Microsoft.EntityFrameworkCore.Tools  
* Npgsql.EntityFrameworkCore.PostgreSQL  
    
## Поддержка  
Если у вас возникнут какие-либо трудности или вопросы, создайте обсуждение в этом репозитории.

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

## Авторы
[ArrayKat2](http://gogs.ngknn.ru:3000/ArrayKat2)  
[klmnvan](http://gogs.ngknn.ru:3000/klmnvan)  
[Otter](http://gogs.ngknn.ru:3000/Otter)  

