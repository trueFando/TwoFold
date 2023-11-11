using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileCreator : MonoBehaviour
{
    [SerializeField] private TileBehaviour _tilePrefab;
    [SerializeField] private TileSpritesContainer _spritesContainer;

    private int _rows, _columns;
    private TileColor[] _levelColors;

    // основной массив
    private TileColor[,] _tileArray;

    // список уничтоженных тайлов
    private List<Vector2Int> _destroyedTiles = new List<Vector2Int>();

    public void InstantiateTiles(int rows, int columns, TileColor[] colors, bool swipeEnabled)
    {
        _levelColors = colors;
        SetSize(rows, columns);

        Create(swipeEnabled);
    }

    private void Create(bool withSwipe)
    {
        GenerateSimpleArray();

        SpawnTiles();
    }

    private void SetSize(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Vector2 worldPosition = CoordinatesConverter.FromArrayToWorld(i, j, _rows);
                TileBehaviour newTile = Instantiate(_tilePrefab, worldPosition, Quaternion.identity);
                newTile.ArrayPosition = new Vector2Int(i, j);
                newTile.Color = _tileArray[i, j];
                Sprite newSprite = _spritesContainer.GetSprite(_tileArray[i, j]);
                newTile.SetSprite(newSprite);
                newTile.Appear();
            }
        }
    }

    private void SpawnTilesDuringGame()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                if (IsTileDestroyed(i, j))
                {
                    Vector2 worldPosition = CoordinatesConverter.FromArrayToWorld(i, j, _rows);
                    TileBehaviour newTile = Instantiate(_tilePrefab, worldPosition, Quaternion.identity);
                    newTile.ArrayPosition = new Vector2Int(i, j);
                    newTile.Color = _tileArray[i, j];
                    Sprite newSprite = _spritesContainer.GetSprite(_tileArray[i, j]);
                    newTile.SetSprite(newSprite);
                    newTile.Appear();
                }
            }
        }
    }

    private void GenerateSimpleArray() // introduction & medium
    {
        // предусмотреть наличие хотя бы 1-го мэтча при генерации
        do
        {
            GenerateRandomArray();
            if (!IsMatchDetected()) _tileArray = null; // вот это только в старте можно делать
        } while (!IsMatchDetected());
    }

    private void GenerateArrayDuringGame()
    {
        // предусмотреть наличие хотя бы 1-го мэтча при генерации
        do
        {
            GenerateRandomArray(); // вот это только в старте можно делать
        } while (!IsMatchDetected());

        SpawnTilesDuringGame();
    }

    private void GenerateMasterArray() // master
    {

    }

    private void GenerateRandomArray()
    {
        if (_tileArray == null) // начало уровня - сетки еще нет
        {
            if (_rows > 0 && _columns > 0)
            {
                _tileArray = new TileColor[_rows, _columns];

                int colorsLength = _levelColors.Length;

                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        _tileArray[i, j] = _levelColors[UnityEngine.Random.Range(0, colorsLength)];
                    }
                }
            }
            else
            {
                Debug.LogError("Tile Creator can't generate an array (too small size)");
                return;
            }
        }
        else // генерация в моменте, когда уже есть сетка - после мэтча
        {
            int colorsLength = _levelColors.Length;
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (IsTileDestroyed(i, j))
                    {
                        _tileArray[i, j] = _levelColors[UnityEngine.Random.Range(0, colorsLength)];
                    }
                }
            }
        }
    }

    public void GetDestroyedTiles(List<Vector2Int> points)
    {
        _destroyedTiles = points;
        GenerateArrayDuringGame();
    }

    private bool IsTileDestroyed(int x, int y)
    {
        foreach (var tile in _destroyedTiles)
        {
            if (tile.x == x && tile.y == y)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsMatchDetected()
    {
        if (_tileArray == null) return false;

        for (int i = 0; i < _rows - 1; i++)
        {
            for (int j = 0; j < _columns - 1; j++)
            {
                if (_tileArray[i, j] == _tileArray[i + 1, j] ||
                    _tileArray[i, j] == _tileArray[i, j + 1])
                {
                    return true;
                }
            }
        }

        return false;
    }
}