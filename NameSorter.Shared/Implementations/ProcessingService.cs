﻿using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameSorter.Shared.Implementations
{
    public class ProcessingService : IProcessingService
    {
        #region Fields

        private readonly ISortingService sortingService;

        private readonly IInputFileService inputFileService;

        #endregion

        public ProcessingService(
            ISortingService sortingSrv,
            IInputFileService inputFileSrv)
        {
            sortingService = sortingSrv;
            inputFileService = inputFileSrv;
        }

        public async Task ProcessAsync(string filename)
        {
            try
            {
                // Read the list of names from the input file
                var names = await inputFileService.ReadAsync(filename);
                if (names == null || !names.Any())
                {
                    DisplayError("The input file does not contain any names");
                    return;
                }

                // Validate the list of names
                var invalidNames = new List<NameModel>();
                foreach (var name in names)
                {
                    if (!name.Validate())
                        invalidNames.Add(name);
                }

                if (invalidNames.Any())
                {
                    var invalidNamesStr = "Invalid name(s) detected";
                    invalidNamesStr += Environment.NewLine;

                    foreach (var name in invalidNames)
                        invalidNamesStr += " - " + name.ToString() + Environment.NewLine;
                    
                    DisplayError(invalidNamesStr);
                    return;
                }

                // Display the list of names before sorting
                Console.WriteLine("------------------------");
                Console.WriteLine("----------Input---------");
                DisplayList(names);

                // Sort the list
                names = sortingService.Sort(names);

                // Display the list of names after sorting
                Console.WriteLine();
                Console.WriteLine("------------------------");
                Console.WriteLine("---------Output---------");
                DisplayList(names);
                Console.WriteLine();

                // Write the sorted list to an output file
                var newFilename = "sorted-names-list.txt";
                await inputFileService.WriteAsync(newFilename, names);
            }
            catch(Exception ex)
            {
                DisplayError(ex.Message);
            }
        }

        #region Private methods
        
        private void DisplayList(IEnumerable<NameModel> names)
        {
            if (names == null)
                return;

            foreach (var name in names)
                Console.WriteLine(name.ToString());
        }

        private void DisplayError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        #endregion
    }
}
