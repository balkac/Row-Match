using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelContainer _levelContainer;

    public LevelContainer LevelContainer => _levelContainer;

    public LevelData GetLevelData(int levelNumber)
    {
        LevelData levelData = new LevelData();
        string fileName = "LEVEL" + levelNumber;
        string savePath = string.Format("{0}/{1}.txt", Application.persistentDataPath, fileName);

        try
        {
            string text= System.IO.File.ReadAllText(savePath);
            ParseText(text,levelData);
        }
        catch (Exception e)
        {
            // Console.WriteLine(e);
            savePath = "Levels/" + _levelContainer.GetLevelContainerData(levelNumber).OfflineFolderName;
            var levelTxt = Resources.Load<TextAsset>(savePath);
            ParseText(levelTxt.text,levelData);
        }

        return levelData;
    }
    private void ParseText(string text,LevelData levelData)
    {
        string[] splitArray =  text.Split(char.Parse("\n"));
        
        string levelText = splitArray[0].Split(char.Parse(":"))[1];
        bool success = int.TryParse(levelText, out int levelNumber);
        if (success)
        {
            levelData.LevelNumber = levelNumber;
        }
        
        string widthText = splitArray[1].Split(char.Parse(":"))[1];
        success = int.TryParse(widthText, out int width);
        if (success)
        {
            levelData.GridWidth = width;
        }
        
        string heightText = splitArray[2].Split(char.Parse(":"))[1];
        success = int.TryParse(heightText, out int height);
        if (success)
        {
            levelData.GridHeight = height;
        }
        
        string moveCountText = splitArray[3].Split(char.Parse(":"))[1];
        success = int.TryParse(moveCountText, out int moveCount);
        if (success)
        {
            levelData.MoveCount = moveCount;
        }
        
        List<EItem> gridItems = new List<EItem>();
        string grid = splitArray[4].Split(char.Parse(":"))[1];
        string[] itemTypes = grid.Split(char.Parse(","));
        var values = Enum.GetValues(typeof(EItem));
        foreach (var itemType in itemTypes)
        {
            foreach (EItem value in values)
            {
                string valueText = value.ToString();
                if (valueText.Contains(itemType.Trim().ToUpper()))
                {
                    gridItems.Add(value);
                }
            }
        }

        levelData.GridItems = gridItems;
        // foreach (var eItem in gridItems)
        // {
        //     Debug.Log("ITEM --- " + eItem);
        // }
    }
}