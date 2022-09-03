using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    public int Width;
    public int Height;
    public List<GameObject> ItemPrefabs;
    public GameObject TilePrefab;
    public Transform TileContainer;
    private Tile[,] _allTiles;
    private GameObject[,] _allItems;

    public Tile[,] AllTiles => _allTiles;
    public GameObject[,] AllItems => _allItems;

    public Action<EItem,int> OnRowMatched;
    void Start()
    {
        _allTiles = new Tile[Width, Height];
        _allItems = new GameObject[Width, Height];
        Init();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _allItems[i, j].GetComponent<Item>().OnItemMoved += OnItemMoved;
            }
        }
    }

    private void OnItemMoved(Item item)
    {
        EItem itemType = item.GetComponent<Item>().ItemType;
        int row = item.GetComponent<Item>().Row;
        if(CheckRowMatch(itemType, row))
        {
            OnRowMatched?.Invoke(itemType,row);
            for (int i = 0; i < Width; i++)
            {
                _allItems[i, row].GetComponent<Item>().DisableItem();
            }
            Debug.Log(row+".ROW ESLESTI ---"+ "Item tipi ---" + itemType );
        }
    }

    private void OnApplicationQuit()
    {
        UnsubscribeToEvents();
    }

    private void UnsubscribeToEvents()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (_allItems[i, j] != null)
                {
                    _allItems[i, j].GetComponent<Item>().OnItemMoved -= OnItemMoved;
                }
            }
        }
    }

    private void Init()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector2 position = new Vector2(i, j);
                GameObject backGroundTile = 
                    Instantiate(TilePrefab, position, Quaternion.identity,TileContainer);
                backGroundTile.name = "( " + i + ", " + j + " )";
                
                
                int itemToUse = Random.Range(0, ItemPrefabs.Count);
                GameObject item = Instantiate(ItemPrefabs[itemToUse],
                    position, Quaternion.identity,transform);
                item.transform.parent = backGroundTile.transform;
                item.name = item.GetComponent<Item>().ItemType + "";
                _allItems[i, j] = item;
            }
        }

        TileContainer.position = transform.position;
    }

    
    private bool CheckRowMatch(EItem itemType, int row)
    {
        for (int i = 0; i < Width; i++)
        {
            if (_allItems[i, row].GetComponent<Item>().ItemType != itemType)
            {
                return false;
            }
        }
        
        return true;
    }
}
