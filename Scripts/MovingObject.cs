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

    public abstract void Destroy();
}
