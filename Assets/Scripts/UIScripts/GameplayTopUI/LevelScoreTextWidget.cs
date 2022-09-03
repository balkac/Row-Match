public class LevelScoreTextWidget : TextWidget
{
    protected override void AwakeCustomActions()
    {
        base.AwakeCustomActions();
        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
        SetTargetText(ScoreManager.Instance.LevelScore.ToString());
    }

    protected override void OnDestroyCustomActions()
    {
        base.OnDestroyCustomActions();
        ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        SetTargetText(score.ToString());
    }
}