using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private GameplayPointData gameplayPointData;

    private Grid _grid;

    private int _levelScore;
    
    public Action<int> OnScoreChanged;
    
    public int LevelScore => _levelScore;
    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
        _grid.OnRowMatched += OnRowMatched;
    }

    private void OnDestroy()
    {
        _grid.OnRowMatched -= OnRowMatched;
    }

    private void OnRowMatched(EItem ItemType, int row)
    {
        int itemPoint = gameplayPointData.GetItemPoint(ItemType);
        _levelScore += _grid.Width * itemPoint;
        OnScoreChanged?.Invoke(_levelScore);
        Debug.Log("LEVEL SCORE : " + _levelScore);
    }
}