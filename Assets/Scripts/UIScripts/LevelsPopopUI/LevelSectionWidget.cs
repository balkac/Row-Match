using TMPro;
using UnityEngine;

public class LevelSectionWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text _playButtonText;
    [SerializeField] private TMP_Text _levelHighestScoreText;
    [SerializeField] private TMP_Text _levelMovesText;
    [SerializeField] private UIButton _levelButton;
    private int _levelNumber;
    private void Awake()
    {
        _levelButton.OnButtonClicked += OnButtonClicked;
    }

    private void OnDestroy()
    {
        _levelButton.OnButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        GameManager.Instance.StartLevel(_levelNumber);
    }

    public void SetLevelNumberAndMoves(int levelNumber)
    {
        _levelMovesText.text = "Level " + levelNumber + "- " +
                               LevelManager.Instance.GetLevelData(levelNumber).MoveCount +" Moves";
       
        _levelNumber = levelNumber;
    }
    
    public void TryActivate(int highScore)
    {
        _levelButton.EnabledButton(true);
        _playButtonText.text = "PLAY";

        if (highScore>0)
        {
            _levelHighestScoreText.text = "Highest Score: " + highScore;
        }
        else
        {
            _levelHighestScoreText.text = "No Score";
        }
    }

    public void TryDeactivate()
    {
        _levelButton.EnabledButton(false);
        _levelHighestScoreText.text = "Locked Level";
        _playButtonText.text = "LOCKED";
    }
}