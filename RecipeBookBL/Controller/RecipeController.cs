﻿using RecipeBookBL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBookBL.Controller
{
    public class RecipeController : BaseController
    {
        public Recipe CurrentRecipe { get; private set; }

        private string Name;

        private string Description;

        private List<Product> RecipeProductList;

        private List<Recipe> Recipes;

        private CultureInfo Culture;

        public RecipeController(CultureInfo culture)
        {
            Culture = culture;
            Recipes = Load<Recipe>(Culture);
            RecipeProductList = new List<Product>();

        }


        public void AddToRecipe(string name, string description)
        {
            if (name != null)
            {
                Name = name;
            }
            if (description != null)
            {
                Description = description;
            }
        }

        public void AddToRecipe(Product product)
        {
            if (product != null)
            {
                RecipeProductList.Add(product);
            }
        }


        public void SaveRecipe()
        {
            if (Name != null && Description != null && RecipeProductList.Count > 0)
            {
                CurrentRecipe = new Recipe(Name, Description, RecipeProductList);
                Recipes.Add(CurrentRecipe);
                Save<Recipe>(Recipes, Culture);
            }
            else
            {
                throw new Exception("Can`t save recipe. Invalid arguments.", new SystemException(nameof(Recipe)));
            }        
        }


        public int[] FindByName(string name)
        {
            var indexes = new List<int>();

            for (int i = 0; i < Recipes.Count; i++)
            {
                if (Recipes[i].Name != null && Recipes[i].Name.ToLower().Contains(name.ToLower()))
                {
                    indexes.Add(i);
                }
            }

            return indexes.ToArray();
        }

        public int[] FindByProduct(string[] names)
        {
            var recipeindexes = new List<int>();    
            
            for (int i = 0; i < Recipes.Count; i++)
            {
                var sucsesffulCheckCount = 0;

                foreach (string name in names)
                {
                    if (Recipes[i].IsContainsProduct(name))
                    {
                        sucsesffulCheckCount++;
                    }
                }

                if (sucsesffulCheckCount == names.Length)
                {
                    recipeindexes.Add(i);                 
                }

            }

            return recipeindexes.ToArray();

        }

        public string GetRecipeByIndex(int index, bool shorted = true)
        {
            if (shorted)
            {
                return Recipes[index].Name;
            }
            else
            {
                return Recipes[index].ToString();
            }
            
        }




    }
}
