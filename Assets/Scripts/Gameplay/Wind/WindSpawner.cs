using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    [SerializeField] private WindView[] _winds;
    [SerializeField] private float _delay = 1f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            WindView wind = Instantiate(_winds[Random.Range(0, _winds.Length)], transform);
            yield return new WaitForSeconds(_delay);
        }
    }
}
