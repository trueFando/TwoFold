using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level Setup", menuName ="Level Setup")]
public class LevelSetupSO : ScriptableObject
{
    public int Rows;
    public TileColor[] TileArray;
}
