using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Tower : Object
{
    public float prodTime = 5;
    public float prodMultiplier = 1;
    private float prodTimer;
    public int cellMax = 3;
    private List<GameObject> cells = new List<GameObject>();
    public GameObject cell;

    void Awake()
    {
        Init();
        prodTimer = prodTime * prodMultiplier;
    }

    public void updateProduction(float mult)
    {
        prodMultiplier -= mult;
        prodTimer = prodTime * prodMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.inRound)
        {
            if(GridManager.Instance.connectedPath(transform.position).Count > 0)
            {
                produceCell();
            }
        }
    }

    public void produceCell()
    {
        if(Cells.Count < cellMax)
        {
            if (prodTimer <= 0)
            {
                prodTimer = prodTime * prodMultiplier;

                NNConstraint constraint = NNConstraint.Default;
                constraint.walkable = true;
                constraint.constrainWalkability = true;
                GraphNode node = AstarPath.active.GetNearest(transform.position, constraint).node;
                Vector3 pos = (Vector3)node.position;
                GameObject newCell = Instantiate<GameObject>(cell, pos, Quaternion.identity);
                newCell.GetComponent<MovingObject>().Tower = this;
                Cells.Add(newCell);
            }
            else
            {
                prodTimer -= Time.deltaTime;
            }
        }
    }

    public void deleteCells()
    {
        foreach(GameObject cell in cells)
        {
            cell.GetComponent<MovingObject>().Tower = null;
            cell.GetComponent<MovingObject>().Destroy();
        }
        cells.Clear();
    }

    public List<GameObject> Cells
    {
        get { return cells; }
    }
}
