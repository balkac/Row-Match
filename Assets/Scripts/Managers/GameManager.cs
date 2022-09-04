using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action<LevelData> OnGameStarted;
    private Grid _grid;
    private List<int> _remainingRows = new List<int>();
    public Action OnGameEnded;
    private int _currentLevel = 1;
    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
        _grid.OnRowMatched += OnRowMatched;
        _grid.OnGridInitialized += OnGridInitialized;
    }
    private void OnGridInitialized(LevelData levelData)
    {
        for (int i = 0; i < levelData.GridHeight; i++)
        {
            _remainingRows.Add(i);
        }
    }
    
    //Button OnClicked;
    private void Start()
    {
        // StartLevel(_currentLevel);
    }
    private void OnDestroy()
    {
        _grid.OnRowMatched -= OnRowMatched;
        _grid.OnGridInitialized -= OnGridInitialized;
    }
    private void OnRowMatched(EItem itemType, int row)
    {
        _remainingRows.Remove(row);
        if (CheckGameEnd())
        {
            EndGame();
            Debug.Log("END GAME");
        }
    }
    private bool CheckGameEnd()
    {
        Dictionary<EItem, int> itemToCounts = new Dictionary<EItem, int>();

        for (int i = 0; i < _remainingRows.Count; i++)
        {
            var currentRow = _remainingRows[i];
            if (i != 0)
            {
                var previousRow = _remainingRows[i - 1];
                if (previousRow + 1 != currentRow)
                {
                    if (!CheckRows())
                    {
                        return false;
                    }
                    itemToCounts = new Dictionary<EItem, int>();
                }
            }
            FillDictionary(currentRow);
        }

        if (!CheckRows())
        {
            return false;
        }
        
        void FillDictionary(int row)
        {
            for (int j = 0; j < _grid.Width; j++)
            {
                if (!_grid.AllItems[j,row].IsMatch)
                {
                    if (itemToCounts.ContainsKey(_grid.AllItems[j,row].ItemType))
                    {
                        itemToCounts[_grid.AllItems[j,row].ItemType]++;
                    }
                    else
                    {
                        itemToCounts.Add(_grid.AllItems[j,row].ItemType,1);
                    }
                }
            }
        }
        
        bool CheckRows()
        {
            foreach (var itemToCount in itemToCounts)
            {
                if (itemToCount.Value >= _grid.Width)
                {
                    return false;
                }
            }
            return true;
        }
        
        return true;
    }
    private void StartLevel(int levelNumber)
    {
        LevelData levelData = LevelManager.Instance.GetLevelData(levelNumber);
       
        OnGameStarted?.Invoke(levelData);
        // Debug.Log("H---" + levelData.GridHeight);
        // Debug.Log("W---" + levelData.GridWidth);
        // Debug.Log("MOVECOUNT---" + levelData.MoveCount);
    }

    public void LoadMainScene()
    {
        
    }
    public void EndGame()
    {
        SaveManager.Instance.SaveCurrentLevel(_currentLevel);
        OnGameEnded?.Invoke();
    }
}