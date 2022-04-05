using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    private IBoard board;
    public GameObject[,] boardObjects;
    private TileSelector selector;
    public int width = 20;
    public int height = 20;
    public const int TILE_SIZE = 10;

    public GameObject prefab_Unknown;
    public GameObject prefab_Start;
    public GameObject prefab_Path;
    public GameObject prefab_Border;
    public GameObject prefab_Obstacle;
    public GameObject prefab_End;


    private void Awake()
    {
        board = new RectBoard(width, height);
        boardObjects = new GameObject[width + 2, height + 2];

        selector = gameObject.GetComponent<TileSelector>();

        if (board.tiles == null)
        {
            board.InitializeTiles();
            board.InitializeBorders();
        }
        if (board.startingTile == null)
        {
            board.InitializeStartingLocation();
        }
        //InstantiateBoard();
        InstantiateTile(board.startingTile);
    }

    public void InstantiateTile(ITile tile)
    {
        tile.visible = true;
        Instantiate(tile);

        if (tile.type == TileType.Border)
        {
            Instantiate(tile);
            return;
        }

        if (tile.type == TileType.Start)
        {
            selector.InstantiateSelector(gameObject.transform, new Vector3(tile.x * TILE_SIZE, tile.y * TILE_SIZE, 0));
        }

        if (tile.neighbors.north.visible == false)
        {
            Instantiate(new Tile(tile.x, tile.y + 1, TileType.Unknown));
            tile.neighbors.north.visible = true;
        }
        if (tile.neighbors.south.visible == false)
        {
            Instantiate(new Tile(tile.x, tile.y - 1, TileType.Unknown));
            tile.neighbors.south.visible = true;
        }
        if (tile.neighbors.east.visible == false)
        {
            Instantiate(new Tile(tile.x + 1, tile.y, TileType.Unknown));
            tile.neighbors.east.visible = true;
        }
        if (tile.neighbors.west.visible == false)
        {
            Instantiate(new Tile(tile.x - 1, tile.y, TileType.Unknown));
            tile.neighbors.west.visible = true;
        }
    }

    public void InstantiateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Instantiate(new Tile(x, y, board.tiles[x, y].type));
            }
        }
    }

    private void Instantiate(ITile t)
    {
        GameObject objCategory = GameObject.Find(t.type.ToString());
        if (objCategory == null)
        {
            objCategory = new GameObject(t.type.ToString());
            objCategory.transform.parent = gameObject.transform;
        }

        GameObject coordObj = new GameObject("[" + t.x + ", " + t.y + "]");
        coordObj.transform.parent = objCategory.transform;

        if (boardObjects[t.x, t.y] != null)
            Destroy(boardObjects[t.x, t.y].transform.parent.gameObject);

        boardObjects[t.x, t.y] = GameObject.Instantiate(GetPrefab(t.type), new Vector3(t.x * TILE_SIZE, t.y * TILE_SIZE, 0), Quaternion.identity, coordObj.transform);

        if (t.type != TileType.Unknown && t.type != TileType.Start && t.visible == false)
            boardObjects[t.x, t.y].SetActive(false);
    }

    private GameObject GetPrefab(TileType type)
    {
        GameObject prefab = prefab_Unknown;
        switch (type)
        {
            case (TileType.Border):
                prefab = prefab_Border;
                break;
            case (TileType.Start):
                prefab = prefab_Start;
                break;
            case (TileType.End):
                prefab = prefab_End;
                break;
            case (TileType.Path):
                prefab = prefab_Path;
                break;
            case (TileType.Obstacle):
                prefab = prefab_Obstacle;
                break;
            case (TileType.Unknown):
            default:
                break;
        }

        return prefab;
    }

    public void Research(DirectionType dir)
    {
        if (board.currentTile.type != TileType.Path && board.currentTile.type != TileType.Start)
            return;

        if (dir == DirectionType.Up)
        {
            if (board.NorthOf(board.currentTile).type != TileType.Unknown)
            {
                InstantiateTile(board.NorthOf(board.currentTile));
                return;
            }

            board.currentTile.neighbors.north = GenerateTileType(board.currentTile.x, board.currentTile.y + 1);
            board.Add(board.currentTile.neighbors.north);
            InstantiateTile(board.currentTile.neighbors.north);
        }
        else if (dir == DirectionType.Down)
        {
            if (board.SouthOf(board.currentTile).type != TileType.Unknown)
            {
                InstantiateTile(board.SouthOf(board.currentTile));
                return;
            }

            board.currentTile.neighbors.south = GenerateTileType(board.currentTile.x, board.currentTile.y - 1);
            board.Add(board.currentTile.neighbors.south);
            InstantiateTile(board.currentTile.neighbors.south);
        }
        else if (dir == DirectionType.Right)
        {
            if (board.EastOf(board.currentTile).type != TileType.Unknown)
            {
                InstantiateTile(board.EastOf(board.currentTile));
                return;
            }

            board.currentTile.neighbors.east = GenerateTileType(board.currentTile.x + 1, board.currentTile.y);
            board.Add(board.currentTile.neighbors.east);
            InstantiateTile(board.currentTile.neighbors.east);
        }
        else if (dir == DirectionType.Left)
        {
            if (board.WestOf(board.currentTile).type != TileType.Unknown)
            {
                InstantiateTile(board.WestOf(board.currentTile));
                return;
            }

            board.currentTile.neighbors.west = GenerateTileType(board.currentTile.x - 1, board.currentTile.y);
            board.Add(board.currentTile.neighbors.west);
            InstantiateTile(board.currentTile.neighbors.west);
        }
    }

    private Tile GenerateTileType(int x, int y)
    {
        Tile t = new Tile(x, y);
        int num = Random.Range(0, 100);
        if (num >= 20) // 80%
        {
            t.type = TileType.Path;
        }
        else if (num >= 5) // 15%
        {
            t.type = TileType.Obstacle;
        }
        else // 5%
        {
            t.type = TileType.End;
        }

        t.visible = true;
        return t;
    }

    public void Up()
    {
        board.ShiftCurrentTile(DirectionType.Up);
        ShiftSelector(DirectionType.Up);
    }

    public void Down()
    {
        board.ShiftCurrentTile(DirectionType.Down);
        ShiftSelector(DirectionType.Down);
    }

    public void Left()
    {
        board.ShiftCurrentTile(DirectionType.Left);
        ShiftSelector(DirectionType.Left);
    }

    public void Right()
    {
        board.ShiftCurrentTile(DirectionType.Right);
        ShiftSelector(DirectionType.Right);
    }

    void ShiftSelector(DirectionType dir)
    {
        selector.ShiftSelector(dir);
    }
}