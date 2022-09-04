using System;
using DG.Tweening;
using UnityEngine;

public class UIButton : MonoBehaviour {

    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private Vector3 _defaultScale;
    
    public Action OnButtonClicked;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        _buttonTransform.DOScale(_defaultScale*0.9f, _tweenDuration);
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
        _buttonTransform.DOScale(_defaultScale, _tweenDuration);
    }

    private void OnMouseUpAsButton()
    {
        _buttonTransform.DOScale(_defaultScale, _tweenDuration).OnComplete(
            () =>
            {
                OnButtonClicked?.Invoke();
            });
        Debug.Log("OnMouseUpAsButton");
       
    }
}