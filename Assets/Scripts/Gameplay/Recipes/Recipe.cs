using System.Collections.Generic;

public class Recipe
{
    private TileColor _color;
    private int _points;

    public TileColor Color => _color;
    public int Points => _points;

    public Recipe(TileColor color, int points)
    {
        _color = color;
        _points = points;
    }
}