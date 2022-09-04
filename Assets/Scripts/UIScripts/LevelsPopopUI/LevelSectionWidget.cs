using TMPro;
using UnityEngine;

public class LevelSectionWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private TMP_Text _levelHighScore;
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

    public void SetLevelNumber(int levelNumber)
    {
        _levelNumber = levelNumber;
        _levelNumberText.text = levelNumber.ToString();
    }

    public void SetLevelHighText(string text)
    {
        _levelHighScore.text = text;
    }
    
}