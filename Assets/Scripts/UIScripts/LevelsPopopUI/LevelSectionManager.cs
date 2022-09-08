using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSectionManager : MonoBehaviour
{
    [SerializeField] private GameObject _sectionWidgetPrefab;
    [SerializeField] private Transform _sectionParent;
    [SerializeField] private float _offset = 1.5f;
    private float _yValue;
    private List<LevelSection> _activeLevelSections;
    private List<LevelSectionWidget> _levelSectionWidgets;
    public Action<int> OnSectionsAdded;
    private void Awake()
    {
        _activeLevelSections = new List<LevelSection>();
        _levelSectionWidgets = new List<LevelSectionWidget>();

        if (!SaveManager.Instance.HasSavedLevelDatas())
        {
            SaveManager.Instance.OnReadCompleted += OnReadCompleted;
        }
        else
        {
            SaveManager.Instance.OnSaveLoaded += OnSaveLoaded;
        }
        
    }

    private void OnReadCompleted(bool isCompleted)
    {
        SaveManager.Instance.OnReadCompleted -= OnReadCompleted;
        AddSections();
    }
    
    private void OnSaveLoaded(bool isHighScore)
    {
        SaveManager.Instance.OnSaveLoaded -= OnSaveLoaded;
        AddSections();
    }
    
    private void AddSections()
    {
        _activeLevelSections = SaveManager.Instance.ActiveLevelSections;
        
        foreach (var levelContainerData in LevelManager.Instance.LevelContainer.LevelContainerDatas )
        {
            if (levelContainerData.OfflineFolderName == "" && 
                !SaveManager.Instance.HasSavedLevelDatas())
            {
                break;
            }
            
            GameObject sectionGO = Instantiate(_sectionWidgetPrefab);
            sectionGO.transform.parent = _sectionParent;
            sectionGO.transform.localPosition = new Vector3(0, _yValue, 0);
            _yValue -= _offset;
            LevelSectionWidget levelSectionWidget = sectionGO.GetComponent<LevelSectionWidget>();
            levelSectionWidget.SetLevelNumberAndMoves(levelContainerData.LevelNumber);
            levelSectionWidget.TryDeactivate();
            _levelSectionWidgets.Add(levelSectionWidget);
        }
        
        foreach (var levelSection in _activeLevelSections)
        {
            _levelSectionWidgets[levelSection.Level-1].TryActivate(levelSection.HighScore);
        }
        
        OnSectionsAdded?.Invoke(_levelSectionWidgets.Count);
    }
}