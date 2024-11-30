using System.Text;
using System.Text.Json;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool userWon = false;
            int stepsCounter = 0;
            int invalidAnswersCounter = 0;
            List<char> wrongLetters = new List<char>();
            string word = GetWord();
            int minValue = 97;
            int maxValue = 122;


            var correctLetters = word.ToCharArray();
            char[] usersCorrectLetters = new char[correctLetters.Length];


            for (int i = 0; i < usersCorrectLetters.Length; i++)
            {
                usersCorrectLetters[i] = '-';
            }

            // The game
            while (invalidAnswersCounter < 8 && !userWon)
            {
                DrawStartingBoard();
                Console.Write("Press the letter: ");
                char userChoice = validateUserInput();
                

                ++stepsCounter;
                // check if letter is correct or not
                if (correctLetters.Contains(userChoice))
                {
                    FillWord(userChoice);
                }
                else
                {
                    ++invalidAnswersCounter;
                    wrongLetters.Add(userChoice);
                }
                Console.Clear();
                PrintGuessWord();
                DrawHangman(invalidAnswersCounter);
                PrintWrongCharsEntered();
            }

            // Endgame message.
            if(userWon)
            {
                Console.WriteLine();
                Console.WriteLine($"Congratulations! You guessed {word} in {stepsCounter} steps! You had {8 - invalidAnswersCounter} life(s) left.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"You failed to guess {word}. Steps: {stepsCounter}. Correctly guessed letters: {stepsCounter - invalidAnswersCounter}.");
            }





            void PrintGuessWord()
            {
                Console.Write("Guess the word: ");
                usersCorrectLetters.ToList().ForEach(correctLetter => Console.Write(correctLetter));
                Console.Write($" Letter count: ({correctLetters.Length}). Remaining Lifes: {8 - invalidAnswersCounter}.");
                Console.WriteLine();
            }

            void FillWord(char userChoice)
            {
                for (int i = 0; i < correctLetters.Count(); i++)
                {
                    if (correctLetters[i] == userChoice)
                    {
                        usersCorrectLetters[i] = correctLetters[i];
                    }
                }
                CheckIfUserWon();
            }

            void DrawHangman(int invalidAnswers)
            {
                Console.WriteLine();
                if (invalidAnswers == 1)
                {
                    Console.WriteLine("------");
                }
                else if (invalidAnswers == 2)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("     |");
                }
                else if (invalidAnswers == 3)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("     |");
                    Console.WriteLine("     O");
                }
                else if (invalidAnswers == 4)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("     |");
                    Console.WriteLine("     O");
                    Console.WriteLine("     |");
                }
                else if (invalidAnswers == 5)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("     |");
                    Console.WriteLine("     O");
                    Console.WriteLine("    /|");
                }
                else if (invalidAnswers == 6)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("     |");
                    Console.WriteLine("     O");
                    Console.WriteLine("    /|\\");
                }
                else if (invalidAnswers == 7)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("     |");
                    Console.WriteLine("     O");
                    Console.WriteLine("    /|\\");
                    Console.WriteLine("    / ");
                }
                else if (invalidAnswers == 8)
                {
                    Console.WriteLine("------");
                    Console.WriteLine("     |");
                    Console.WriteLine("     O");
                    Console.WriteLine("    /|\\");
                    Console.WriteLine("    / \\");
                }
            }

            void CheckIfUserWon()
            {
                userWon = correctLetters.SequenceEqual(usersCorrectLetters);
            }
            
            void PrintWrongCharsEntered()
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Wrong letters entered:");
                wrongLetters.ForEach(leter => Console.Write($" {leter};"));
                Console.WriteLine();
            }

            void DrawStartingBoard()
            {
                if (stepsCounter == 0)
                {
                    PrintGuessWord();
                }
            }

            char validateUserInput()
            {
                bool isValid = false;
                char userInput = ' ';
                while(!isValid)
                {
                    userInput = char.ToLower(Console.ReadKey().KeyChar);
                    while ((int)userInput < minValue || (int)userInput > maxValue)
                    {
                        Console.WriteLine();
                        Console.Write("Wrong input. Please enter valid letter: ");
                        userInput = char.ToLower(Console.ReadKey().KeyChar);
                    }
                    if (usersCorrectLetters.Contains(userInput) || wrongLetters.Contains(userInput))
                    {
                        Console.WriteLine();
                        Console.Write($"You already chose {userInput} letter. Choose Other letter: ");
                    }
                    else
                        isValid = true;
                }
                return userInput;
            }

            // Get word from API
            string GetWord()
            {
                string url = "https://random-word-api.vercel.app/api?words=1";
                HttpClient client = new HttpClient();
                var response = client.GetAsync(url).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                string word = JsonSerializer.Deserialize<List<string>>(responseBody)[0];
                return word;
            }
        }
    }
}