public static class LevelInfo
{
    public static int Rows { get; set; }
    public static int Columns { get; set; }
    public static TileColor[] Colors { get; set; }
    public static bool IsSwipeEnabled { get; set; }
    public static bool IsManual { get; set; }
    public static LevelSetupSO LevelSetup { get; set; }
    public static Recipe[] Recipes { get; set; }
    public static int LevelNumber { get; set; }
    public static LevelType LevelType { get; set; }
    public static bool MasterLevel { get; set; } // проверять ли кол-во ходов
    public static int MovesCount { get; set; }

    public static void SetupInfo(int rows, int cols, TileColor[] colors, bool swipeEnabled, Recipe[] recipes, int levelNumber, LevelType type)
    {
        Rows = rows; 
        Columns = cols;
        Colors = colors;
        IsSwipeEnabled = swipeEnabled;
        Recipes = recipes;
        LevelNumber = levelNumber;
        LevelType = type;
    }
}   
    