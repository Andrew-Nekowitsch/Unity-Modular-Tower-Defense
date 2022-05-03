using UnityEngine;

public class Tiles : MonoBehaviour
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

	public GameObject gUp;
	public GameObject gDown;
	public GameObject gLeft;
	public GameObject gRight;

	public GameObject pUp;
	public GameObject pDown;
	public GameObject pLeft;
	public GameObject pRight;

	[SerializeField] private TileType up = TileType.None;
	[SerializeField] private TileType down = TileType.None;
	[SerializeField] private TileType left = TileType.None;
	[SerializeField] private TileType right = TileType.None;
	[SerializeField] private bool center = false;
	[SerializeField] private bool initialized = false;

	public void Initialize()
	{
		if (initialized == true) return;
		initialized = true;

		Up.SetActive(false);
		Down.SetActive(false);
		Left.SetActive(false);
		Right.SetActive(false);

		Center.SetActive(false);
		Inner.SetActive(false);

		UL.SetActive(false);
		UR.SetActive(false);
		DL.SetActive(false);
		DR.SetActive(false);

		gUp.SetActive(false);
		gDown.SetActive(false);
		gLeft.SetActive(false);
		gRight.SetActive(false);

		pUp.SetActive(false);
		pDown.SetActive(false);
		pLeft.SetActive(false);
		pRight.SetActive(false);
	}

	public void Initialize(Tiles t)
	{
		Initialize();
		if (t.up == TileType.Path) PathUp();
		else if (t.up == TileType.Grass) GrassUp();
		if (t.down == TileType.Path) PathDown();
		else if (t.down == TileType.Grass) GrassDown();
		if (t.left == TileType.Path) PathLeft();
		else if (t.left == TileType.Grass) GrassLeft();
		if (t.right == TileType.Path) PathRight();
		else if (t.right == TileType.Grass) GrassRight();
		if (t.center) SetCenter();
	}

	public void Reset()
	{
		initialized = false;
		center = false;
		up = TileType.None;
		down = TileType.None;
		left = TileType.None;
		right = TileType.None;
	}

	public void PathUp()
	{
		if (up != TileType.None) return;

		up = TileType.Path;
		SetCenter();
		Up.SetActive(true);
		UL.SetActive(true);
		UR.SetActive(true);
		pUp.SetActive(true);
	}

	public void PathDown()
	{
		if (down != TileType.None) return;

		down = TileType.Path;
		SetCenter();
		Down.SetActive(true);
		DL.SetActive(true);
		DR.SetActive(true);
		pDown.SetActive(true);
	}

	public void PathLeft()
	{
		if (left != TileType.None) return;

		left = TileType.Path;
		SetCenter();
		Left.SetActive(true);
		UL.SetActive(true);
		DL.SetActive(true);
		pLeft.SetActive(true);
	}

	public void PathRight()
	{
		if (right != TileType.None) return;

		right = TileType.Path;
		SetCenter();
		Right.SetActive(true);
		UR.SetActive(true);
		DR.SetActive(true);
		pRight.SetActive(true);
	}

	private void SetCenter()
	{
		if (center == true) return;

		center = true;
		Center.SetActive(true);
		Inner.SetActive(true);
	}

	public void GrassUp()
	{
		if (up != TileType.None) return;

		up = TileType.Grass;
		SetCenter();
		Up.SetActive(true);
		UL.SetActive(true);
		UR.SetActive(true);
		gUp.SetActive(true);
	}

	public void GrassDown()
	{
		if (down != TileType.None) return;

		down = TileType.Grass;
		SetCenter();
		Down.SetActive(true);
		DL.SetActive(true);
		DR.SetActive(true);
		gDown.SetActive(true);
	}

	public void GrassLeft()
	{
		if (left != TileType.None) return;

		left = TileType.Grass;
		SetCenter();
		Left.SetActive(true);
		UL.SetActive(true);
		DL.SetActive(true);
		gLeft.SetActive(true);
	}

	public void GrassRight()
	{
		if (right != TileType.None) return;

		right = TileType.Grass;
		SetCenter();
		Right.SetActive(true);
		UR.SetActive(true);
		DR.SetActive(true);
		gRight.SetActive(true);
	}
}
