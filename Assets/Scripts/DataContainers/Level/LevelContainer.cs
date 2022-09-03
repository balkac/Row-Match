using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LevelContainerData
{
    public int LevelNumber;
    public string Url;
    public string OfflineFolderName;
}


[CreateAssetMenu(fileName = "LevelContainer", menuName = "Levels/LevelContainerSO", order = 1)]
public class LevelContainer : ScriptableObject
{
    [SerializeField] private List<LevelContainerData> _levelContainerDatas;

    public List<LevelContainerData> LevelContainerDatas => _levelContainerDatas;

    public LevelContainerData GetLevelContainerData(int levelNumber)
    {
        for (int i = 0; i < _levelContainerDatas.Count; i++)
        {
            if (_levelContainerDatas[i].LevelNumber == levelNumber)
            {
                return _levelContainerDatas[i];
            }
        }
        return null;
    }
}