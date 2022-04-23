public interface IBoard
{
    ITile[,] Tiles { get;  }
    ITile StartingTile { get;  }
    ITile CurrentTile { get;  }
    int Width { get;  }
    int Height { get;  }

    void Add(ITile t);
    ITile GetTileAt(int x, int y);
    void Setup();
    void InitializeBorders();
    void InitializeStartingLocation();
    void InitializeInsideTiles();
    void Log();
    void SetNeighbors(ITile t);
    ITile NorthOf(ITile t);
    ITile EastOf(ITile t);
    ITile SouthOf(ITile t);
    ITile WestOf(ITile t);
    void ShiftCurrentTile(DirectionType dir);
}