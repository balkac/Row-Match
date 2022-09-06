﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private LevelContainer _levelContainer;
    
    private bool _requestResponse = true;

    private const string HAS_USER_LEVEL_DATAS = "UserLevelSave";
    
    private const string LEVEL_HIGH_SCORE = "LevelHighScore";

    private const string LAST_PLAYABLE_LEVEL = "LastPlayableLevel";
    
    private const string IS_HIGH_SCORE = "IsHighScore";

    private const string LAST_HIGH_SCORE = "LastHighScore";
    
    public List<LevelSection> LevelSections = new List<LevelSection>();
    
    public Action<bool> OnSaveLoaded;
    private void Start()
    {
        LoadSave();
    }
    private void OnDestroy()
    {
        if (_requestResponse && !PlayerPrefs.HasKey(HAS_USER_LEVEL_DATAS))
        {
            PlayerPrefs.SetInt(HAS_USER_LEVEL_DATAS, 1);
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey(IS_HIGH_SCORE);
        PlayerPrefs.DeleteKey(LAST_HIGH_SCORE);
    }

    private void LoadSave()
    {
        bool hasLevelSave = PlayerPrefs.HasKey(HAS_USER_LEVEL_DATAS);
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
            for (int i = 1; i < lastLevel+1; i++)
            {
                LevelSections.Add(new LevelSection(i, PlayerPrefs.GetInt(LEVEL_HIGH_SCORE + i)));
            }
        }
        else
        {
            PlayerPrefs.SetInt(LEVEL_HIGH_SCORE + 1, 0);
            LevelSections.Add(new LevelSection(1, PlayerPrefs.GetInt(LEVEL_HIGH_SCORE + 1)));
        }
        OnSaveLoaded?.Invoke(PlayerPrefs.HasKey(IS_HIGH_SCORE));
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

    public bool SaveCurrentLevel(int level)
    {
        bool isHighScore = false;
        string levelHigh = LEVEL_HIGH_SCORE + level;

        if (PlayerPrefs.HasKey(levelHigh))
        {
            if (ScoreManager.Instance.LevelScore > PlayerPrefs.GetInt(levelHigh))
            {
                PlayerPrefs.SetInt(levelHigh,ScoreManager.Instance.LevelScore);
                PlayerPrefs.SetInt(IS_HIGH_SCORE,1);
                PlayerPrefs.SetInt(LAST_HIGH_SCORE,ScoreManager.Instance.LevelScore);
                isHighScore = true;
            }
        }
        else
        {
            if (ScoreManager.Instance.LevelScore <= 0)
            {
                PlayerPrefs.DeleteKey(IS_HIGH_SCORE);
                return false;
            }
            PlayerPrefs.SetInt(levelHigh,ScoreManager.Instance.LevelScore);
            PlayerPrefs.SetInt(IS_HIGH_SCORE,1);
            PlayerPrefs.SetInt(LAST_HIGH_SCORE,ScoreManager.Instance.LevelScore);
            isHighScore = true;
        }
        

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
        return isHighScore;
    }

    public int GetLastHighScore()
    {
        return PlayerPrefs.GetInt(LAST_HIGH_SCORE);
    }

    public int GetHighestLevelScore(int levelHigh)
    {
        string levelHighScore = LEVEL_HIGH_SCORE + levelHigh;
        return PlayerPrefs.HasKey(levelHighScore) ? PlayerPrefs.GetInt(levelHighScore) : 0;
    }
}