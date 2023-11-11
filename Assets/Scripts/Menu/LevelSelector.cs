using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private LevelType _levelType;

    [SerializeField] private Image _ball;
    [SerializeField] private Text _text;
    [SerializeField] private float _fadeDelay;

    [SerializeField] private Image _medalsImg;
    [SerializeField] private Text _medalsCountText;

    [SerializeField] private float _ballRotatingSpeed;
    [SerializeField] private float _ballMovementSpeed;

    private bool _animating = false;

    public void FadeText()
    {
        GetComponent<HorizontalLayoutGroup>().enabled = false;
        StartCoroutine(FadeTextRoutine());
        _animating = true;
    }

    private IEnumerator FadeTextRoutine()
    {
        char[] charArray = _text.text.ToCharArray();

        for (int i = 0; i < charArray.Length; i++)
        {
            charArray[i] = ' ';
            _text.text = new string(charArray);
            yield return new WaitForSeconds(_fadeDelay);
        }

        yield return new WaitForSeconds(0.25f);

        if (_medalsImg != null)
        {
            Destroy(_medalsImg);
            Destroy(_medalsCountText);
        }
    }

    private void Update()
    {
        if (_animating)
        {
            _ball.transform.Rotate(0, 0, _ballRotatingSpeed * Time.deltaTime);
            _ball.transform.position += new Vector3(_ballMovementSpeed * Time.deltaTime, 0f, 0f);
            if (_ball.transform.position.x > 750f)
            {
                SetLevelData();
                SceneManager.LoadScene("Game");
            }
        }
    }

    private void SetLevelData()
    {
        if (_levelType == LevelType.Intro)
        {
            LevelInfo.MasterLevel = false;

            int lastLevel = PlayerPrefs.GetInt("level_intro");

            if (lastLevel == 0)
            {
                Recipe[] recipes = new Recipe[] { new Recipe(TileColor.Blue, 16) };
                LevelInfo.SetupInfo(2, 2, new TileColor[] { TileColor.Blue }, false, recipes, lastLevel + 1, _levelType);
            }
            else if (lastLevel > 0 && lastLevel < 10)
            {
                PrepareNewLevel(_levelType, lastLevel, new Vector2Int(2, 4), new Vector2Int(2, 4), new Vector2Int(1, 2));
            }
            else if (lastLevel >= 10 && lastLevel < 30)
            {
                PrepareNewLevel(_levelType, lastLevel, new Vector2Int(3, 5), new Vector2Int(3, 5), new Vector2Int(2, 4));
            }
            else if (lastLevel < 40)
            {
                PrepareNewLevel(_levelType, lastLevel, new Vector2Int(3, 6), new Vector2Int(3, 6), new Vector2Int(2, 5));
            }
        }
        else if (_levelType == LevelType.Standard)
        {
            LevelInfo.MasterLevel = false;

            int lastLevel = PlayerPrefs.GetInt("level_std");

            if (lastLevel > 0 && lastLevel < 10)
            {
                PrepareNewLevel(_levelType, lastLevel, new Vector2Int(3, 6), new Vector2Int(3, 6), new Vector2Int(3, 6));
            }
            else 
            {
                PrepareNewLevel(_levelType, lastLevel, new Vector2Int(4, 6), new Vector2Int(4, 6), new Vector2Int(4, 7));
            }
        }
        else
        {
            LevelInfo.MasterLevel = true;
            LevelInfo.MovesCount = 30;

            int lastLevel = PlayerPrefs.GetInt("level_master");

            PrepareNewLevel(_levelType, lastLevel, new Vector2Int(4, 6), new Vector2Int(4, 6), new Vector2Int(4, 7));
        }
    }

    public static void PrepareNewLevel(LevelType type, int lastLevel, Vector2Int minMaxSize, Vector2Int minMaxColors, Vector2Int minMaxRecipes)
    {
        int rows = Random.Range(minMaxSize.x, minMaxSize.y);
        int cols = Random.Range(minMaxSize.x, minMaxSize.y);

        int colorsCount = Random.Range(minMaxColors.x, minMaxColors.y);
        
        TileColor[] colors = new TileColor[colorsCount];
        if (colorsCount == 5)
        {
            colors = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue, TileColor.Yellow, TileColor.White};
        }
        else
        {
            for (int i = 0; i < colorsCount; i++)
            {
                TileColor newColor;
                do
                {
                    newColor = (TileColor)Random.Range(0, 5);
                } while (colors.Contains(newColor));
                colors[i] = newColor;
            }
        }

        int recipesCount = Random.Range(minMaxRecipes.x, minMaxRecipes.y);
        Recipe[] recipes = new Recipe[recipesCount];
        for (int i = 0; i < recipesCount; i++)
        {
            Recipe newRecipe;
            do
            {
                newRecipe = new Recipe(colors[Random.Range(0, colorsCount)], (int)Mathf.Pow(2, Random.Range(4, 6)));
            } while (recipes.Contains(newRecipe));
            recipes[i] = newRecipe;
        }

        LevelInfo.SetupInfo(rows, cols, colors, false, recipes, lastLevel + 1, type);
    }
}

public enum LevelType
{
    Intro, Standard, Master
}