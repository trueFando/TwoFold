using System.Collections;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _disappearingSpeed;

    public TileColor Color;
    public Vector2Int ArrayPosition;

    private bool _selected = false;

    public void SetSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            _renderer.sprite = sprite;
        }
    }

    public void Disappear()
    {
        // исчезновение шара
        StopAllCoroutines();
        StartCoroutine(DisappearingRoutine());
    }

    public void Appear()
    {
        StopAllCoroutines();
        StartCoroutine(AppearingRoutine());
    }

    public void Move()
    {

    }

    // ловим нажатие на тайл и отдаем это хэндлеру
    private void OnMouseOver()
    {
        if (_selected) return;

        if (Input.GetMouseButton(0))
        {
            TilesSelectHandle.TileSelected.Invoke(this);
            _selected = true;
        }
    }

    private void OnMouseExit()
    {
        _selected = false;
    }

    private IEnumerator DisappearingRoutine()
    {
        Transform skin = _renderer.transform;
        skin.localScale = Vector3.one;
        do
        {
            skin.localScale -= _disappearingSpeed * Time.deltaTime * Vector3.one;
            yield return null;
        } while (skin.localScale.x > 0);
        skin.localScale = Vector3.zero;
    }

    private IEnumerator AppearingRoutine()
    {
        Transform skin = _renderer.transform;
        skin.localScale = Vector3.zero;
        do
        {
            skin.localScale += _disappearingSpeed * Time.deltaTime * Vector3.one;
            yield return null;
        } while (skin.localScale.x < 1);
        skin.localScale = Vector3.one;
    }
}