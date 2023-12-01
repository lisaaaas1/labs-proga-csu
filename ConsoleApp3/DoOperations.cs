namespace ConsoleApp3;

public class DoOperations
{
    
    // Выполнение операции
    public double Calculate(double num1, double num2, char operation)
    {
        switch (operation)
        {
            case '+':
                return num1 + num2;
            case '-':
                return num1 - num2;
            case '*':
                return num1 * num2;
            case '/':
                if (num2 == 0) throw new DivideByZeroException("Делить на ноль нельзя");
                return num1 / num2;
            default:
                throw new ArgumentException("Неподдерживаемый оператор");
        }
    }
}