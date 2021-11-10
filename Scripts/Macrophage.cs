using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Macrophage : AttackingObject
{
    public int health = 3;
    private void Awake()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            Destroy();
        }
        if (atEnd())
        {
            Destroy();
        }
        if (findTarget(gameManager.Viruses, GameManager.Instance.end, Range))
        {
            if(Target != null)
            {
                health -= 1;
                Target.GetComponent<Virus>().Destroy();
            }
        }
    }

    public override void Destroy()
    {
        if(Tower != null)
        {
            Tower.Cells.Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
