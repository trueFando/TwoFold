using UnityEngine;

public static class CoordinatesConverter
{
    public static Vector2 FromWorldToArray(int worldX, int worldY, int rowsTotal)
    {
        int row = rowsTotal - worldY; 
        int col = worldX; 
        return new Vector2(row, col);
    }

    public static Vector2 FromArrayToWorld(int row, int col, int rowsTotal)
    {
        float x = col; 
        float y = rowsTotal - row; 
        return new Vector2(x, y - 1f);
    }
}