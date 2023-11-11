using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;

    public void GetAllPoints(Vector3[] points)
    {
        Clear();
        _renderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector3(points[i].x, points[i].y, 0f);
        }

        _renderer.SetPositions(points);
    }

    public void Clear()
    {
        _renderer.positionCount = 0;
    }
}
