using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCreator : MonoBehaviour
{
    [SerializeField] private RecipeView _recipePrefab;
    [SerializeField] private RectTransform _recipesParent;
    private List<RecipeView> _recipeViews = new List<RecipeView>();
    [SerializeField] private InGameMenuActions _gameMenuActions;

    public void Create(Recipe[] recipes)
    {
        foreach (Recipe recipe in recipes)
        {
            RecipeView view = Instantiate(_recipePrefab, _recipesParent);
            view.Setup(recipe.Color, recipe.Points);
            _recipeViews.Add(view);
        }
    }

    public void DamageRecipes(TileColor color, int points)
    {
        foreach(RecipeView recipe in _recipeViews)
        {
            if (recipe.TileColor == color)
            {
                recipe.GetDamage(points);
                if (recipe.Points <= 0)
                {
                    _recipeViews.Remove(recipe);
                }
                if (_recipeViews.Count == 0)
                {
                    _gameMenuActions.WinGame();
                }
                return;
            }
        }
    }
}
