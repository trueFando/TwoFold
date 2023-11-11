 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuActions : MonoBehaviour
{
    [SerializeField] private ScoreKeeper _scoreKeeper;

    [SerializeField] private Text _levelNumberText;
    [SerializeField] private GameObject _winImage;
    [SerializeField] private GameObject _wind;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _movesCounter;

    [Header("Lose")]
    [SerializeField] private Image _backImage;
    [SerializeField] private Sprite _loseSpriteBack;
    [SerializeField] private GameObject _loseImage; // надпись LOSE
    [SerializeField] private GameObject _againButton;
    [SerializeField] private GameObject _mainButton;



    private bool _won = false, _lost = false;
    public bool Won => _won;
    public bool Lost => _lost;

    private void Start()
    {
        _levelNumberText.text = LevelInfo.LevelNumber.ToString();
        if (LevelInfo.LevelType != LevelType.Master)
        {
            _movesCounter.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void WinGame()
    {
        PlayerPrefs.SetInt("game_started", 1); // не первый запуск игры

        _wind.SetActive(false);
        _winImage.SetActive(true);
        _nextButton.SetActive(true);
        _won = true;
        
        if (LevelInfo.LevelType == LevelType.Intro)
        {
            int lastLevel = PlayerPrefs.GetInt("level_intro");
            if (lastLevel != 39)
            {
                PlayerPrefs.SetInt("level_intro", lastLevel + 1);
            }
        }
        else if (LevelInfo.LevelType == LevelType.Standard)
        {
            int lastLevel = PlayerPrefs.GetInt("level_std");
            if (lastLevel != 19)
            {
                PlayerPrefs.SetInt("level_std", lastLevel + 1);
            }
            PlayerPrefs.SetInt("std_score", PlayerPrefs.GetInt("std_score") + _scoreKeeper.Score);
        }
        else
        {
            int lastLevel = PlayerPrefs.GetInt("level_master");
            if (lastLevel != 9)
            {
                PlayerPrefs.SetInt("level_master", lastLevel + 1);
            }
            PlayerPrefs.SetInt("master_score", PlayerPrefs.GetInt("std_score") + _scoreKeeper.Score);
        }
        
    }

    public void OnNextClick()
    {
        if (LevelInfo.LevelType == LevelType.Intro)
        {
            Debug.Log("Go");
            int lastLevel = PlayerPrefs.GetInt("level_intro");
            if (lastLevel == 39)
            { 
                SceneManager.LoadScene("Menu");
            }
            else
            {
                if (lastLevel > 0 && lastLevel < 10)
                {
                    LevelSelector.PrepareNewLevel(LevelInfo.LevelType, lastLevel, new Vector2Int(2, 4), new Vector2Int(2, 4), new Vector2Int(1, 2));
                }
                else if (lastLevel >= 10 && lastLevel < 30)
                {
                    LevelSelector.PrepareNewLevel(LevelInfo.LevelType, lastLevel, new Vector2Int(3, 5), new Vector2Int(3, 5), new Vector2Int(2, 4));
                }
                else if (lastLevel < 40)
                {
                    LevelSelector.PrepareNewLevel(LevelInfo.LevelType, lastLevel, new Vector2Int(3, 6), new Vector2Int(3, 6), new Vector2Int(2, 5));
                }
                SceneManager.LoadScene("Game");
            }
        }
        else if (LevelInfo.LevelType == LevelType.Standard)
        {
            int lastLevel = PlayerPrefs.GetInt("level_std");
            if (lastLevel == 19)
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                if (lastLevel > 0 && lastLevel < 10)
                {
                    LevelSelector.PrepareNewLevel(LevelInfo.LevelType, lastLevel, new Vector2Int(3, 6), new Vector2Int(3, 6), new Vector2Int(3, 6));
                }
                else
                {
                    LevelSelector.PrepareNewLevel(LevelInfo.LevelType, lastLevel, new Vector2Int(4, 6), new Vector2Int(4, 6), new Vector2Int(4, 7));
                }
                SceneManager.LoadScene("Game");
            }
        }
        else
        {
            int lastLevel = PlayerPrefs.GetInt("level_master");
            if (lastLevel == 9)
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                LevelSelector.PrepareNewLevel(LevelInfo.LevelType, lastLevel, new Vector2Int(5, 6), new Vector2Int(5, 6), new Vector2Int(4, 7));
            }
            SceneManager.LoadScene("Game");
        }
    }

    public void OnAgainClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoseGame()
    {
        _lost = true;
        _backImage.sprite = _loseSpriteBack;
        _againButton.SetActive(true);
        _mainButton.SetActive(true);
        _loseImage.SetActive(true);
        _wind.SetActive(false);
    }
}
