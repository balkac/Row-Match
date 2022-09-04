using UnityEngine;

public class GamePlayTopVM : MonoBehaviour
{
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
        gameObject.SetActive(true);       
    }
    
    
}