using System;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class UIButton : MonoBehaviour {

    [SerializeField] private Transform _buttonTransform;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private Vector3 _defaultScale;
    
    public Sprite regular;
    public Sprite mouseOver;
    public Sprite mouseClicked;
    public TextMeshPro buttonText;
    public Action OnButtonClicked;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

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
                gameObject.SetActive(false);
            });
        Debug.Log("OnMouseUpAsButton");
       
    }
}