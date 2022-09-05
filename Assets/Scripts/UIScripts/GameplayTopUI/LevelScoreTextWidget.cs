public class LevelScoreTextWidget : TextWidget
{
    protected override void AwakeCustomActions()
    {
        base.AwakeCustomActions();
        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
        SetTargetText(ScoreManager.Instance.LevelScore.ToString());
    }

    protected override void OnApplicationQuitActions()
    {
        base.OnApplicationQuitActions();
        ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score, EItem itemType, int row)
    {
        SetTargetText(score.ToString());
    }
}