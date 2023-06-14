using System;
using System.Collections.Generic;
namespace RecipeBook
{
    /*-------
    Class
    -------
    This class represents the Recipe Book and contains variables and methods for managing recipes. It allows users to enter new
    recipes, display existing recipes, scale recipes, reset ingredient quantities, and clear recipes. It provides a user interface
    for interacting with the Recipe Book and performs calculations such as calculating total calories.
    */
    public class RecipeDetails
    {
        #region Variables
        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Variables
        These are used to store information regarding the recipe.*/
        private List<Recipe> recipes = new List<Recipe>();
        private List<string> recipeNames = new List<string>();
        private int numIngredients;
        private List<string> ingredientNames = new List<string>();
        private List<double> ingredientQuantities = new List<double>();
        private List<string> ingredientUnits = new List<string>();
        private int numSteps;
        private List<string> stepDescriptions = new List<string>();
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion

        #region Methods
        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method prompts the user to enter details for a new recipe. It captures the recipe name, number of ingredients, and
        for each ingredient, captures its name, quantity, unit of measurement, calories, and food group. It then adds the recipe
        to the Recipe Book.
        */
        public void EnterRecipe()
        {
            Console.WriteLine("Enter the name of the recipe:");
            string recipeName = Console.ReadLine();

            Recipe recipe = new Recipe(recipeName);

            Console.Write("Enter the number of ingredients:");
            if (!int.TryParse(Console.ReadLine(), out int numIngredients) || numIngredients < 1)
            {
                InvalidInputErrorMessage();
                return;
            }

            for (int i = 0; i < numIngredients; i++)
            {

                Console.Write($"Enter the name of ingredient {i + 1}: ");
                string ingredientName = Console.ReadLine();

                Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out double quantity) || quantity < 0)
                {
                    InvalidInputErrorMessage();
                    i--;
                    continue;
                }

                Console.Write($"Enter the unit of measurement for ingredient {i + 1}: ");
                string unit = Console.ReadLine();

                Console.Write($"Enter the number of calories for ingredient {i + 1}: ");
                if (!int.TryParse(Console.ReadLine(), out int calories) || calories < 0)
                {
                    Console.WriteLine($"Invalid input for the number of calories of ingredient {i + 1}. Please enter a positive integer.");
                    i--;
                    continue;
                }

                Console.Write($"Enter the food group for ingredient {i + 1}: ");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient(ingredientName, quantity, unit, calories, foodGroup);
                recipe.Ingredients.Add(ingredient);
            }

