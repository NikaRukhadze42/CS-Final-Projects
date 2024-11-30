namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int randomNumber = random.Next(0,101);
            int stepCounter = 1;
            int userChoiceOfNumber;

            // Start the game
            ValidateAndGetUserChoiceOfNumber("Enter number from 0 to 100: ");

            // Check if user input is equals of randomNumber
            while (userChoiceOfNumber != randomNumber)
            {
                stepCounter++;
                if(userChoiceOfNumber > randomNumber)
                    ValidateAndGetUserChoiceOfNumber($"{userChoiceOfNumber} is higher, try lower. Enter new number: ");
                else
                    ValidateAndGetUserChoiceOfNumber($"{userChoiceOfNumber} is lower, try higher. Enter new number: ");
            }


            // Congratulations text
            Console.WriteLine($"Congratulations! {randomNumber} was the number! You needed {stepCounter} steps to find the correct number.");


            // Check user input and get valid value
            void ValidateAndGetUserChoiceOfNumber(string msg)
            {
                Console.Write(msg);
                while ((!int.TryParse(Console.ReadLine(), out userChoiceOfNumber)) || !(userChoiceOfNumber >= 0) || !(userChoiceOfNumber <= 100))
                {
                    Console.Write("Please enter valid number: ");
                }
            }
        }
    }
}
