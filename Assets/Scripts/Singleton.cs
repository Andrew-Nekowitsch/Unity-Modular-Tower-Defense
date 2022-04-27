using UnityEngine;

public class Singleton : MonoBehaviour
{
	public static Singleton Instance { get; private set; }
	public GameManager GameManager { get; private set; }
	public Gameboard Gameboard { get; private set; }
	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		Instance = this;
		GameManager = GetComponentInChildren<GameManager>();
		Gameboard = GetComponentInChildren<Gameboard>();
	}
}
