﻿﻿using System;

// the tech jobs class contains three methods that will drive our program’s functionality
namespace TechJobsConsoleAutograded6
{
	public class TechJobs
	{
        // our **********FIRST METHOD********** is run program
        public void RunProgram()
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);
            // break and end the program if the user doesn't select a choice
                if (actionChoice == null)
                {
                    break;
                }
                // code to execute if the user selects list column
                else if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);
                // will print out all the jobs in a list, no exceptions
                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);
                // prints "*** All ((Skill/Employer/Location/Position Type/)) Values ***" above the list of the correlating jobs from the search
                        Console.WriteLine(Environment.NewLine + "*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else //  // code to execute if the user selects column "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine(Environment.NewLine + "Search term: ");
                    string searchTerm = Console.ReadLine();

                    // search by the user's search term and Fetch the results
                    if (columnChoice.Equals("all"))
                    {
                        List<Dictionary<string, string>> searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                        // look in one column (either skill, employer, location, or position type NOT all) and search for the user's search term in that column, then print the results
                        List<Dictionary<string, string>> searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }

            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */

         // our **********SECOND METHOD********** is get user selection
        public string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                // printing out a prompt at the top, starting with View Jobs
                if (choiceHeader.Equals("View Jobs"))
                {
                    Console.WriteLine(Environment.NewLine + choiceHeader + " by (type 'x' to quit):");
                }
                else
                {
                    // prints either "Search by" or List by: depending on what the user selected
                    Console.WriteLine(Environment.NewLine + choiceHeader + " by:");
                }
        // puts that dash inbetween the key of numbers on the left column and the right column (skill/employer/location.position type/all)
                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }
        // exit out if the user doesn't select a column to list/search by afterall, and wants to exit
                string input = Console.ReadLine();
                if (input.Equals("x") || input.Equals("X"))
                {
                    return null;
                }
                else
                {
                    // turns users typed number into an int - unhandled exception if the user puts in a letter, instead of a number, that cant be parsed
                    choiceIdx = int.Parse(input);
                }
                    // if you put in a negative or too large number the program prompts you for a number again
                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    // if the number the user types makes sense in the range then...
                    isValidChoice = true;
                }

            } while (!isValidChoice);
                // reprint the choises list while the number the user inputed is not within range
            return choiceKeys[choiceIdx];
        }
// our **********THID METHOD********** is get user selectionprintjobs

       // TODO: complete the PrintJobs method.
        public void PrintJobs(List<Dictionary<string, string>> someJobs)
        { if(someJobs.Count > 0) {
             // Iterate over each dictionary in the list
            foreach (var job in someJobs)
            {
                // start with newline (to make sure a space is between the previous line (ex: search term: launchcode, or a previous job listing, then add the stars on next line))
                Console.WriteLine(Environment.NewLine + "*****");

                // Iterate over each key-value pair in the dictionary
                foreach (var kvp in job)
                {
                    // print the key columns on the left of a colon (name, employer, location, positin, core competency) and the actual values for the specific job on the right of the colon
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
                // print some stars at the end of the job description
                Console.WriteLine("*****");
            }
        }
        else {
            // if search term does not match any job listings, then print this for the user
            Console.WriteLine("No results");
        }

           
        }
    }
}