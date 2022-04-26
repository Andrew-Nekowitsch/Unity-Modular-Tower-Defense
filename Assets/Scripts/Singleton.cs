using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
	public static Singleton Instance { get; private set; }
	[SerializeField]
	public GameManager GameManager { get; private set; }
	[SerializeField]
	public Gameboard Gameboard { get; private set; }
	private void Awake()
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
