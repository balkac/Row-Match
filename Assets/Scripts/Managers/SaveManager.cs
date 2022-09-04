using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private LevelContainer _levelContainer;
    
    private bool _requestResponse = true;

    private const string USER_LEVEL_SAVE = "UserLevelSave";
    
    private const string LEVEL_HIGH_SCORE = "LevelHighScore";

    private const string LAST_PLAYABLE_LEVEL = "LastPlayableLevel";
    
    public List<LevelSection> LevelSections = new List<LevelSection>();
    
    public Action OnSaveLoaded;
    private void Awake()
    {
        LoadSave();
    }
    private void OnDestroy()
    {
        if (_requestResponse && !PlayerPrefs.HasKey(USER_LEVEL_SAVE) )
        {
            PlayerPrefs.SetInt(USER_LEVEL_SAVE, 1);
            PlayerPrefs.Save();
        }
    }
    private void LoadSave()
    {
        bool hasLevelSave = PlayerPrefs.HasKey(USER_LEVEL_SAVE);
        if (!hasLevelSave)
        {
            foreach (var levelContainerData in _levelContainer.LevelContainerDatas)
            {
                ReadText(levelContainerData.Url,levelContainerData.LevelNumber);
            }
        }

        bool hasLastPlayableLevel = PlayerPrefs.HasKey(LAST_PLAYABLE_LEVEL);
        if (hasLastPlayableLevel)
        {
            int lastLevel = PlayerPrefs.GetInt(LAST_PLAYABLE_LEVEL);
            for (int i = 1; i < lastLevel; i++)
            {
                LevelSections.Add(new LevelSection(i, PlayerPrefs.GetInt(LEVEL_HIGH_SCORE + i)));
            }
        }
        OnSaveLoaded?.Invoke();
    }
    private void ReadText(string url,int levelNumber)
    {
        StartCoroutine(GetText(url,levelNumber));
    }
    private IEnumerator GetText(string url, int levelNumber)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
        {
            yield return unityWebRequest.Send();
            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                Debug.Log(unityWebRequest.error);
                _requestResponse = false;
            }
            else
            {
                string fileName = "LEVEL" + levelNumber;
                string savePath = string.Format("{0}/{1}.txt", Application.persistentDataPath, fileName);        
                System.IO.File.WriteAllText(savePath, unityWebRequest.downloadHandler.text);
            }
        }
    }

    public void SaveCurrentLevel(int level)
    {
        string levelHigh = LEVEL_HIGH_SCORE + level;
        PlayerPrefs.SetInt(levelHigh,ScoreManager.Instance.LevelScore);

        if (PlayerPrefs.HasKey(LAST_PLAYABLE_LEVEL) &&
            level+1 > PlayerPrefs.GetInt(LAST_PLAYABLE_LEVEL))
        {
            PlayerPrefs.SetInt(LAST_PLAYABLE_LEVEL,level+1);
        }

        if (!PlayerPrefs.HasKey(LAST_PLAYABLE_LEVEL))
        {
            PlayerPrefs.SetInt(LAST_PLAYABLE_LEVEL,level+1);
        }
           
        PlayerPrefs.Save();
    }
}