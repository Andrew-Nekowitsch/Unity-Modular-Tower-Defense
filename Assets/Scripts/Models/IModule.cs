using UnityEngine;

public interface IModule
{
	int X { get; set; }
    int Y { get; set; }
    DirectionType WalkDir { get; set; }
    Tiles Tiles { get; set; }

	void SetVisible(Visibility value);
	Visibility GetVisible();
	void SetModuleType(ModuleType value);
	ModuleType GetModuleType();
	void SetGameObject(GameObject value);
	GameObject GetGameObject();

    void Set(IModule m);

    void SetVisibility(Visibility v);
    void Initialize(int x, int y);
    void SetCoordinates(int x, int y);
    string ToString();
    void Instantiate(GameObject prefab, GameObject parent);
}
