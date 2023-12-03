// See https://aka.ms/new-console-template for more information
using System.Text;
using withRPN;

RPN rpn = new RPN();
Console.WriteLine("Обратная польская запись");

// получаем выражение
Console.WriteLine("Введите выражение: ");
string expression = Console.ReadLine();
Console.WriteLine("Ваш результат: "+RPN.ResultRPN(expression)); 

// преобразование входной строки в строку опз
string RPNexpression = RPN.DoExpressionToRPN(expression);
Console.WriteLine("Ваше выражение в ОПЗ: " + RPNexpression);




