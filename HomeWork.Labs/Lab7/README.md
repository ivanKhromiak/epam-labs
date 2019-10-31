# Epam .NET Lab 10

This project is a part of homework for Epam .NET Lab #10

## Description

- Project type: Class Library (.NET Core)

- Contains classes which helps to solves task
  1. [DirectoryComparer](Lab7/DirectoryComparer.cs) - to compare directories (uses [EnumerableComparer](Lab7/EnumerableComparer.cs))
  2. [EnumerableComparer](Lab7/EnumerableComparer.cs) - to compare collections

Excel Input/Ouptut implemented in the corresponding Lab Runner

## Tasks
Варіант 1.

Задача. Є два списки в Excel файлі, треба знайти всі комірки з унікальними значеннями (які не повторюються) та вивести іх на екран або в файл 
в залежності від налаштувань в AppConfig (номера колонок, які порівнювати теж задати в апп конфізі)

Варіант 2.
Задача. Є дві папки з файлами (підпапки фраховуємо),  за командою:
1. вивести список всіх файлів, які дублюються, та іх загальну кількість.
2. вивести список унікальних файлів по директоріі
3. вивести результати в файл або на консоль в залежності від налаштувань в AppConfig

Адреси папок задаються в AppConfig.

Загальна умова. try catch там де потрібно, SOLID, незалежність від інтерфейса (на Dll), Сode Style, файли в форматі excel (як для ввода так і виводу) 

### Roman Moravskyi, 2019
