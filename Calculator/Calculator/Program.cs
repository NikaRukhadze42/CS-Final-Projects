namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get first number
            double number1;
            Console.Write("Enter first number: ");
            while (!double.TryParse(Console.ReadLine(), out number1))
            {
                Console.Write("Your first number is invalid. Enter again: ");
            }

            // Get Second number
            double number2;
            Console.Write("Enter second number: ");
            while (!double.TryParse(Console.ReadLine(), out number2))
            {
                Console.Write("Your Second number is invalid. Enter again: ");
            }

            // Get Math Operation
            Console.Write("Enter math operation (+,-,*,/): ");
            string mathOperation = Console.ReadLine();
            while ( (mathOperation != "+") && (mathOperation != "-") && (mathOperation != "*") && (mathOperation != "/"))
            {
                Console.Write("Your math operation is invalid. Enter again (+,-,*,/): ");
                mathOperation = Console.ReadLine();
            }


            // Use Calculator methods
            if (mathOperation == "+")
            {
                Console.WriteLine($"{number1} + {number2} = {number1 + number2}");
                Calculator.Addition(number1, number2);
            }
            else if (mathOperation == "-")
            {
                Console.WriteLine($"{number1} - {number2} = {number1 - number2}");
                Calculator.Subtraction(number1, number2);
            }
            else if (mathOperation == "*")
            {
                Console.WriteLine($"{number1} * {number2} = {number1 * number2}");
                Calculator.Multiplication(number1, number2);
            }
            else if (mathOperation == "/")
            {
                if(number2 != 0)
                {
                    Console.WriteLine($"{number1} / {number2} = {number1 / number2}");
                    Calculator.Division(number1, number2);
                }
                else if(number2 == 0)
                    Console.WriteLine("Division by zero is not allowed.");
            }
        }
        class Calculator
        {
            static public double Addition(double a, double b) => a + b;
            static public double Subtraction(double a, double b) => a - b;
            static public double Multiplication(double a, double b) => a * b;
            static public double Division(double a, double b) => a / b;
        }
    }
}