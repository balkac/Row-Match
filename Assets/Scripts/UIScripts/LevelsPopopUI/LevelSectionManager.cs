using System.Collections.Generic;
using UnityEngine;

public class LevelSectionManager : MonoBehaviour
{
    [SerializeField] private GameObject _sectionWidgetPrefab;

    [SerializeField] private float _offset = 1.5f;
    private float _yValue;
    private List<LevelSection> _levelSections = new List<LevelSection>();
    private List<LevelSectionWidget> _levelSectionWidgets = new List<LevelSectionWidget>();
    private void Awake()
    {
        SaveManager.Instance.OnSaveLoaded += OnSaveLoaded;
        
        foreach (var levelContainerData in LevelManager.Instance.LevelContainer.LevelContainerDatas )
        {
            GameObject sectionGO = Instantiate(_sectionWidgetPrefab);
            sectionGO.transform.parent = transform;
            sectionGO.transform.localPosition = new Vector3(0, _yValue, 0);
            _yValue -= _offset;
            LevelSectionWidget levelSectionWidget = sectionGO.GetComponent<LevelSectionWidget>();
            levelSectionWidget.SetLevelNumber(levelContainerData.LevelNumber);
            _levelSectionWidgets.Add(levelSectionWidget);
        }
    }

    private void OnDestroy()
    {
        SaveManager.Instance.OnSaveLoaded -= OnSaveLoaded;
    }

    private void OnSaveLoaded()
    {
        _levelSections = SaveManager.Instance.LevelSections;
        foreach (var levelSection in _levelSections)
        {
            _levelSectionWidgets[levelSection.Level-1].SetLevelHighText(levelSection.HighScore.ToString());
        }
    }
}