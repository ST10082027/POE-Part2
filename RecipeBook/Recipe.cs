using System;
namespace RecipeBook
{
    /*-------
    Class
    -------
     This class represents a recipe. It contains properties for the recipe's name, a list of ingredients (instances of
     the Ingredient class), a list of steps, and a scale factor. It is used to store and organize information about a
     specific recipe, including its ingredients and preparation steps. The scale factor allows scaling the recipe's 
     portion size.
    */
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public double ScaleFactor = 1;

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();

        }
    }
}
