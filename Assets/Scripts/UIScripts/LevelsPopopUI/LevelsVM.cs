using System.Collections;
using UnityEngine;

public class LevelsVM : MonoBehaviour
{
    [SerializeField] private UIButton _levelsButton;
    [SerializeField] private UIButton _xButton;
    [SerializeField] private GameObject _menuVisuals;
    [SerializeField] private GameObject _mainVm;
    [SerializeField] private HighestScoreUI _highestScoreUI;
    public float HighestScoreDelay;
    private void Awake()
    {
        _levelsButton.OnButtonClicked += OnButtonClicked;
        SaveManager.Instance.OnSaveLoaded += OnSaveLoaded;
        GameManager.Instance.OnGameStarted += OnGameStarted;
        _xButton.OnButtonClicked += OnXButtonClicked;
    }

    private void OnSaveLoaded(bool isHighScore)
    {
        if (isHighScore)
        {
            StartCoroutine(VmDelayRoutine(HighestScoreDelay));
            _highestScoreUI.TryActivate();
            _menuVisuals.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            _highestScoreUI.gameObject.SetActive(false);
        }
    }

    private IEnumerator VmDelayRoutine(float highestScoreDelay)
    {
        yield return new WaitForSeconds(highestScoreDelay);
        _highestScoreUI.gameObject.SetActive(false);
        _menuVisuals.SetActive(true);
    }
    private void OnGameStarted(LevelData obj)
    {
        gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        _levelsButton.OnButtonClicked -= OnButtonClicked;
        _xButton.OnButtonClicked -= OnXButtonClicked;
        SaveManager.Instance.OnSaveLoaded -= OnSaveLoaded;
        GameManager.Instance.OnGameStarted -= OnGameStarted;
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