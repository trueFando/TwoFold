using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [Header("Spawning Cells")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private float _gapBetweenCells;
    [Header("Dimensions")]
    [SerializeField] private int _maxRowsCols;

    private int _rows, _columns;

    public void InstantiateGrid(int rows, int columns)
    {
        SetSize(rows, columns);
        Create();
        SetupCamera();
    }

    private void SetSize(int rows, int columns)
    {
        if (IsSizeValid(rows, columns))
        {
            _rows = rows;
            _columns = columns;
        }
        else
        {
            Debug.LogError("Too big/small grid");
        }
    }

    private void Create()
    {
        if (IsSizeValid(_rows, _columns))
        {
            for (int i = 0; i < _columns; i++) // for x
            {
                for (int j = 0; j < _rows; j++) // for y
                {
                    var cell = Instantiate(_cellPrefab, new Vector3(i * _gapBetweenCells, j * _gapBetweenCells, 0f), Quaternion.identity);
                    cell.name = i.ToString() + ";" + j.ToString();
                }
            }
        }
        else
        {
            Debug.LogError("Too big/small grid");
        }
    }

    private void SetupCamera()
    {
        Camera _camera = Camera.main;

        float cellWidth = _cellPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        cellWidth *= 1.2f;  // +20%, чтобы точно помещалось в экран все

        if (_rows < _columns)
        {
            _camera.orthographicSize = cellWidth * _columns * Screen.height / Screen.width / 2f;
            _camera.orthographicSize *= 1.5f; // подогнал
        }
        else
        {
            _camera.orthographicSize = cellWidth * _columns * Screen.height / Screen.width / 2f;
            _camera.orthographicSize *= 2.5f; // подогнал
        }
        

        _camera.transform.position =
            new Vector3(_gapBetweenCells * (_columns - 1) / 2f, _gapBetweenCells * (_rows - 1) / 2f, -1f);
    }

    private bool IsSizeValid(int rows, int columns)
    {
        return rows > 0 && columns > 0 && rows <= _maxRowsCols && columns <= _maxRowsCols;
    }
}
