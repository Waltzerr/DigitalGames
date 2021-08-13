using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class GameManager : MonoBehaviour
{
    private List<GameObject> infectableCells = new List<GameObject>();
    private List<GameObject> viruses = new List<GameObject>();
    public Transform end;

    public List<GameObject> InfectableCells
    {
        get { return infectableCells; }
    }
    public List<GameObject> Viruses
    {
        get { return viruses; }
    }
}
