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

    public Tiles(GameObject gameObject)
    {
    }
}
