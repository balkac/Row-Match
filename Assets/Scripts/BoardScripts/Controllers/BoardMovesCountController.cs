using System;
using UnityEngine;

public class BoardMovesCountController : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private int _movesCount;

    public int MovesCount => _movesCount;

    public Action<int> OnMovesCountChanged;
    private void Awake()
    {
        _grid.OnGridInitialized += OnGridInitialized;
    }
    private void OnDestroy()
    {
        _grid.OnGridInitialized -= OnGridInitialized;
    }
    private void OnGridInitialized(LevelData levelData)
    {
        _movesCount = levelData.MoveCount;
        OnMovesCountChanged?.Invoke(_movesCount);
        for (int i = 0; i < _grid.Width; i++)
        {
            for (int j = 0; j < _grid.Height; j++)
            {
                if (_grid.AllItems[i, j] != null)
                {
                    _grid.AllItems[i, j].GetComponent<Item>().OnItemMoved += OnItemMoved;
                }
            }
        }
    }
    private void OnItemMoved(Item item)
    {
        _movesCount -= 1;
        if (MovesCount == 0)
        {
            GameManager.Instance.EndGame();
        }
        OnMovesCountChanged?.Invoke(_movesCount);
    }
}