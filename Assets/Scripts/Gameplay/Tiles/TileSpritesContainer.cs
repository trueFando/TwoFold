using System.Collections;
using UnityEngine;

public class TileSpritesContainer : MonoBehaviour
{
    // нужно соблюсти порядок спрайтов, как в enum TilesColor
    [SerializeField] private Sprite[] _sprites;

    public Sprite GetSprite(TileColor color)
    {
        if ((int)color < _sprites.Length && (int)color >= 0)
        {
            return _sprites[(int)color];
        }

        Debug.Log("Invalid color was sent to TileSpritesContainer");
        return null;
    }
}