using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSectionManager : MonoBehaviour
{
    [SerializeField] private GameObject _sectionWidgetPrefab;
    [SerializeField] private Transform _sectionParent;
    [SerializeField] private float _offset = 1.5f;
    private float _yValue;
    private List<LevelSection> _levelSections;
    private List<LevelSectionWidget> _levelSectionWidgets;
    public Action<int> OnSectionsAdded;
    private void Awake()
    {
        _levelSections = new List<LevelSection>();
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

    private void OnApplicationQuit()
    {
        SaveManager.Instance.OnSaveLoaded -= OnSaveLoaded;
    }

    private void OnSaveLoaded(bool isHighScore)
    {
        AddSections();
    }

    private void AddSections()
    {
        _levelSections = SaveManager.Instance.LevelSections;
        
        foreach (var levelContainerData in LevelManager.Instance.LevelContainer.LevelContainerDatas )
        {
            if (levelContainerData.OfflineFolderName == "" && 
                Application.internetReachability == NetworkReachability.NotReachable)
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
        
        if (_levelSections.Count == 0)
        {
            _levelSections.Add(new LevelSection(1,0));
        }
        
        foreach (var levelSection in _levelSections)
        {
            _levelSectionWidgets[levelSection.Level-1].TryActivate(levelSection.HighScore);
        }
        
        OnSectionsAdded?.Invoke(_levelSectionWidgets.Count);
    }
}