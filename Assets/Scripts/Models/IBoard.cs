public interface IBoard
{
    ITile[,] tiles { get;  }
    ITile startingTile { get;  }
    ITile currentTile { get;  }
    int width { get;  }
    int height { get;  }

    void Add(ITile t);
    ITile GetTileAt(int x, int y);
    void InitializeBorders();
    void InitializeStartingLocation();
    void InitializeTiles();
    void Log();
    void SetNeighbors(ITile t);
    ITile NorthOf(ITile t);
    ITile EastOf(ITile t);
    ITile SouthOf(ITile t);
    ITile WestOf(ITile t);
    void ShiftCurrentTile(DirectionType dir);
}