using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMarrow : MonoBehaviour
{
    public float prodTime = 5;
    private float prodTimer;
    public int cellMax = 3;
    private List<GameObject> cells = new List<GameObject>();
    public GameObject cell;

    void Awake()
    {
        prodTimer = prodTime;
    }

    // Update is called once per frame
    void Update()
    {
        produceCell();
    }

    public void produceCell()
    {
        if(Cells.Count < cellMax)
        {
            if (prodTimer <= 0)
            {
                prodTimer = prodTime;
                GameObject newCell = Instantiate<GameObject>(cell, transform.position, Quaternion.identity);
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
