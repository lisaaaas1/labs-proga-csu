using System.Text;
namespace withRPN;

public class RPN
{
    static public float ResultRPN(string input)
    {
        string output = DoExpressionToRPN(input); //Преобразовываем выражение в постфиксную запись
        float result = CalculateRPN(output); //Решаем полученное выражение
        return result; //Возвращаем результат
    }

    // метод преобразовывает вводимую строку к виду ОПЗ
    static public string DoExpressionToRPN(string expression)
    {
        string result = string.Empty;
        Stack<char> operators = new Stack<char>(); // хранение операторов

        for (int i = 0; i < expression.Length; i++)
        {
            // если разделитель
            if (IsDelimeter(expression[i]))
                continue; //Переходим к следующему символу

            // если символ
            if (Char.IsDigit(expression[i])) //Если цифра
            {
                // записываем число до разделителя или оператора
                while (!IsDelimeter(expression[i]) && !IsOperator(expression[i]))
                {
                    result += expression[i];
                    i++;

                    if (i == expression.Length)
                    {
                        break;
                    }
                }

                result += " "; // добавляем разделитель
                i--; // возвращаемся к символу перед разделителем
            }

            // если оператор
            if (IsOperator(expression[i]))
            {
                // если открывающая скобка
                if (expression[i] == '(')
                    operators.Push(expression[i]); // записываем её в стек
                // если закрывающая скобка
                else if (expression[i] == ')')
                {
                    // выписываем все операторы до открывающей скобки в строку
                    char operator1 = operators.Pop();

                    while (operator1 != '(')
                    {
                        result += operator1.ToString() + ' ';
                        operator1 = operators.Pop();
                    }
                }
                // остальные операторы
                else
                {
                    // если в стеке остались элементы
                    if (operators.Count > 0)
                    {
                        // если приоритет текущего оператора из перебора меньше
                        // или равен приоритету оператора на вершине стека
                        if (GetPriority(expression[i]) <= GetPriority(operators.Peek())) 
                        {
                            // то добавляем последний оператор из стека в строку с выражением
                            result += operators.Pop().ToString() + " "; }
                    }
                    // если стек пуст, или же приоритет оператора выше - добавляем операторы на вершину стека
                    operators.Push(char.Parse(expression[i].ToString())); 
                }
            }
        }

        // выкидываем из стека все оставшиеся операторы в строку ОПЗ
        while (operators.Count > 0)
        {
             result += operators.Pop() + " ";
        }
        return result;
    }

    // метод вычисления выражения опз
    static public float CalculateRPN(string expressionRPN)
    {
        Stack<float> stack = new Stack<float>();
        float calculate = 0;
        for (int element = 0; element < expressionRPN.Length; element++)
        {
            // если число
            if (Char.IsDigit(expressionRPN[element]))
            {
                string digitMemory = string.Empty;

                // извлекаем число пока не встретим определитель или оператор
                while (!IsDelimeter(expressionRPN[element]) && !IsOperator(expressionRPN[element]))
                {
                    digitMemory += expressionRPN[element];
                    element++;
                    if (element == expressionRPN.Length) break;
                }

                // записываем число в стек
                stack.Push(float.Parse(digitMemory));
                element--;
            }
            // если символ оператор 
            else if (IsOperator(expressionRPN[element]))
            {
                float num2 = stack.Pop();
                float num1 = stack.Pop();

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
                        calculate = (float.Parse(Math.Pow(float.Parse(num2.ToString()), float.Parse(num1.ToString()))
                            .ToString()));
                        break;
                    default:
                        throw new ArgumentException($"Этот оператор пока не поддерживается: {expressionRPN[element]}");
                }

                stack.Push(calculate);
            }
        }

        return stack.Peek();
    }

    // расстановка приоритета операций
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

    // проверка что символ - разделитель
    static public bool IsDelimeter(char symbol)
    {
        // если указанные в кавычках символы не найдены в symbol, метод возвращает -1
        // если символ соответствует одному из указанных в кавычках - значит это разделитель
        if ((" =".IndexOf(symbol) != -1))
        {
            return true;
        }
        else return false;
    }

    // проверка, что символ - оператор
    static public bool IsOperator(char symbol)
    {
        // если указанные в кавычках символы не найдены в symbol, метод возвращает -1
        // если символ соответствует одному из указанных в кавычках - значит это разделитель
        if (("+-/*^()".IndexOf(symbol) != -1)) return true;
        else
        {
            return false;
        }
    }
}