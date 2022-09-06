using UnityEngine;

public class GamePlayTopVM : MonoBehaviour
{
    [SerializeField] private HighestScoreTextWidget _highestScoreTextWidget;
    [SerializeField] private LevelTextWidget _levelTextWidget;
    private void Awake()
    {
        gameObject.SetActive(false);   
        GameManager.Instance.OnGameStarted += OnGameStarted;
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted(LevelData levelData)
    {
        _highestScoreTextWidget.SetTargetText(SaveManager.Instance.GetHighestLevelScore(levelData.LevelNumber).ToString());
        _levelTextWidget.SetTargetText(levelData.LevelNumber.ToString());
        gameObject.SetActive(true);       
    }
    
    
}