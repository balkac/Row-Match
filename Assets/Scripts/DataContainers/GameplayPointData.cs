using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemPoint
{
    public EItem Type;
    public int Score;
}

[CreateAssetMenu(fileName = "GameplayScoreData", menuName = "Gameplay/GameplayScoreDataSO", order = 1)]
public class GameplayPointData : ScriptableObject
{
    [SerializeField] private List<ItemPoint> _itemTypeToScore;

    public int GetItemPoint(EItem itemType)
    {
        foreach (var itemTypeToScore in _itemTypeToScore)
        {
            if (itemTypeToScore.Type == itemType)
            {
                return itemTypeToScore.Score;
            }
        }

        return 0;
    }
}