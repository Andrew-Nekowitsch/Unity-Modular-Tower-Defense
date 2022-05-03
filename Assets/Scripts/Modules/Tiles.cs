using UnityEngine;

public interface ITile
{

}

public class Tiles : MonoBehaviour, ITile
{
	public GameObject Up;
	public GameObject Down;
	public GameObject Left;
	public GameObject Right;
	public GameObject Center;

	public GameObject UL;
	public GameObject UR;
	public GameObject DL;
	public GameObject DR;
	public GameObject Inner;

	private bool up = false;
	private bool down = false;
	private bool left = false;
	private bool right = false;
	private bool center = false;
	private bool initialized = false;

	public void Initialize()
	{
		if (initialized) return;
		initialized = true;
		Up.SetActive(false);
		Down.SetActive(false);
		Left.SetActive(false);
		Right.SetActive(false);
		Center.SetActive(false);
		UL.SetActive(false);
		UR.SetActive(false);
		DL.SetActive(false);
		DR.SetActive(false);
		Inner.SetActive(false);
	}

	public void Reset()
	{
		initialized = false;
		up = false;
		down = false;
		left = false;
		right = false;
		center = false;
	}

	public void SetUp()
	{
		if (up == true) return;

		up = true;
		SetCenter();
		Up.SetActive(true);
		UL.SetActive(true);
		UR.SetActive(true);
	}

	public void SetDown()
	{
		if (down == true) return;

		down = true;
		SetCenter();
		Down.SetActive(true);
		DL.SetActive(true);
		DR.SetActive(true);
	}

	public void SetLeft()
	{
		if (left == true) return;

		left = true;
		SetCenter();
		Left.SetActive(true);
		UL.SetActive(true);
		DL.SetActive(true);
	}

	public void SetRight()
	{
		if (right == true) return;

		right = true;
		SetCenter();
		Right.SetActive(true);
		UR.SetActive(true);
		DR.SetActive(true);
	}

	private void SetCenter()
	{
		if (center == true) return;

		center = true;
		Center.SetActive(true);
		Inner.SetActive(true);
	}
}
