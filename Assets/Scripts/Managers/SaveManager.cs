using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private LevelContainer _levelContainer;
    
    private bool _requestResponse = true;

    private const string USER_LEVEL_SAVE = "UserLevelSave";
    private void Awake()
    {
        LoadSave();
    }

    private void OnDestroy()
    {
        if (_requestResponse)
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
}