// See https://aka.ms/new-console-template for more information

using System.Text;

Console.WriteLine("Обратная польская запись");

// получаем выражение
Console.WriteLine("Введите выражение: ");
string expression = Console.ReadLine();

// преобразование входной строки в строку опз
string RPNexpression = convertToRPN(expression);
Console.WriteLine("Ваше выражение в ОПЗ: " + RPNexpression);
// выполнение вычислений
double result = doRPN(RPNexpression);
Console.WriteLine("Результат операций: " + result);

// приводим строку к опз
static string convertToRPN(string expression)
{
    StringBuilder expressionRPN = new StringBuilder();
    Stack<char> operators = new Stack<char>();

    foreach (char token in expression)
    {
        if (char.IsDigit(token))
        {
            // если токен - число, добавляем его к строке опз
            expressionRPN.Append(token);
        }
        else if (token == '(')
        {
            operators.Push(token);
        }
        else if (token == ')')
        {
            // пока не встретим открывающую скобку, выталкиваем операторы из стека в строку опз
            while (operators.Count > 0 && operators.Peek() != '(')
            {
                expressionRPN.Append(operators.Pop());
            }

            // убираем '(' из стека
            operators.Pop();
        }
        else
        {
            // пока операторы в стеке имеют больший или равный приоритет, выталкиваем их в строку опз
            while (operators.Count > 0 && GetPrecedence(token) <= GetPrecedence(operators.Peek()))
            {
                expressionRPN.Append(operators.Pop());
            }

            operators.Push(token);
        }
    }

    // обработка оставшихся операторов в стеке
    while (operators.Count > 0)
    {
        expressionRPN.Append(operators.Pop());
    }

    return expressionRPN.ToString();
}

// расставляем приоритеоты
static int GetPrecedence(char op)
{
    switch (op)
    {
        case '+':
            return 1;
        case '-':
            return 1;
        case '*':
            return 2;
        case '/':
            return 2;
        default:
            return 0; // если не оператор, возвращаем 0
    }
}


// метод опз
static double doRPN(string expressionRPNString)
{
    Stack<double> stack = new Stack<double>();
    string[] tokens = expressionRPNString.Split(new char[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
    
    foreach (var token in tokens)
    {
        if (double.TryParse(token, out var num))
        {
            stack.Push(num);
        }
        else if (IsOperator(token))
        {
            if (stack.Count < 2) 
            {
                Console.WriteLine("Ошибка");
            }

            double num2 = stack.Pop();
            double num1 = stack.Pop();

            switch (token)
            {
                case "+":
                    stack.Push(num1 + num2);
                    break;
                case "-":
                    stack.Push(num1 - num2);
                    break;
                case "*":
                    stack.Push(num1 * num2);
                    break;
                case "/":
                    stack.Push(num1 / num2);
                    break;
                default:
                    throw new ArgumentException($"Этот оператор пока не поддерживается: {token}");
                    
            }
        }
        else
        {
            Console.WriteLine("Ошибка. В введенном выражении некорректные символы.");
        }
    }
    return stack.Pop();
}

static bool IsOperator(string token)
{
    return token == "+" || token == "-" || token == "*" || token == "/";
}