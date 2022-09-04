using UnityEngine;

public class MainSceneVM : MonoBehaviour
{
    [SerializeField] private UIButton _levelsButton;

    private void Awake()
    {
        _levelsButton.OnButtonClicked += OnButtonClicked;
    }

    private void OnDestroy()
    {
        _levelsButton.OnButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        gameObject.SetActive(false);
    }
}