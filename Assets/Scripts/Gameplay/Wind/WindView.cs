using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindView : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime, 0f, 0f);
        if (_camera.WorldToViewportPoint(transform.position).x < -1) Destroy(gameObject);
    }
}
