using UnityEngine;

public interface IBoard
{
	ITile[,] Tiles { get; set; }
	ITile StartingTile { get; set; }
	ITile CurrentTile { get; set; }

	void InstantiateTile(ITile tile, GameObject prefab);
	void InstantiateBorder(ITile tile);
	bool AddHidden(ITile t, GameObject prefab);
	bool Add(ITile t, GameObject prefab);
	bool AddWithoutNeighbors(ITile t, GameObject prefab);
	bool AddWithoutInstantiation(ITile t);
	ITile GetTileAt(int x, int y);
	void Log();
	void SetNeighbors();
	void SetNeighbors(ITile t);
	ITile NorthOf(ITile t);
	ITile EastOf(ITile t);
	ITile SouthOf(ITile t);
	ITile WestOf(ITile t);
	void ShiftCurrentTile(DirectionType dir);
	void Research(ITile t, GameObject prefab);
	bool InvalidTileLocation(int x, int y);
	bool InvalidTileLocation(ITile tile);
}