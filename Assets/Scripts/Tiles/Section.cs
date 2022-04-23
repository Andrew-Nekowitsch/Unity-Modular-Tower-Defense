using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISection
{

}

public class Section : MonoBehaviour
{
    public GameObject SectionObject { get; set; }
    public ISection section { get; set; }

}
