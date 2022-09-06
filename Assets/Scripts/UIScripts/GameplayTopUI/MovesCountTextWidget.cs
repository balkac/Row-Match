using UnityEngine;

public class MovesCountTextWidget : TextWidget
{
    [SerializeField] private BoardMovesCountController boardMovesCountController;
    [SerializeField] private string _prefix;
    protected override void AwakeCustomActions()
    {
        base.AwakeCustomActions();
        SetTargetText(_prefix + boardMovesCountController.MovesCount.ToString());
        boardMovesCountController.OnMovesCountChanged += OnMovesCountChanged;
    }

    protected override void OnApplicationQuitActions()
    {
        base.OnApplicationQuitActions();
        boardMovesCountController.OnMovesCountChanged -= OnMovesCountChanged;
    }

    private void OnMovesCountChanged(int movesCount)
    {
        SetTargetText(_prefix + " " + movesCount);
    }
}