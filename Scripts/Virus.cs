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
        playSound(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (atEnd())
        {
            playSound(1);
            GameManager.Instance.HP -= 1;
            Destroy();
        }
        if (findTarget(gameManager.InfectableCells, GameManager.Instance.end, range))
        {
            playSound(0);
            Target.GetComponent<Cell>().Infect();
            Destroy();
        }
    }

    public override void Destroy()
    {
        if(Vector2.Distance(Target.position, transform.position) > 0.25f)
        {
            playSound(3, 0.25f);
        }
        gameManager.Viruses.Remove(gameObject);
        Destroy(gameObject);
    }
}
