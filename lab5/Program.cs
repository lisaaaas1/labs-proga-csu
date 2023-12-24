using static lab5.Lab5;

Console.WriteLine("Введите ваше выражение для ОПЗ: ");

string expression1 = Console.ReadLine();

Console.WriteLine($"Вы ввели: {expression1}");
Console.WriteLine($"Ваш результат: {RPN.ResultRPN(expression1)}");

