using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HighestScoreUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _visuals;
    [SerializeField] private List<ParticleSystem> _particleSystems;
    [SerializeField] private TMP_Text _highestScoreText;
    
    public void TryActivate()
    {
        _highestScoreText.text = SaveManager.Instance.GetLastHighScore().ToString();
        gameObject.SetActive(true);
        foreach (var particleSystem in _particleSystems)
        {
            particleSystem.Play();
        }

        foreach (var visual in _visuals)
        {
            visual.transform.localScale = Vector3.zero;
            visual.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
    }

    public void TryDeactivate()
    {
        gameObject.SetActive(false);
    }
}