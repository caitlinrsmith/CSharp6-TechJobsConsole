﻿using System;
using System.Data;
using System.Text;

// There are three more methods in JobData

// FindAll()
// FindAll(string)
// FindByColumnAndValue(string, string)

namespace TechJobsConsoleAutograded6
{
    public class JobData
    {
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

// return all the jobs, no exceptions
        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();
            return AllJobs;
        }

        /*
         * Returns a list of all values contained in a given column,
         * without duplicates. (columns are: name,employer,location,position type,core competency)
         */
        public static List<string> FindAll(string column)
        {
            LoadData();

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> job in AllJobs)
            {
                string aValue = job[column];

                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }

            return values;
        }

        /**
         * Search all columns for the given term
         */

        //TODO: Complete the FindByValue method
        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();
            List<Dictionary<string, string>> foundJobs = new List<Dictionary<string, string>>();
        // first loop iterates over a list of all the job listings, & each job listing itself is a dictionary
            foreach (Dictionary<string, string> jobListing in AllJobs)
            {
                // second loop iterates over each job dictionary, looking at the values in the key value pairs (ie the details of the specific job listing)
                foreach (string deets in jobListing.Values)
                {
                    // looks at the specific details of a job listing, and puts it to upper to be case insensitive,
                    if (deets.ToUpper().Contains(value.ToUpper()) && !foundJobs.Contains(jobListing))
                    {
                        // adds the job listing to the results list if the job is NOT ALREADY in the search results
                      foundJobs.Add(jobListing);
                    }
                }
            }

            return foundJobs;
        }

        /**
         * Returns results of search the jobs data by key/value, using
         * inclusion of the search term.
         *
         * For example, searching for employer "Enterprise" will include results
         * with "Enterprise Holdings, Inc".
         */
        public static List<Dictionary<string, string>> FindByColumnAndValue(
            string column,
            string value
        )
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> row in AllJobs)
            {
                string aValue = row[column].ToUpper();

                //TODO: Make search case-insensitive
                if (aValue.Contains(value.ToUpper()))
                {
                    jobs.Add(row);
                }
            }

            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         After parsing the file data, it stores the data in the private property AllJobs which is of type List<Dictionary<string, string>>
         */
        private static void LoadData()
        {
            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(
            string row,
            char fieldSeparator = ',',
            char stringSeparator = '\"'
        )
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
