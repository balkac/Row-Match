using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private ItemContainer _itemContainer;

    private Grid _grid;

    private int _levelScore;
    
    public Action<int,EItem,int> OnScoreChanged;
    
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
    private void OnRowMatched(EItem itemType, int row)
    {
        int itemPoint = _itemContainer.GetItemPoint(itemType);
        _levelScore += _grid.Width * itemPoint;
        OnScoreChanged?.Invoke(_levelScore,itemType,row);
        Debug.Log("LEVEL SCORE : " + _levelScore);
    }
}