using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private int _score;
    public int Score => _score;

    [SerializeField] private Animator _animator;
    [SerializeField] private Text _textScore;
    [SerializeField] private Text _textPlus;

    public void AddScore(int score)
    {
        _textPlus.text = "+" + score.ToString();
        _score += score;
        _animator.SetTrigger("Score");
        Invoke("ChangeText", .5f);
    }

    private void ChangeText()
    {
        _textScore.text = _score.ToString();
    }
}
