using TMPro;
using UnityEngine;

public class TextWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text _targetText;

    private void Awake()
    {
        AwakeCustomActions();
    }

    private void OnApplicationQuit()
    {
        OnApplicationQuitActions();
    }
    
    protected virtual void AwakeCustomActions()
    {
        
    }

    protected virtual void OnApplicationQuitActions()
    {
        
    }
    public TMP_Text TargetText
    {
        get => _targetText;
        set => _targetText = value;
    }

    protected void SetTargetText(string text)
    {
        _targetText.text = text;
    }
}