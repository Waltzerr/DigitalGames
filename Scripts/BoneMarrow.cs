using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMarrow : Object
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
            produceCell();
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
                pos.z -= 1; //makes cells appear above the path
                GameObject newCell = Instantiate<GameObject>(cell, pos, Quaternion.identity);
                newCell.GetComponent<Macrophage>().BoneMarrow = this;
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
