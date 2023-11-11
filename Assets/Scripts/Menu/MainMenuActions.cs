using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuActions : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private LevelSelector[] _levelSelectors;
    [SerializeField] private GameObject _newGameButton;
    [SerializeField] private GameObject _faqPanel;

    [Header("Disappering objects")]
    [SerializeField] private Image _newBall;
    [SerializeField] private Image _resumeBall;
    [SerializeField] private Image _faqBall;
    [SerializeField] private Text _newText;
    [SerializeField] private Text _resumeText;
    [SerializeField] private Text _faqText;
    [SerializeField] private Text _mainText;
    [SerializeField] private Button _backButton;

    [SerializeField] private GameObject _levelButtons;
    [SerializeField] private Image _introBall;
    [SerializeField] private Image _stdBall;
    [SerializeField] private Image _masterBall;
    [SerializeField] private Text _introText;
    [SerializeField] private Text _stdText;
    [SerializeField] private Text _masterText;
    [SerializeField] private Image _medalsStd;
    [SerializeField] private Image _medalsMaster;
    [SerializeField] private Text _medalsTextStd;
    [SerializeField] private Text _medalsTextMaster;

    [Header("Animators")]
    [SerializeField] private Animator _newBallAnimator;
    [SerializeField] private Animator _resumeBallAnimator;
    [SerializeField] private Animator _upperPart;

    [Header("Level buttons")]
    [SerializeField] private Button _introButton;
    [SerializeField] private Button _stdButton;
    [SerializeField] private Button _masterButton;

    [Header("Positions for level tab")]
    [SerializeField] private RectTransform _upPosition;
    [SerializeField] private RectTransform _downPosition;

    [Header("FAQ elements")]
    [SerializeField] private TextMeshProUGUI[] _textsFAQ;
    [SerializeField] private Image[] _imagesFAQ;

    [Header("Scores")]
    [SerializeField] private Text _stdScore;
    [SerializeField] private Text _masterScore;

    private float _disappearingDelay = 0.01f;

    private bool _levelsShowed = false;
    private bool _faqShowed = false;

    private Graphic[] _levelChooseElements;

    private void Awake()
    {
        Application.targetFrameRate = 144;
    }

    private void Start()
    {
        CheckIfGameStarted();
        _levelChooseElements = new Graphic[] { _introBall, _stdBall, _masterBall, _introText, _stdText, _masterText,
                                        _medalsStd, _medalsMaster, _medalsTextStd, _medalsTextMaster};

        foreach (var g in _levelChooseElements)
        {
            g.color = new Color(g.color.r, g.color.g, g.color.b, 0);
        }

        _levelButtons.SetActive(false);
        _faqPanel.SetActive(false);

        _stdScore.text = PlayerPrefs.GetInt("std_score").ToString();
        _masterScore.text = PlayerPrefs.GetInt("master_score").ToString();
    }

    public void ShowLevelsNewGame()
    {
        if (_levelsShowed) return;
        _levelsShowed = true;
        
        if (!IsGameStarted())
        {
            _levelButtons.transform.position = _upPosition.position;
        }
        else
        {
            _levelButtons.transform.position = _downPosition.position;
        }

        StartCoroutine(GraphicDisappear(_resumeBall));
        StartCoroutine(GraphicDisappear(_faqBall));
        StartCoroutine(GraphicDisappear(_resumeText));
        StartCoroutine(GraphicDisappear(_faqText));
        StartCoroutine(GraphicDisappear(_mainText));

        StartCoroutine(GraphicAppear(_backButton.targetGraphic));
        _backButton.interactable = true;

        _upperPart.SetTrigger("Down");
        _resumeBallAnimator.SetTrigger("Stop");
        _newBallAnimator.SetTrigger("Stop");

        _levelButtons.SetActive(true);
        foreach (Graphic g in _levelChooseElements)
        {
            StartCoroutine(GraphicAppear(g));
        }

        _introButton.interactable = true;
        _stdButton.interactable = true;
        _masterButton.interactable = true;

        PlayerPrefs.SetInt("level_intro", 0);
        PlayerPrefs.SetInt("level_std", 0);
        PlayerPrefs.SetInt("level_master", 0);
    }

    public void ShowLevelsResume()
    {
        if (_levelsShowed) return;
        _levelsShowed = true;

        _levelButtons.transform.position = _upPosition.position;

        StartCoroutine(GraphicDisappear(_newBall));
        StartCoroutine(GraphicDisappear(_faqBall));
        StartCoroutine(GraphicDisappear(_newText));
        StartCoroutine(GraphicDisappear(_faqText));
        StartCoroutine(GraphicDisappear(_mainText));

        StartCoroutine(GraphicAppear(_backButton.targetGraphic));
        _backButton.interactable = true;

        _upperPart.SetTrigger("Down");
        _resumeBallAnimator.SetTrigger("Stop");
        _newBallAnimator.SetTrigger("Stop");

        _levelButtons.SetActive(true);
        foreach (Graphic g in _levelChooseElements)
        {
            StartCoroutine(GraphicAppear(g));
        }

        _introButton.interactable = true;
        _stdButton.interactable = true;
        _masterButton.interactable = true;
    }

    public void OpenMain()
    {
        _newGameButton.SetActive(true);
        _resumeButton.SetActive(true);
        CheckIfGameStarted();

        _levelsShowed = false;
        _faqShowed = false;

        // отобразить главные кнопки
        StartCoroutine(GraphicAppear(_resumeBall));
        StartCoroutine(GraphicAppear(_faqBall));
        StartCoroutine(GraphicAppear(_resumeText));
        StartCoroutine(GraphicAppear(_newBall));
        StartCoroutine(GraphicAppear(_newText));
        StartCoroutine(GraphicAppear(_faqText));
        StartCoroutine(GraphicAppear(_mainText));

        // убрать кнопку "назад"
        StartCoroutine(GraphicDisappear((_backButton.targetGraphic)));
        _backButton.interactable = false;

        // анимации пульса кнопок и движения хэдера
        _resumeBallAnimator.SetTrigger("Start");
        _newBallAnimator.SetTrigger("Start");
        _upperPart.SetTrigger("Up");

        StartCoroutine(DeactivateLevels());
        StartCoroutine(DeactivateFAQ());

        _introButton.interactable = false;
        _stdButton.interactable = false;
        _masterButton.interactable = false;
    }

    public void OpenFAQ()
    {
        if (_faqShowed) return;
        _faqShowed = true;

        StartCoroutine(GraphicAppear(_backButton.targetGraphic));
        _backButton.interactable = true;

        _upperPart.SetTrigger("Down");
        StartCoroutine(GraphicDisappear(_mainText));

        _faqPanel.SetActive(true);
        _resumeButton.SetActive(false);
        _newGameButton.SetActive(false);

        foreach (Graphic graphic in _imagesFAQ)
        {
            StartCoroutine(GraphicAppear(graphic));
        }

        foreach (Graphic graphic in _textsFAQ)
        {
            StartCoroutine(GraphicAppear(graphic));
        }
    }

    private void CheckIfGameStarted()
    {
        if (!IsGameStarted())
        {
            _resumeButton.SetActive(false);
        }
        else
        {
            _newBallAnimator.enabled = false;
        }
    }

    private IEnumerator DeactivateLevels()
    {
        foreach (Graphic g in _levelChooseElements)
        {
            if (_levelChooseElements[_levelChooseElements.Length - 1] == g)
            {
                yield return StartCoroutine(GraphicDisappear(g));
            }
            else
            {
                StartCoroutine(GraphicDisappear(g));
            }
        }

        _levelButtons.SetActive(false);
    }

    private IEnumerator DeactivateFAQ()
    {
        foreach (Graphic g in _imagesFAQ)
        {
            StartCoroutine(GraphicDisappear(g));
        }

        foreach (Graphic g in _textsFAQ)
        {
            if (_textsFAQ[_textsFAQ.Length - 1] == g)
            {
                yield return StartCoroutine(GraphicDisappear(g));
            }
            else
            {
                StartCoroutine(GraphicDisappear(g));
            }
        }

        _faqPanel.SetActive(false);
    }

    private bool IsGameStarted()
    {
        return PlayerPrefs.GetInt("game_started") != 0;
    }

    private IEnumerator GraphicDisappear(Graphic graphic)
    {
        do
        {
            float a = graphic.color.a;
            a -= 0.1f;
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, a);
            yield return new WaitForSeconds(_disappearingDelay);
        } while (graphic.color.a > 0);
    }

    private IEnumerator GraphicAppear(Graphic graphic)
    {
        do
        {
            float a = graphic.color.a;
            a += 0.1f;
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, a);
            yield return new WaitForSeconds(_disappearingDelay);
        } while (graphic.color.a < 1);
    }
}
