using UnityEngine;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private GridCreator _gridCreator;
    [SerializeField] private TileCreator _tileCreator;
    [SerializeField] private RecipeCreator _recipeCreator;

    private void Start()
    {
        // �������� ������ ������
        int rows = LevelInfo.Rows;
        int columns = LevelInfo.Columns;

        // �������
        if (rows == 0 || columns == 0) return;

        TileColor[] colors = LevelInfo.Colors;
        bool swipeEnabled = LevelInfo.IsSwipeEnabled;
        Recipe[] recipes = LevelInfo.Recipes;

        // ���������� �����, ��������� ������
        _gridCreator.InstantiateGrid(rows, columns);

        // ������������� � ���������� �����
        _tileCreator.InstantiateTiles(rows, columns, colors, swipeEnabled);

        // ������������? ������
        if (swipeEnabled)
        {

        }

        // ���������� �������
        _recipeCreator.Create(recipes);
    }
}
