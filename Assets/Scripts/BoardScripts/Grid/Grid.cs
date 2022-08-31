using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        _allTiles = new Tile[Width, Height];
        _allItems = new GameObject[Width, Height];
        Init();
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
                item.transform.parent = TileContainer;
                item.name = "( " + i + ", " + j + " )";
                _allItems[i, j] = item;
            }
        }

        TileContainer.position = transform.position;
    }
}
