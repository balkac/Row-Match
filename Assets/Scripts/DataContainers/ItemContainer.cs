using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemData
{
    public EItem Type;
    public GameObject ItemPrefab;
    public int Point;
}

[CreateAssetMenu(fileName = "ItemContainer", menuName = "Gameplay/ItemContainerSO", order = 1)]
public class ItemContainer : ScriptableObject
{
    [SerializeField] private List<ItemData> _itemDatas;

    public int GetItemPoint(EItem itemType)
    {
        foreach (var itemData in _itemDatas)
        {
            if (itemData.Type == itemType)
            {
                return itemData.Point;
            }
        }

        return 0;
    }

    public GameObject GetItemPrefab(EItem itemType)
    {
        foreach (var itemData in _itemDatas)
        {
            if (itemData.Type == itemType)
            {
                return itemData.ItemPrefab;
            }
        }

        return null;
    }
}