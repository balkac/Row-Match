﻿using UnityEngine;

public class MovesCountTextWidget : TextWidget
{
    [SerializeField] private BoardMovesCountController boardMovesCountController;

    protected override void AwakeCustomActions()
    {
        base.AwakeCustomActions();
        SetTargetText(boardMovesCountController.MovesCount.ToString());
        boardMovesCountController.OnMovesCountChanged += OnMovesCountChanged;
    }

    protected override void OnDestroyCustomActions()
    {
        base.OnDestroyCustomActions();
        boardMovesCountController.OnMovesCountChanged -= OnMovesCountChanged;
    }

    private void OnMovesCountChanged(int movesCount)
    {
        SetTargetText(movesCount.ToString());
    }
}