using System;
namespace RecipeBook;

/*-------
Class
-------
This class represents an ingredient used in a recipe. It contains properties such as name, quantity, unit of measurement,
calories, food group, and original quantity. It is used to store and manage information about each ingredient in a recipe.
*/
public class Ingredient
{
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public int Calories { get; set; }
    public string FoodGroup { get; set; }
    public double OriginalQuantity { get; set; }

    public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
        Calories = calories;
        FoodGroup = foodGroup;
        OriginalQuantity = quantity;
    }
}
