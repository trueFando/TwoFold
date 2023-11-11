using UnityEngine;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private GridCreator _gridCreator;
    [SerializeField] private TileCreator _tileCreator;
    [SerializeField] private RecipeCreator _recipeCreator;

    private void Start()
    {
        // получить данные уровня
        int rows = LevelInfo.Rows;
        int columns = LevelInfo.Columns;

        // затычка
        if (rows == 0 || columns == 0) return;

        TileColor[] colors = LevelInfo.Colors;
        bool swipeEnabled = LevelInfo.IsSwipeEnabled;
        Recipe[] recipes = LevelInfo.Recipes;

        // отрисовать сетку, поставить камеру
        _gridCreator.InstantiateGrid(rows, columns);

        // сгенерировать и отрисовать тайлы
        _tileCreator.InstantiateTiles(rows, columns, colors, swipeEnabled);

        // активировать? свайпы
        if (swipeEnabled)
        {

        }

        // отрисовать рецепты
        _recipeCreator.Create(recipes);
    }
}
