using System;
using System.Collections.Generic;
using ConsoleApp3;

// инициализация списков символов и чисел выражения 
List<char> operations = new List<char>();
List<double> numbers = new List<double>();
// список возможных вводимых символов
List<char> operationsList = new List<char>{'-', '+', '/', '*'};

Console.WriteLine("Введите математическое выражение");
string mathematicalExpression = Console.ReadLine();

// разделяем введенную строку на части, используя операторы
string[] parts = mathematicalExpression.Split(new char[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);

foreach (var element in parts)
{
    // проверяем может ли элемент быть числом
    if (double.TryParse(element, out double number))
    {
        // добавляем число в массив чисел
        numbers.Add(number);
    }
} 

foreach (char element in mathematicalExpression)
{
    // цикл для сравнения значений строки с массивом операций
    foreach (var symbol in operationsList)
    {
        // проверяем может ли элемент быть операцией
        if (element == symbol)
        {
            operations.Add(symbol);
        }
    }
}

// лабораторная работа 2

DoOperations doOperations = new DoOperations();

// сначала выполняются операции выше по приоритету
for (int i = 0; i < operations.Count; i++)
{
    if (operations[i] == '^')
    {
        // передаем в метод Calculate две переменные и операцию
        // записываем результат в том же массиве чисел
        numbers[i] = doOperations.Calculate(numbers[i], numbers[i + 1], operations[i]);
        // удаляем использованную для подсчета переменную
        numbers.RemoveAt(i + 1);
        // удаляем использованный для подсчета оператор
        operations.RemoveAt(i);
        i--;
    }
}

// сначала выполняются операции выше по приоритету
for (int i = 0; i < operations.Count; i++)
{
    if (operations[i] == '*' || operations[i] == '/')
    {
        // передаем в метод Calculate две переменные и операцию
        // записываем результат в том же массиве чисел
        numbers[i] = doOperations.Calculate(numbers[i], numbers[i + 1], operations[i]);
        // удаляем использованную для подсчета переменную
        numbers.RemoveAt(i + 1);
        // удаляем использованный для подсчета оператор
        operations.RemoveAt(i);
        i--;
    }
}

// выполнение остальных операций
for (int i = 0; i < operations.Count; i++)
{
    if (operations.Contains(operations[i]))
    {
        // передаем в метод Calculate две переменные и операцию
        // записываем результат в том же массиве чисел
        numbers[i] = doOperations.Calculate(numbers[i], numbers[i + 1], operations[i]);
        // удаляем использованную для подсчета переменную
        numbers.RemoveAt(i + 1);
        // удаляем использованный для подсчета оператор
        operations.RemoveAt(i);
        i--;
    }
}
Console.WriteLine(numbers[0]);