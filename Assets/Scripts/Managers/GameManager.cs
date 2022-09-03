using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action<LevelData> OnGameStarted;
    private void Start()
    {
        StartLevel(1);
    }

    public void StartLevel(int levelNumber)
    {
        LevelData levelData = LevelManager.Instance.GetLevelData(levelNumber);
        OnGameStarted?.Invoke(levelData);
        // Debug.Log("H---" + levelData.GridHeight);
        // Debug.Log("W---" + levelData.GridWidth);
        // Debug.Log("MOVECOUNT---" + levelData.MoveCount);
    }
}