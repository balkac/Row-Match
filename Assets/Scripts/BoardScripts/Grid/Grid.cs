using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    public int Width;
    public int Height;
    // public List<GameObject> ItemPrefabs;
    public ItemContainer ItemContainer;
    public GameObject TilePrefab;
    public Transform TileContainer;
    private Tile[,] _allTiles;
    private Item[,] _allItems;
    public Tile[,] AllTiles => _allTiles;
    public Item[,] AllItems => _allItems;
    public Action<EItem,int> OnRowMatched;
    public Action<LevelData> OnGridInitialized;

    private void Awake()
    {
        GameManager.Instance.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted(LevelData levelData)
    {
        Init(levelData);
        SubscribeToEvents();
    }

    private void OnApplicationQuit()
    {
        UnsubscribeToEvents();
        GameManager.Instance.OnGameStarted -= OnGameStarted;
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

    private void OnItemMoved(Item item)
    {
        EItem itemType = item.GetComponent<Item>().ItemType;
        int row = item.GetComponent<Item>().Row;
        if(CheckRowMatch(itemType, row))
        {
            OnRowMatched?.Invoke(itemType,row);
            for (int i = 0; i < Width; i++)
            {
                _allItems[i, row].DisableItem();
            }
            Debug.Log(row+".ROW ESLESTI ---"+ "Item tipi ---" + itemType );
        }
    }
    

    private void Init(LevelData levelData = null)
    {
        Width = levelData.GridWidth;
        Height = levelData.GridHeight;
        
        _allTiles = new Tile[Width, Height];
        _allItems = new Item[Width, Height];
        int prefabIndex = 0;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector2 position = new Vector2(i, j);
                GameObject backGroundTile = 
                    Instantiate(TilePrefab, position, Quaternion.identity,TileContainer);
                backGroundTile.name = "( " + i + ", " + j + " )";
                
                GameObject itemToUse = ItemContainer.GetItemPrefab(levelData.GridItems[prefabIndex]);
                GameObject item = Instantiate(itemToUse,
                    position, Quaternion.identity,transform);
                item.transform.parent = backGroundTile.transform;
                item.name = item.GetComponent<Item>().ItemType + "";
                _allItems[i, j] = item.GetComponent<Item>();
                prefabIndex++;
            }
        }
        TileContainer.position = transform.position;
        OnGridInitialized?.Invoke(levelData);
    }

    
    private bool CheckRowMatch(EItem itemType, int row)
    {
        for (int i = 0; i < Width; i++)
        {
            if (_allItems[i, row].ItemType != itemType)
            {
                return false;
            }
        }
        
        return true;
    }
}