            Console.Write("Enter the number of steps: ");
            if (!int.TryParse(Console.ReadLine(), out int numSteps) || numSteps < 1)
            {
                InvalidInputErrorMessage();
                return;
            }

            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"Enter the description for step {i + 1}: ");
                string stepDescription = Console.ReadLine();
                recipe.Steps.Add(stepDescription);
            }
            recipes.Add(recipe);
            Console.WriteLine("Recipe added successfully.");
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method displays a list of all the recipes in the Recipe Book. It checks if any recipes are available and if so, it
        lists their names in alphabetical order. It prompts the user to select a recipe and calls the DisplayRecipe() method to
        show the details of the selected recipe.*/
        public void DisplayRecipesList()
        {
            if (recipes.Count == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("Recipes:");
            Console.WriteLine("--------");

            List<string> recipeNames = new List<string>();
            foreach (Recipe recipe in recipes)
            {
                recipeNames.Add(recipe.Name);
            }
            recipeNames.Sort();

            foreach (string recipeName in recipeNames)
            {
                Console.WriteLine(recipeName);
            }
            DisplayRecipe();
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method displays the details of a selected recipe. It prompts the user to enter the name of the recipe they want to
        view. It then retrieves the recipe from the Recipe Book and displays its name, ingredients with their quantities, and
        the preparation steps.
        */
        public void DisplayRecipe()
        {
            if (recipes.Count == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("Enter the name of the recipe you want to select:");
            string recipeName = Console.ReadLine();

            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine("\nRecipe Details:");
            Console.WriteLine("---------------");
            Console.WriteLine("Recipe Name: " + recipe.Name);
            Console.WriteLine("Ingredients:");
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                Ingredient ingredient = recipe.Ingredients[i];
                Console.WriteLine($"{i + 1}. {ingredient.Name} - {ingredient.Quantity} {ingredient.Unit}");
            }
            Console.WriteLine("Steps:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Steps[i]}");
            }
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method allows the user to scale the portion size of a recipe. It prompts the user to enter the name of the recipe
        they want to scale and the scaling factor. It multiplies the quantities of all the ingredients in the recipe by the
        scaling factor, updating their values.
        */
        public void ScaleRecipe()
        {
            if (recipes.Count == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("Enter the name of the recipe you want to scale:");
            string recipeName = Console.ReadLine();

            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine($"Current scaling factor for {recipeName}: {recipe.ScaleFactor}");
            Console.Write("Enter the scaling factor (0.5, 2, 3, etc.): ");
            if (!double.TryParse(Console.ReadLine(), out double scaleFactor) || scaleFactor <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                return;
            }

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                ingredient.Quantity *= scaleFactor;
            }

            recipe.ScaleFactor = scaleFactor;

            Console.WriteLine($"Recipe '{recipe.Name}' scaled successfully.");
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Resets the quantities of ingredients in a recipe to their original values. It prompts the user to enter the name of the
        recipe they want to reset. Once the recipe is specified, the method retrieves the original ingredient quantities from
        the Recipe Book and restores them, ensuring that the recipe returns to its initial state.
        */
        public void ResetQuantities()
        {
            if (recipes.Count == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("Enter the name of the recipe you want to reset quantities for:");
            string recipeName = Console.ReadLine();

            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }

            Console.WriteLine($"Quantities for recipe '{recipe.Name}' have been reset to their original values.");
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        removes a recipe from the Recipe Book. It prompts the user to enter the name of the recipe they want to clear. Once the
        recipe name is provided, the method locates the corresponding recipe in the Recipe Book and removes it, effectively
        deleting the recipe from the collection.
        */
        public void ClearRecipe()
        {
            if (recipes.Count == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("Enter the name of the recipe you want to clear:");
            string recipeName = Console.ReadLine();

            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                NoRecipeFoundErrorMessage();
                return;
            }
            recipes.Remove(recipe);

            Console.WriteLine($"Recipe '{recipe.Name}' has been removed from the recipe list.");
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*
        |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Creates and displays a user interface.
        This method creates an interactive user interface for managing the Recipe Book. It displays a menu of options, allowing
        the user to choose various actions such as entering a new recipe, displaying existing recipes, scaling a recipe,
        resetting ingredient quantities, clearing a recipe, or exiting the program. Based on the user's selection, the method
        calls the corresponding methods to execute the chosen action.
        */
        public void UserInterface()
        {
            /*Obejct
             Instantiates the RecipeDetails class to access all of its contents*/
            RecipeDetails recipe = new RecipeDetails();

            /*Loop
             While Loop*/
            while (true)
            {
                Console.WriteLine("\n\n==========================================");
                Console.WriteLine("Recipe Book");
                Console.WriteLine("==========================================");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display recipes");
                Console.WriteLine("3. Scale recipes");
                Console.WriteLine("4. Reset quantities");
                Console.WriteLine("5. Clear a recipe");
                Console.WriteLine("6. Calculate recipe calories");
                Console.WriteLine("7. Exit");
                Console.WriteLine("==========================================\n");

                string choiceString = Console.ReadLine();

                if (int.TryParse(choiceString, out int choice))
                {
                    /*Switch Case
                     Runs the method selected by the user*/
                    switch (choice)
                    {
                        case 1:
                            recipe.EnterRecipe();
                            break;

                        case 2:
                            recipe.DisplayRecipesList();
                            break;

                        case 3:
                            recipe.ScaleRecipe();
                            break;

                        case 4:
                            recipe.ResetQuantities();
                            break;

                        case 5:
                            recipe.ClearRecipe();
                            break;

                        case 6:
                            CalculateTotalCalories();
                            return;

                        case 7:
                            Console.WriteLine("Goodbye!");
                            return;
                        default:
                            recipe.InvalidInputErrorMessage();
                            break;
                    }
                }
                else
                {
                    recipe.InvalidInputErrorMessage();
                }
            }
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        It informs the user that there are no recipes available and prompts them to continue by pressing any key, possibly
        indicating that they need to enter a recipe before performing certain actions.
        */
        public void NoRecipeFoundErrorMessage()
        {
            Console.WriteLine("\n\n==========================================");
            Console.WriteLine("!!! E R R O R !!!");
            Console.WriteLine("No recipe found. Please enter a recipe and try again.");
            Console.WriteLine("==========================================");
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadKey();
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Displays an error message when the user provides invalid input or selects an invalid option.
        */
        public void InvalidInputErrorMessage()
        {
            Console.WriteLine("\n\n==========================================");
            Console.WriteLine("!!! E R R O R !!!");
            Console.WriteLine("Invalid choice. Please enter a number.");
            Console.WriteLine("==========================================");
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadKey();
        }

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method calculates the total calorie count of a recipe. It prompts the user to enter the name of the recipe they want
        to assess. Once the recipe name is provided, the method retrieves the corresponding recipe from the Recipe Book, sums up
        the calories of all the ingredients in the recipe, and displays the total calorie count. Additionally, it checks if the
        total calories exceed 300 and may perform further actions based on this evaluation.
        */
        public void CalculateTotalCalories()
        {
            if (recipes.Count == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("Enter the name of the recipe:");
            string recipeName = Console.ReadLine();

            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            int totalCalories = 0;
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                totalCalories += ingredient.Calories;
            }

            Console.WriteLine($"Total calories of {recipeName}: {totalCalories}");
            if (totalCalories > 300)
            {
                Console.WriteLine("This recipe exceeds 300 calories.");
            }
        }
    }
    #endregion
}