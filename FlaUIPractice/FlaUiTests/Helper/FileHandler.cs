﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlaUiTests.Helper
{
    /// <summary>
    /// Provides methods to read and write CSV files for test data.
    /// </summary>
    public class FileHandler
    {
        /// <summary>
        /// Reads a CSV file and returns a list of string arrays, where each array represents a row in the CSV file.
        /// </summary>
        /// <param name="filePath">File path to the csv file</param>
        /// <returns>List of string arrays</returns>
        /// <exception cref="FileNotFoundException">Thrown when the file does not exist</exception>"
        public static List<string[]> ReadCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }
            return File.ReadLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(','))
                .ToList();
        }

        /// <summary>
        /// Writes a list of string arrays to a CSV file, where each array represents a row in the CSV file.
        /// </summary>
        /// <param name="filePath">File path to the csv file</param>
        /// <param name="data">List of string array representing the data</param>
        /// <exception cref="FileNotFoundException">Thrown when the file does not exist</exception>"
        public static void WriteCsv(string filePath, List<string[]> data)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }
            string[] header = File.ReadLines(filePath).First().Split(',');
            using (var writer = new StreamWriter(filePath))
            {
                // Write the header
                writer.WriteLine(string.Join(",", header));
                for (int i = 0; i < data.Count; i++)
                {
                    writer.WriteLine(string.Join(",", data[i]));
                }
            }
        }
    }
}