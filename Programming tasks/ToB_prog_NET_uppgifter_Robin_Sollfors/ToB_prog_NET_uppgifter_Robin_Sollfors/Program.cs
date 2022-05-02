// See https://aka.ms/new-console-template for more information
using System;

class TaskNET
{
    //Console.WriteLine("Hello, World!");

    static Random rnd = new Random();
    static string filePath = Directory.GetCurrentDirectory() + "\\textFile.txt";
    static Character player = new Character(), opponent = new Character();
    //string fileDir = Directory.GetCurrentDirectory();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine(
                "0: Exit\n" +
                "1: Hello world!\n" +
                "2: Who are you?\n" +
                "3: Random colored text\n" +
                "4: Todays date and time\n" +
                "5: Which number is larger?\n" +
                "6: Guess the number\n" +
                "7: Write a text file on the computer\n" +
                "8: Read a text file on the computer\n" +
                "9: Square, power of 2 and power of 10 for a value\n" +
                "10: Multiplication table 10x10\n" +
                "11: Random aray + sort\n" +
                "12: Palindrom?\n" +
                "13: Write every number between two numbers\n" +
                "14: Sort numbers by even and odd\n" +
                "15: Sum values\n" +
                "16: Player and opponent\n");

            string line = Console.ReadLine();
            int functionNumber;
            if (int.TryParse(line, out functionNumber))
            {
                bool repeat = true;
                while (repeat)
                {
                    switch (functionNumber)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;

                        case 1:
                            Function1();
                            break;

                        case 2:
                            Function2();
                            break;

                        case 3:
                            Function3();
                            break;

                        case 4:
                            Function4();
                            break;

                        case 5:
                            Function5();
                            break;

                        case 6:
                            Function6();
                            break;

                        case 7:
                            Function7();
                            break;

                        case 8:
                            Function8();
                            break;

                        case 9:
                            Function9();
                            break;

                        case 10:
                            Function10();
                            break;

                        case 11:
                            Function11();
                            break;

                        case 12:
                            Function12();
                            break;

                        case 13:
                            Function13();
                            break;

                        case 14:
                            Function14();
                            break;

                        case 15:
                            Function15();
                            break;

                        case 16:
                            Function16();
                            break;


                        default:
                            Console.WriteLine("No function for this int, please try again");
                            repeat = false;
                            break;
                    }

                    if (repeat == true)
                    {
                        Console.WriteLine("\nRepeat function? \"0\" for no, \"1\" for yes");
                        line = Console.ReadLine();
                        if (line == "1")
                        {
                            repeat = true;
                            Console.WriteLine("\n");
                        }
                        else if (line == "0")
                        {
                            repeat = false;
                            Console.WriteLine("\n");
                        }

                        else
                        {
                            repeat = false;
                            Console.WriteLine("Not a valid input, function not repeating");
                            Console.WriteLine("\n");
                        }
                    }

                }
            }
            else { Console.WriteLine("Please input an int"); }
        }
    }

    static void Function1()
    {
        Console.WriteLine("Hello, World!");
    }

    static void Function2()
    {
        Console.WriteLine("\n" + "What is your first name?");
        string userNameFirst = Console.ReadLine();
        
        Console.WriteLine("\n" + "What is your last name?");
        string userNameLast = Console.ReadLine();
        
        Console.WriteLine("\n" + "How old are you?");
        int userAge = 0;
        bool valid = false;
        while (!valid)
        {
            valid = int.TryParse(Console.ReadLine(), out userAge);
            if (!valid  || userAge < 0)
            {
                Console.WriteLine("Invalid age, please input a valid age");
                valid = false;
            }
        }

        Console.WriteLine("\nHello " + userNameFirst + " " + userNameLast + " (" + userAge.ToString() + ")");
    }

    #region function3
    static bool changedColor = false;
    static void Function3()
    {
        if (!changedColor)
        {
            Console.ForegroundColor = RandomColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        changedColor = !changedColor;
        Console.WriteLine("Current color:");
    }
    
    static ConsoleColor RandomColor()
    {
        ConsoleColor rndColor = ConsoleColor.Gray;
        bool notBlackOrGray = false;
        while (!notBlackOrGray)
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            rndColor = (ConsoleColor)consoleColors.GetValue(rnd.Next(consoleColors.Length));
            if (rndColor != ConsoleColor.Black && rndColor != ConsoleColor.Gray) { notBlackOrGray = true; }
        }

        return rndColor;
    }
    #endregion


    static void Function4()
    {
        Console.WriteLine("The current date is: " + DateTime.Now.Date.ToString("yyyy/MM/dd") + "\nAnd the time is: " + DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss"));
    }

    static void Function5()
    {
        float value1 = 0, value2 = 0;
        Console.WriteLine("\n" + "Input two values to check which is bigger");
        Console.WriteLine("\n" + "Please input the first value");

        bool valid = false;
        while (!valid)
        {
            valid = float.TryParse(Console.ReadLine(), out value1);
            if (!valid)
            {
                Console.WriteLine("Invalid value, please input a valid value");
            }
        }

        Console.WriteLine("\n" + "Please input the second value");
        valid = false;
        while (!valid)
        {
            valid = float.TryParse(Console.ReadLine(), out value2);
            if (!valid)
            {
                Console.WriteLine("Invalid value, please input a valid value");
                valid = false;
            }
        }

        if(value1 > value2)
        {
            Console.WriteLine("\nThe first value is larger: " + value1.ToString());
        }
        else if (value1 < value2)
        {
            Console.WriteLine("\nThe second value is larger: " + value2.ToString());
        }
        else
        {
            Console.WriteLine("\nThey are the same value: " + value1.ToString());
        }
    }

    static void Function6()
    {
        int numberToFind = rnd.Next(99) + 1;
        Console.WriteLine("Random number generated between 1 and 100, please guess the number." +
            "\nYou can exit/giveup by typing \"0\"");
        bool numberFound = false, valid = false;
        int guessedNumber = 0, numberOfGuesses = 0;

        while (!numberFound)
        {
            while (!valid)
            {
                valid = int.TryParse(Console.ReadLine(), out guessedNumber);
                if (!valid)
                {
                    Console.WriteLine("Invalid value, please input a valid value");
                }

                else if (guessedNumber < 0 || guessedNumber > 100)
                {
                    Console.WriteLine("Please input a value between 0 and 100");
                    valid = false;
                }
            }

            valid = false;
            numberOfGuesses++;

            if (guessedNumber == numberToFind)
            {
                Console.WriteLine("Congratulations, you found the number " + numberToFind.ToString() + ", and it took you this amount of tries: " + numberOfGuesses.ToString());
                numberFound = true;
            }

            else if (guessedNumber == 0)
            {
                Console.WriteLine("You gave up after " + (numberOfGuesses - 1).ToString() + " number of tries, better luck next time." +
                    "\nExiting function");
                numberFound = true;
            }

            else if (guessedNumber > numberToFind)
            {
                Console.WriteLine("Too high");
            }

            else if (guessedNumber < numberToFind)
            {
                Console.WriteLine("Too low");
            }
        }
    }

    #region function 7&8 / write read
    static void Function7()
    {
        StreamWriter sw = File.CreateText(filePath);

        Console.WriteLine("Please input text");
        string text = Console.ReadLine();
        sw.WriteLine(text);
        sw.Close();
    }

    static void Function8()
    {
        StreamReader sr = File.OpenText(filePath);
        string s = "";
        while ((s = sr.ReadLine()) != null)
        {
            Console.WriteLine(s);
        }
        sr.Close();
    }
    #endregion

    static void Function9()
    {
        Console.WriteLine("Please input the value");
        float number = 0;

        bool valid = false;
        while (!valid)
        {
            valid = float.TryParse(Console.ReadLine(), out number);
            if (!valid)
            {
                Console.WriteLine("Invalid value, please input a valid value");
            }
        }

        float squareRoot = MathF.Sqrt(number);
        float elevated2 = number * number;
        float elevated10 = MathF.Pow(number, 10);

        Console.WriteLine("The square of {0} is: {1}" +
            "\n{0} to the power of 2 is: {2}" +
            "\n{0} to the power of 10 is: {3}", number, squareRoot, elevated2, elevated10);
    }

    static void Function10()
    {
        Console.WriteLine("\n");
        for (int i = 1; i < 11; i++)
        {
            string s = "";
            for (int j = 1; j < 11; j++)
            {
                s += (i * j).ToString() + "\t";
            }
            Console.WriteLine(s);

            if (i != 10) { Console.WriteLine("\n"); }
        }
    }

    #region function11
    static void Function11()
    {
        Console.WriteLine("What is the size of the array?");
        bool valid = false;
        int arraySize = 1;
        while (!valid)
        {
            valid = int.TryParse(Console.ReadLine(), out arraySize);
            if (!valid || arraySize < 1)
            {
                Console.WriteLine("Invalid value, please input a valid value");
                valid = false;
            }
        }

        int highestRandomNumber = 0;
        Console.WriteLine("What is the highest number?");
        valid = false;
        while (!valid)
        {
            valid = int.TryParse(Console.ReadLine(), out highestRandomNumber);
            if (!valid || highestRandomNumber < 0)
            {
                Console.WriteLine("Invalid value, please input a valid value");
                valid = false;
            }
        }


        int[] arrayUnsorted = new int[arraySize];
        int[] arraySorted;

        for (int i = 0; i < arrayUnsorted.Length; i++)
        {
            arrayUnsorted[i] = rnd.Next(highestRandomNumber);
        }

        arraySorted = sortArrayInt(arrayUnsorted);

        string s = "Unsorted array:\n";
        for (int i = 0; i < arrayUnsorted.Length; i++)
        {
            s += arrayUnsorted[i].ToString() + "\t";
        }
        Console.WriteLine(s);

        s = "\nSorted array:\n";
        for (int i = 0; i < arraySorted.Length; i++)
        {
            s += arraySorted[i].ToString() + "\t";
        }
        Console.WriteLine(s);
    }

    static int[] sortArrayInt (int[] arrayToSort)
    {
        int[] sortedArray = new int[arrayToSort.Length];
        for(int i = 0; i < arrayToSort.Length; i++)
        {
            sortedArray[i] = arrayToSort[i];
        }

        for (int i = 0; i < sortedArray.Length - 1; i++)
        {
            for (int j = 0; j < sortedArray.Length - i - 1; j++)
            {
                if(sortedArray[j] > sortedArray[j + 1])
                {
                    int temp = sortedArray[j];
                    sortedArray[j] = sortedArray[j + 1];
                    sortedArray[j + 1] = temp;
                }
            }
        }

        return sortedArray;
    }
    #endregion

    static void Function12()
    {
        Console.WriteLine("Please input the word to see if it is a palindrom");
        string s = Console.ReadLine();
        string str = s.ToLower();
        str = str.Replace(" ", "");
        str = str.Replace("-", "");

        bool palindrom = true;
        for (int i = 0; i < str.Length/2; i++)
        {
            if (str[i] != str[str.Length - i - 1])
            {
                palindrom = false;
                break;
            }
        }

        if (palindrom)
        {
            Console.WriteLine("{0} is a palindrom", s);
        }
        else
        {
            Console.WriteLine("{0} is not a palindrom", s);
        }
    }

    static void Function13()
    {
        int value1 = 0, value2 = 0;
        bool valid = false;
        Console.WriteLine("Enter first value");
        while (!valid)
        {
            valid = int.TryParse(Console.ReadLine(), out value1);
            if (!valid)
            {
                Console.WriteLine("Invalid value, please input a valid value");
            }
        }

        valid = false;
        Console.WriteLine("Enter second value");
        while (!valid)
        {
            valid = int.TryParse(Console.ReadLine(), out value2);
            if (!valid)
            {
                Console.WriteLine("Invalid value, please input a valid value");
            }
        }

        string s = "";

        if (value1 < value2)
        {
            for (int i = value1 + 1; i < value2; i++)
            {
                s += i.ToString() + "\t";
            }

            Console.WriteLine(s);
        }

        else if (value2 < value1)
        {
            for (int i = value2 + 1; i < value1; i++)
            {
                s += i.ToString() + "\t";
            }

            Console.WriteLine(s);
        }

        else { Console.WriteLine("Same value, no numbers inbetween"); }
    }

    static void Function14()
    {
        Console.WriteLine("Please enter values seperated by a \",\" (comma)");

        int[] numbers = new int[1];
        bool valid = false;
        while (!valid)
        {
            string[] s = Console.ReadLine().Split(",");
            numbers = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                valid = int.TryParse(s[i], out numbers[i]);
                if (!valid)
                {
                    Console.WriteLine("Invalid value, please input a valid value");
                    break;
                }
            }
        }

        numbers = sortArrayInt(numbers);

        string evenNumbers = "";
        string oddNumbers = "";

        for(int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] % 2 == 0)
            {
                evenNumbers += numbers[i].ToString() + "\t";
            }
            else
            {
                oddNumbers += numbers[i].ToString() + "\t";
            }
        }

        Console.WriteLine("Odd numbers:\n{0}", oddNumbers);

        Console.WriteLine("\nEven numbers:\n{0}", evenNumbers);
    }

    static void Function15()
    {
        Console.WriteLine("Please enter values seperated by a \",\" (comma)");

        int[] numbers = new int[1];
        bool valid = false;
        while (!valid)
        {
            string[] s = Console.ReadLine().Split(",");
            numbers = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                valid = int.TryParse(s[i], out numbers[i]);
                if (!valid)
                {
                    Console.WriteLine("Invalid value, please input a valid value");
                    break;
                }
            }
        }

        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }

        Console.WriteLine("Sum of values: {0}", sum);
    }

    static void Function16()
    {
        if(player.Name != null)
        {
            Console.WriteLine("Do you want to display the characters or replace them? \"0\" for display, \"1\" for replace");

            string line = Console.ReadLine();
            if (line == "1")
            {
                ReplaceCharacters();
            }
            else if (line == "0")
            {

            }
        }

        else
        {
            ReplaceCharacters();
        }

        DisplayCharacters();
    }

    static void ReplaceCharacters()
    {
        Console.WriteLine("Please enter the name of your character");
        player.Name = Console.ReadLine();
        player.Health = rnd.Next(9) + 1;
        player.Strength = rnd.Next(9) + 1;
        player.Luck = rnd.Next(9) + 1;

        Console.WriteLine("Please enter the name of your opponent");
        opponent.Name = Console.ReadLine();
        opponent.Health = rnd.Next(9) + 1;
        opponent.Strength = rnd.Next(9) + 1;
        opponent.Luck = rnd.Next(9) + 1;
    }

    static void DisplayCharacters()
    {
        Console.WriteLine("");
        player.Display();
        Console.WriteLine("");
        opponent.Display();
    }
}


class Character
{
    public string? Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Luck { get; set; }

    public Character()
    {
        Name = null;
        Health = 0;
        Strength = 0;
        Luck = 0;
    }

    public Character(string nm, int hp, int str, int luc)
    {
        string Name = nm;
        int Health = hp;
        int Strength = str;
        int Luck = luc;
    }

    public void Display()
    {
        Console.WriteLine("Name: {0}, Health: {1}, Strength: {2}, Luck: {3}", Name, Health, Strength, Luck);
    }
}