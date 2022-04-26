using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
	TileType GetTileType();
	void SetTileType(TileType value);

	int X { get; set; }
    int Y { get; set; }
    Neighbors Neighbors { get; set; }
    DirectionType WalkDir { get; set; }

	Visibility GetVisible();
	void SetVisible(Visibility value);
	GameObject GetGameObject();
	void SetGameObject(GameObject value);
    void SetVisibility(Visibility v);
    void Initialize(int x, int y);
    void SetCoordinates(int _x, int _y);
    void SetNeighbors(ITile n, ITile e, ITile s, ITile w);
    string ToString();
    void Instantiate(GameObject prefab, GameObject parent);
}
