using UnityEngine;

public class MainSceneVM : MonoBehaviour
{
    [SerializeField] private UIButton _levelsButton;

    private void Awake()
    {
        SaveManager.Instance.OnSaveLoaded += OnSaveLoaded;
        _levelsButton.OnButtonClicked += OnButtonClicked;
    }

    private void OnSaveLoaded(bool isHighScore)
    {
        if (isHighScore)
        {
            Debug.Log("HIGH SCORE");
            gameObject.SetActive(false);
        }

        if (SaveManager.Instance.GetNumberOfUserPlay() > 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnApplicationQuit()
    {
        SaveManager.Instance.OnSaveLoaded -= OnSaveLoaded;
        _levelsButton.OnButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        gameObject.SetActive(false);
    }
}