using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TilesSelectHandle : MonoBehaviour
{
    private List<TileBehaviour> _tilesSelected = new List<TileBehaviour>();
    public static UnityEvent<TileBehaviour> TileSelected = new UnityEvent<TileBehaviour>();
    [SerializeField] private LineDrawer _lineDrawer;
    [SerializeField] private TileCreator _tileCreator;
    [SerializeField] private ScoreKeeper _scoreKeeper;
    [SerializeField] private RecipeCreator _recipeCreator;
    [SerializeField] private InGameMenuActions _gameMenuActions;
    [SerializeField] private MovesCounter _movesCounter;

    private void Start()
    {
        TileSelected.AddListener(OnTileSelected);
    }

    private void Update()
    {
        if (_gameMenuActions.Won || _gameMenuActions.Lost) return;

        if (Input.GetMouseButtonUp(0))
        {
            if (_tilesSelected.Count < 2)
            {
                // ������������ �����
                _lineDrawer.Clear();

                foreach (var tile in _tilesSelected)
                {
                    tile.Appear();
                }
                _tilesSelected.Clear();
                return;
            }
            else if (_tilesSelected.Count > 1)
            {
                // �������� tileArray � ��������� ����� �����
                List<Vector2Int> destroyedTiles = new List<Vector2Int>();
                foreach (var tile in _tilesSelected)
                {
                    destroyedTiles.Add(new Vector2Int(tile.ArrayPosition.x, tile.ArrayPosition.y));
                }
                _tileCreator.GetDestroyedTiles(destroyedTiles);

                foreach (var t in _tilesSelected)
                {
                    Destroy(t.gameObject);
                }

                // ���� ������
                _scoreKeeper.AddScore(10 * _tilesSelected.Count);

                // ���� �� �������
                _recipeCreator.DamageRecipes(_tilesSelected[0].Color, (int)Mathf.Pow(2, _tilesSelected.Count));

                // ���-�� �����
                if (LevelInfo.LevelType == LevelType.Master)
                {
                    _movesCounter.MakeMove();
                }

                // ������� ����
                _tilesSelected.Clear();

                // ������������ �����
                _lineDrawer.Clear();
            }
        }
    }

    private void OnTileSelected(TileBehaviour tile)
    {
        if (_gameMenuActions.Won || _gameMenuActions.Lost) return;

        if (IsAlreadySelected(tile)) 
        {
            // ������� ��� ����������� �� �����
            int index = _tilesSelected.IndexOf(tile) + 1;

            for (int i = index; i < _tilesSelected.Count; i++)
            {
                // ��������� ����
                _tilesSelected[i].Appear();
            }

            _tilesSelected.RemoveRange(index, _tilesSelected.Count - index);

            // ��������� �����
            DrawLine();
        }
        else
        {
            if (CanTileBeSelected(tile))
            {
                _tilesSelected.Add(tile);

                // ������������ ���� 
                tile.Disappear();

                // ���������� �����
                DrawLine();
            }
        }
        
    }

    private void DrawLine()
    {
        List<Vector3> points = new List<Vector3>();
        foreach (var t in _tilesSelected)
        {
            points.Add(t.transform.position);
        }
        _lineDrawer.GetAllPoints(points.ToArray());
    }

    private bool IsAlreadySelected(TileBehaviour tile)
    {
        return _tilesSelected.Contains(tile);
    }

    private bool CanTileBeSelected(TileBehaviour tileBehaviour)
    {
        if (_tilesSelected.Count == 0) 
        {
            return true; 
        }
        else
        {
            TileBehaviour lastTile = _tilesSelected[_tilesSelected.Count - 1];
            float lastX = lastTile.ArrayPosition.x;
            float lastY = lastTile.ArrayPosition.y;
            float newX = tileBehaviour.ArrayPosition.x;
            float newY = tileBehaviour.ArrayPosition.y;

            if ((lastTile.Color == tileBehaviour.Color) 
                && ((lastX == newX && Mathf.Abs(lastY - newY) == 1) || (lastY == newY && Mathf.Abs(lastX - newX) == 1)))
            {
                return true;
            }
        }
        return false;
    }
}
