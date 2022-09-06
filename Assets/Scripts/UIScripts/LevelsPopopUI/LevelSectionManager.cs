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
    private void Awake()
    {
        _levelSections = new List<LevelSection>();
        _levelSectionWidgets = new List<LevelSectionWidget>();
        
        SaveManager.Instance.OnSaveLoaded += OnSaveLoaded;
        
        foreach (var levelContainerData in LevelManager.Instance.LevelContainer.LevelContainerDatas )
        {
            GameObject sectionGO = Instantiate(_sectionWidgetPrefab);
            sectionGO.transform.parent = _sectionParent;
            sectionGO.transform.localPosition = new Vector3(0, _yValue, 0);
            _yValue -= _offset;
            LevelSectionWidget levelSectionWidget = sectionGO.GetComponent<LevelSectionWidget>();
            levelSectionWidget.SetLevelNumberAndMoves(levelContainerData.LevelNumber);
            levelSectionWidget.TryDeactivate();
            _levelSectionWidgets.Add(levelSectionWidget);
        }
    }

    private void OnApplicationQuit()
    {
        SaveManager.Instance.OnSaveLoaded -= OnSaveLoaded;
    }

    private void OnSaveLoaded(bool isHighScore)
    {
        _levelSections = SaveManager.Instance.LevelSections;
        if (_levelSections.Count == 0)
        {
            _levelSections.Add(new LevelSection(1,0));
        }
        
        foreach (var levelSection in _levelSections)
        {
            _levelSectionWidgets[levelSection.Level-1].TryActivate(levelSection.HighScore);
        }
    }
}