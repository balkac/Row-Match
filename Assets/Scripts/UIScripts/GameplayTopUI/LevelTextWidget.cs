public class LevelTextWidget : TextWidget
{
    protected override void AwakeCustomActions()
    {
        base.AwakeCustomActions();
        GameManager.Instance.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted(LevelData levelData)
    {
        SetTargetText(levelData.LevelNumber.ToString());
    }

    protected override void OnDestroyCustomActions()
    {
        base.OnDestroyCustomActions();
        GameManager.Instance.OnGameStarted -= OnGameStarted;
    }
}