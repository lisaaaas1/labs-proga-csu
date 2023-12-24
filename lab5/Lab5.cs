namespace lab5;

public class Lab5
{
    // Базовый класс для всех токенов (число, оператор, скобка)

    public abstract class Token
    {
        public abstract string Symbol { get; }
    }

// Класс для представления скобок
    public class Parenthesis : Token
    {
        public override string Symbol => "()";
    }

// Класс для представления чисел
    public class Number : Token
    {
        public float Value { get; }

        public Number(float value)
        {
            Value = value;
        }

        public override string Symbol => Value.ToString();
    }

// Класс для представления операторов
    public class Operation : Token
    {
        public string Operator { get; }
        public int Priority { get; }

        public Operation(string op, int priority)
        {
            Operator = op;
            Priority = priority;
        }

        public override string Symbol => Operator;
    }

// Класс, реализующий алгоритм преобразования выражения в ОПЗ
    public class RPN
    {
        // Метод для вычисления результата выражения в ОПЗ
        static public float ResultRPN(string input)
        {
            string output = DoExpressionToRPN(input);
            float result = CalculateRPN(output);
            return result;
        }

        // Метод преобразования выражения в ОПЗ
        static public string DoExpressionToRPN(string expression)
        {
            string result = string.Empty;
            Stack<Token> operators = new Stack<Token>(); // стек для операторов и скобок

            for (int i = 0; i < expression.Length; i++)
            {
                // если разделитель, пропускаем
                if (IsDelimeter(expression[i]))
                    continue;

                // если цифра, добавляем число в выходную строку
                if (Char.IsDigit(expression[i]))
                {
                    while (!IsDelimeter(expression[i]) && !IsOperator(expression[i]))
                    {
                        result += expression[i];
                        i++;

                        if (i == expression.Length)
                        {
                            break;
                        }
                    }

                    result += " ";
                    i--;
                }

                // если оператор
                if (IsOperator(expression[i]))
                {
                    if (expression[i] == '(')
                        operators.Push(new Parenthesis());
                    else if (expression[i] == ')')
                    {
                        // выписываем операторы до открывающей скобки в выходную строку
                        while (operators.Peek() is Operation)
                        {
                            result += operators.Pop().Symbol + ' ';
                        }

                        operators.Pop(); // убираем открывающую скобку
                    }
                    else
                    {
                        // обработка операторов
                        while (operators.Count > 0 && operators.Peek() is Operation &&
                               ((Operation)operators.Peek()).Priority >= GetPriority(expression[i]))
                        {
                            result += operators.Pop().Symbol + " ";
                        }

                        operators.Push(new Operation(expression[i].ToString(), GetPriority(expression[i])));
                    }
                }
            }

            // выкидываем из стека все оставшиеся операторы в выходную строку
            while (operators.Count > 0)
            {
                result += operators.Pop().Symbol + " ";
            }

            return result;
        }

        // Метод для вычисления результата ОПЗ
        static public float CalculateRPN(string expressionRPN)
        {
            Stack<float> stack = new Stack<float>();
            float calculate = 0;

            for (int element = 0; element < expressionRPN.Length; element++)
            {
                // если число, добавляем в стек
                if (Char.IsDigit(expressionRPN[element]))
                {
                    string digitMemory = string.Empty;

                    while (!IsDelimeter(expressionRPN[element]) && !IsOperator(expressionRPN[element]))
                    {
                        digitMemory += expressionRPN[element];
                        element++;

                        if (element == expressionRPN.Length)
                        {
                            break;
                        }
                    }

                    stack.Push(float.Parse(digitMemory));
                    element--;
                }
                // если оператор
                else if (IsOperator(expressionRPN[element]))
                {
                    float num2 = stack.Pop();
                    float num1 = stack.Pop();

                    // обработка операторов
                    switch (expressionRPN[element])
                    {
                        case '+':
                            calculate = num1 + num2;
                            break;
                        case '-':
                            calculate = num1 - num2;
                            break;
                        case '*':
                            calculate = num1 * num2;
                            break;
                        case '/':
                            calculate = num1 / num2;
                            break;
                        case '^':
                            calculate = (float.Parse(Math
                                .Pow(float.Parse(num2.ToString()), float.Parse(num1.ToString()))
                                .ToString()));
                            break;
                        default:
                            throw new ArgumentException($"Unsupported operator: {expressionRPN[element]}");
                    }

                    stack.Push(calculate);
                }
            }

            return stack.Peek();
        }

        // Получение приоритета оператора
        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 6;
            }
        }

        // Проверка, что символ - разделитель
        static public bool IsDelimeter(char symbol)
        {
            return " =".IndexOf(symbol) != -1;
        }

        // Проверка, что символ - оператор
        static public bool IsOperator(char symbol)
        {
            return "+-/*^()".IndexOf(symbol) != -1;
        }
    }
}