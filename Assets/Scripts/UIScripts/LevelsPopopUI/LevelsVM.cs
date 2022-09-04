using UnityEngine;

public class LevelsVM : MonoBehaviour
{
    [SerializeField] private UIButton _levelsButton;
    [SerializeField] private UIButton _xButton;
    [SerializeField] private GameObject _mainVm;
    private void Awake()
    {
        gameObject.SetActive(false);
        _levelsButton.OnButtonClicked += OnButtonClicked;
        GameManager.Instance.OnGameStarted += OnGameStarted;
        _xButton.OnButtonClicked += OnXButtonClicked;
    }

    private void OnGameStarted(LevelData obj)
    {
        gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        _levelsButton.OnButtonClicked -= OnButtonClicked;
        _xButton.OnButtonClicked -= OnXButtonClicked;
        GameManager.Instance.OnGameStarted += OnGameStarted;
    }

    private void OnButtonClicked()
    {
        gameObject.SetActive(true);
    }
    
    private void OnXButtonClicked()
    {
        gameObject.SetActive(false);
        _mainVm.SetActive(true);
    }
}