using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Object
{
    public float prodTime = 5;
    private float prodTimer;
    public int cellMax = 3;
    private List<GameObject> cells = new List<GameObject>();
    public GameObject cell;

    void Awake()
    {
        Init();
        prodTimer = prodTime;
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
                prodTimer = prodTime;
                Vector3 pos = transform.position;
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

    public List<GameObject> Cells
    {
        get { return cells; }
    }
}
