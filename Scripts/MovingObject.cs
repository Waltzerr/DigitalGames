using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class MovingObject : MonoBehaviour
{
    public GameManager gameManager;
    public SpriteRenderer sprite;
    public AIPath aiPath;
    public void Init()
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
