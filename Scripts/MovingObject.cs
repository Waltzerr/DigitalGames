using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]

public abstract class MovingObject : Object
{
    
    public AIPath aiPath;
    private Tower tower;

    public new void Init()
    {
        gameManager = FindObjectOfType<GameManager>();
        sprite = GetComponent<SpriteRenderer>();
        aiPath = GetComponent<AIPath>();
    }

    public Transform Target
    {
        get { return GetComponent<AIDestinationSetter>().target; }
        set { GetComponent<AIDestinationSetter>().target = value; }
    }

    public bool atEnd()
    {
        if (Vector3.Distance(GameManager.Instance.end.position, transform.position) <= 0.25f && Target == GameManager.Instance.end)
        {
            return true;
        }
        return false;
    }

    public abstract void Destroy();

    public Tower Tower
    {
        get { return tower; }
        set { tower = value; }
    }
}
