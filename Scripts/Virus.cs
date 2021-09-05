using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Virus : AttackingObject
{
    //how many viruses it will produce per second in an infected cell
    public float reproRate = 1f;
    public float range;
    void Awake()
    {
        Init();
        gameManager.Viruses.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (atEnd())
        {
            Destroy();
        }
        if (findTarget(gameManager.InfectableCells, gameManager.end.transform, range))
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
