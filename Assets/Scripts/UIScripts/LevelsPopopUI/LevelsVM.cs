using System;
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
        _xButton.OnButtonClicked += OnXButtonClicked;
    }
    
    private void OnApplicationQuit()
    {
        _levelsButton.OnButtonClicked -= OnButtonClicked;
        _xButton.OnButtonClicked -= OnXButtonClicked;

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