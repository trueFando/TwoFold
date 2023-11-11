using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesCounter : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private InGameMenuActions _actions;
    private int _movesLeft;

    private void Start()
    {
        _movesLeft = LevelInfo.MovesCount;
        _text.text = _movesLeft.ToString();
    }

    public void MakeMove()
    {
        _movesLeft--;
        _text.text = _movesLeft.ToString();

        if (_movesLeft <= 0 && !_actions.Won)
        {
            _actions.LoseGame();
        }
    }
}
