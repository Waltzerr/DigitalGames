using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Virus : AttackingObject
{
    //how many viruses it will produce per second in an infected cell
    public float reproRate = 1f;
    void Awake()
    {
        Init();
        gameManager.Viruses.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (findTarget(gameManager.InfectableCells, transform))
        {
            Target.GetComponent<Cell>().Infect();
            Destroy();
        }
    }

    public override void Destroy()
    {
        gameManager.Viruses.Remove(gameObject);
        Destroy(gameObject);
    }
}
