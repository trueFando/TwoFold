using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeView : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private Sprite _spriteWin;
    
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;
    [SerializeField] private Animator _animator;

    public TileColor TileColor { get; set; }

    private int _points;

    public int Points => _points;

    public void Setup(TileColor color, int points)
    {
        TileColor = color;
        SetPoints(points);
        SetSprite(color);
    }

    public void GetDamage(int points)
    {
        _points -= points;
        if (_points <= 0)
        {
            _points = 0;
            SetSpriteWin();
        }
        
        SetPoints(_points);
        _animator.SetTrigger("Hit"); // нужно еще создать эту анимацию
    }

    private void SetPoints(int points)
    {
        _points = points;
        _text.text = _points.ToString();
    }

    private void SetSprite(TileColor color)
    {
        _image.color = _colors[(int)color];
    }

    private void SetSpriteWin()
    {
        _image.sprite = _spriteWin;
    }
}
