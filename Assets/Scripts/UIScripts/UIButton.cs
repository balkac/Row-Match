using System;
using DG.Tweening;
using UnityEngine;

public class UIButton : MonoBehaviour {

    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private Vector3 _defaultScale;
    
    public bool Enabled = true;
    public Action OnButtonClicked;
    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (!Enabled)
        {
            return;
        }
        Debug.Log("OnMouseDown");
        _buttonTransform.DOScale(_defaultScale*0.9f, _tweenDuration);
    }

    private void OnMouseEnter()
    {
        if (!Enabled)
        {
            return;
        }
        Debug.Log("OnMouseEnter");
    }

    private void OnMouseExit()
    {
        if (!Enabled)
        {
            return;
        }
        Debug.Log("OnMouseExit");
        _buttonTransform.DOScale(_defaultScale, _tweenDuration);
    }

    private void OnMouseUpAsButton()
    {
        if (!Enabled)
        {
            return;
        }
        _buttonTransform.DOScale(_defaultScale, _tweenDuration).OnComplete(
            () =>
            {
                OnButtonClicked?.Invoke();
            });
        Debug.Log("OnMouseUpAsButton");
       
    }

    public void EnabledButton(bool enabled)
    {
        Enabled = enabled;
    }
}