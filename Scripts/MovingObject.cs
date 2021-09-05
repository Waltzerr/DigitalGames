using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class MovingObject : Object
{
    
    public AIPath aiPath;

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
        if (Vector3.Distance(GameManager.Instance.end.position, transform.position) <= 1f && Target == GameManager.Instance.end)
        {
            return true;
        }
        return false;
    }

    public abstract void Destroy();
}
