using UnityEngine;

public interface IBoard
{
	IModule[,] Modules { get; set; }
	IModule StartingModule { get; set; }
	IModule CurrentModule { get; set; }

	void InstantiateModule(IModule tile, GameObject prefab);
	void InstantiateBorder(IModule tile);
	bool AddHidden(IModule t, GameObject prefab);
	bool Add(IModule t, GameObject prefab);
	bool AddWithoutNeighbors(IModule t, GameObject prefab);
	bool AddWithoutInstantiation(IModule t);
	IModule GetModuleAt(int x, int y);
	void Log();
	void SetNeighbors();
	void SetNeighbors(IModule t);
	IModule NorthOf(IModule t);
	IModule EastOf(IModule t);
	IModule SouthOf(IModule t);
	IModule WestOf(IModule t);
	void ShiftCurrentTile(DirectionType dir);
	void Research(IModule t, GameObject prefab);
	bool InvalidTileLocation(int x, int y);
	bool InvalidTileLocation(IModule tile);
}