using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Net.Mail;


namespace Receptenboek_C_
{
    public class Program
    {
            
        static string filePath = "recepten.json";



    static void CreateRecipe()
        {
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter the recípe's name:");

            recipe.name = Console.ReadLine();

            Console.WriteLine("Enter the recipe's description");

            recipe.description = Console.ReadLine();

            Console.WriteLine("Please enter the recipe's ingredients");

            recipe.ingredients = Console.ReadLine();

            Console.WriteLine("Please enter the recipes ingredient list.");

            

            Console.WriteLine("Please enter the preperation time in minutes");

            if (int.TryParse(Console.ReadLine(), out int preperationTime))
            {
                recipe.preperationTime = preperationTime;
            }
            else {
                Console.WriteLine("Invalid number");
            }

            Console.WriteLine("Please enter the instructions");

            recipe.instructions = Console.ReadLine();


            List<Recipe> recipes = LoadRecipes();
            recipes.Add(recipe);

            string json = JsonSerializer.Serialize(
                recipes,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);
            Console.WriteLine("Recipe Created");

            Thread.Sleep(2000);
            Console.Clear();
            MainMenu();


        }




        static List<Recipe> LoadRecipes()
        {
            if (!File.Exists(filePath))
            {
                return new List<Recipe>();
            }
            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Recipe>();
            }


            return JsonSerializer.Deserialize<List<Recipe>>(json)
                ?? new List<Recipe>();
        }


        static void DeleteRecipe() 
        {
            Console.WriteLine("Please enter a recipe number to delete");

            List<Recipe> recipes = LoadRecipes();
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].name}");
            }

            Console.WriteLine("Select a number to view a recipe.");
            int.TryParse(Console.ReadLine(), out int recipeToDelete);

            recipes.RemoveAt(recipeToDelete - 1);
            Console.WriteLine($"Recipe {recipeToDelete - 1} has been deleted.");
            Thread.Sleep(1000);
            Console.Clear();
            MainMenu();


        }


        static void ShowRecipes()
        {
            List<Recipe> recipes = LoadRecipes(); 
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].name}");
            }

            Console.WriteLine("Select a number to view a recipe.");
            int.TryParse(Console.ReadLine(), out int recipeToView);

            Console.WriteLine(recipes[recipeToView - 1]);

        }


        static void ShowOneRecipe(Recipe recipe)
        {

            Console.WriteLine();
            Console.WriteLine($"Name: {recipe.name}");
            Console.WriteLine($"Description: {recipe.description}");
            Console.WriteLine($"Ingredients: {recipe.ingredients}");
            Console.WriteLine($"preperation time: {recipe.preperationTime} minutes.");
            Console.WriteLine($"instructions: {recipe.instructions}");

            Console.WriteLine();
            Console.WriteLine("Enter r to return");
            if (Console.ReadLine() == "r")
            {
                Console.Clear();
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid entry");
            }
        }



        static void SearchRecipe()
        {
            Console.WriteLine("Enter text to search");
            string text = Console.ReadLine();

            List<Recipe> recipes = LoadRecipes();
            List<Recipe> matches = new List<Recipe>();


            foreach(Recipe recipe in recipes)
            {
                if (recipe.name.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    recipe.description.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    recipe.ingredients.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    recipe.instructions.Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    matches.Add(recipe);

                }
            }

            if (matches.Count == 0)
            {
                Console.WriteLine("No recipes found.");
                Thread.Sleep(1000);
                Console.Clear();
                return;
            }




            for (int i = 0; i < matches.Count; i++) 
            {
                Console.WriteLine($"{i + 1}. {matches[i].name}");


            }
            Console.WriteLine("Select a recipe.");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 1 && choice <= matches.Count)
                {
                    Recipe selectedRecipe = matches[choice - 1];

                    ShowOneRecipe(selectedRecipe);
                }

                else
                {
                    Console.WriteLine("Invalid selection.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    MainMenu();

                }
            }
            else 
            {
                Console.WriteLine("Please enter a number");
                Thread.Sleep(1000);
                Console.Clear();
                MainMenu();
            }


        }

        static void MainMenu()
        {
            Console.WriteLine("Hi, welcome! Please choose one of the following options:");
            Console.WriteLine("1: See your current recipe's");
            Console.WriteLine("2: Add a new recipe");
            Console.WriteLine("3: Search a recipe");
            Console.WriteLine("4: Delete a recipe");
            int.TryParse(Console.ReadLine(), out int choice);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Your current recipe's:");
                    ShowRecipes();
                    break;
                case 2:
                    Console.Clear();
                    CreateRecipe();
                    break;
                case 3:
                    Console.WriteLine("Search recipe's");
                    SearchRecipe();
                    break;
                case 4:
                    Console.WriteLine("Delete recipe");
                    DeleteRecipe();
                    break;
            }

        }

        static void Main(string[] args)

        {

            MainMenu();
        }

    }
}