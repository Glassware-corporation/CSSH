namespace CSSH;
class Program 
{
    private static bool tryToFixErrors;
    private static String tempstr = "temp";

    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Console.WriteLine("Arguments Passed by the runner:");
            foreach (Object argv in args)
            {
                Console.Write(argv + "::");
            }

        }
        else
        {
            Console.WriteLine("No command line arguments found.");
        }

        // settings
        try
        {
            tryToFixErrors = Convert.ToBoolean(args[0]);
        }
        catch
        {
            Console.WriteLine("tryToFixErrors got an error, Must be either be false or true :: defaults to false");
            tryToFixErrors = false;
        }

        // call varibles 
        int rndIntSes = StartStatement();
        List<string> shInput = new List<string>();

        // shell loop
        while (true)
        {
            Console.Write("CSSH >>> ");
            shInput.Add(item: Convert.ToString(Console.ReadLine()));
            //shInput[shInput.Count - 1]
            proccesSyntax(shInput[shInput.Count - 1], shInput, tryToFixErrors);
        }
    }

    static void proccesSyntax(String input, List<string> shInput, bool errFixPass = false)
    {
        String[] inputSplits = input.Split();
        switch (inputSplits[0])
        {
            case "exit":
                Console.WriteLine("goodbye...");
                try
                {
                    Environment.Exit(Convert.ToInt32(inputSplits[1]));
                }  catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("\nERROR ON EXIT -- MAKE SURE ITS AN NUMBER (INT) NOT A STRING (WORDS)");
                    if (errFixPass)
                    {
                        Console.WriteLine("Becuase of settings, CSSH can interpret and try to succesfully run \"exit\"...\ntrying...");
                        Environment.Exit(1);
                    }                   
                    else
                    {
                        break;
                    }
                }
                break;
            case "echo":
                Console.WriteLine(inputSplits[1]);
                break;
            case "whatis":
                try
                {
                    tempstr = inputSplits[1];
                } catch (Exception ex) when (ex is IndexOutOfRangeException)
                {
                    Console.WriteLine($"{ex}\n\n");
                    ErrorMessage("Add an argument... Example: shInput");
                    break;
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.ToString());
                    Console.WriteLine("\n\nUnknown error...");
                    break;
                }
                switch (inputSplits[1])
                {
                    case "shInput":
                        foreach (var line in shInput)
                        {
                            Console.WriteLine($"{line}");
                        }
                        break;
                    case "tryToFixErrors":
                        Console.WriteLine(tryToFixErrors);
                        break;
                    default:
                        ErrorMessage("NON-VALUE");
                        break;
                }
                break;
            default:
                Console.WriteLine("\"" + input  + "\" Is not a vaild Statement!\n");
                break;
        }
    }

    static int StartStatement()
    {
        Random rnd = new Random();
        String[] tips = {
            "Insted of \"clear\", you can use \"cls\"",
            "write ddrive to quickly get to the D drive",
            "write cdrive to quickly get to the C drive",
            "CSSH is very fimalier to the linux command line, BUT for windows"
        };

        Console.WriteLine("\n\nWelcome to CSSH (C Sharp Shell)\n(C) 2023, Glassware Corporation. All rights reserved.");
        int randNum = rnd.Next(0, tips.Length);
        Console.WriteLine("Random Int for the session: " + randNum);
        Console.WriteLine("Tip of the session: " + tips[randNum]);
        for (int i = 0; i < Console.WindowWidth; i++)
        {
            Console.Write("=");
        }

        return randNum;
    }

    static void ErrorMessage(string message)
    {
        Console.Error.WriteLine("ERROR: " + message);
    }
}