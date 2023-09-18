﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Vpat.DAO;
using Vpat.Models;
namespace Vpat.Logic
{
    public class InventoryHandler
    {
        FileIO fileIO = new FileIO();
        Dictionary<string, Tamagotchi> currentPets = new Dictionary<string, Tamagotchi>();

        /// <summary>
        /// Generates a Dictionary containing Pet Objects, using the lowercase Name property as the Key.
        /// </summary>
        /// <returns>Dictionary containing Pet Objects.</returns>
        /// <exception cref="Exception"></exception>
        public Dictionary<string, Tamagotchi> GeneratePetDictionary()
        {
            string[] pathArray = fileIO.ReadDirectory();

            if (pathArray.Length == 0)
            {
                return currentPets;
            }
            else
            {
                foreach (string filePath in pathArray)
                {
                    Tamagotchi pet = fileIO.ReadPet(filePath);

                    string petNameLower = pet.Name.ToLower();

                    currentPets.Add(petNameLower, pet);
                }
            }

            return currentPets;
        }

        /// <summary>
        /// Adds the Pet Object to the Dictionary, using the lowercase Name property as the Key.
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>Bool indicating if the Dictionary contains the newly created Pet Object.</returns>
        /// <exception cref="Exception"></exception>
        public bool AddToDictionary(Tamagotchi pet)
        {
            string petNameLower = pet.Name.ToLower();

            if (!currentPets.ContainsKey(petNameLower))
            {
                fileIO.WritePet(pet);

                currentPets.Add(petNameLower, pet);
            }
            else
            {
                throw new FileLoadException("ERROR: The specified pet already exists.");
            }

            return currentPets.ContainsKey(petNameLower);
        }

        /// <summary>
        /// Removes the given Pet Object from the Dictionary.
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>Bool indicating if the Dictionary no longer contains the deleted Pet Object.</returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteFromDictionary(string petName)
        {
            string petNameLower = petName.ToLower();

            if (currentPets.ContainsKey(petNameLower))
            {
                Tamagotchi pet = currentPets[petNameLower];
                
                fileIO.DeletePet(pet);               
                currentPets.Remove(petNameLower);
            }
            else
            {
                throw new KeyNotFoundException("ERROR: The specified pet does not exist.");
            }

            return !currentPets.ContainsKey(petNameLower);
        }

        /// <summary>
        /// Writes the current pets to the Console.
        /// </summary>
        public void PrintCurrentPets()
        {
            if (currentPets.Count > 0)
            {
                foreach (KeyValuePair<string, Tamagotchi> item in currentPets)
                {
                    TimeSpan age = item.Value.CalculateAge(item.Value.Birthday);

                    if (age.Days >= 365)
                    {
                        int yearCounter = 0;
                        int ageDays = age.Days;

                        while (ageDays >= 365)
                        {
                            yearCounter++;
                            ageDays -= 365;
                        }

                        string estimatedAge = $"{yearCounter} year(s) and {ageDays} day(s) old";

                        Console.WriteLine();
                        Console.WriteLine($"Name: {item.Value.Name}");
                        Console.WriteLine($"Pet Type: {item.Value.Type}");
                        Console.WriteLine($"Birthday: {item.Value.Birthday.ToString("g", CultureInfo.CreateSpecificCulture("en-us"))}");
                        Console.WriteLine($"Age: {estimatedAge}");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Name: {item.Value.Name}");
                        Console.WriteLine($"Pet Type: {item.Value.Type}");
                        Console.WriteLine($"Birthday: {item.Value.Birthday.ToString("g", CultureInfo.CreateSpecificCulture("en-us"))}");
                        Console.WriteLine($"Age: {age.Days} day(s) old");
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("There are no active pets.");
            }
        }
    }
}