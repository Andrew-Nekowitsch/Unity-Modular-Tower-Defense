using UnityEngine;

public interface IModule
{
	int X { get; set; }
    int Y { get; set; }
    Neighbors Neighbors { get; set; }
    DirectionType WalkDir { get; set; }
    Tiles Tiles { get; set; }

	void SetTileType(ModuleType value);
	Visibility GetVisible();
	void SetVisible(Visibility value);
	ModuleType GetTileType();
	void SetGameObject(GameObject value);
	GameObject GetGameObject();

    void SetVisibility(Visibility v);
    void Initialize(int x, int y);
    void SetCoordinates(int x, int y);
    void SetNeighbors(IModule n, IModule e, IModule s, IModule w);
    string ToString();
    void Instantiate(GameObject prefab, GameObject parent);
}
