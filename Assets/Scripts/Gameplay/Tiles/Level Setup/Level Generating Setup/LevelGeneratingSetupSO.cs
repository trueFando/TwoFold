using UnityEngine;

[CreateAssetMenu(fileName ="New Level Generating Setup", menuName = "Level Generating Setup")]
public class LevelGeneratingSetupSO : ScriptableObject
{
    public int Number;
    public int Rows;
    public int Columns;
    public TileColor[] Colors;
    public bool SwipeEnabled;
}
