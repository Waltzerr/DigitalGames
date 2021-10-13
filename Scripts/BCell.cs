using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BCell : MovingObject
{
    private bool canProduce;
    public float produceTime;
    public Antibody antibody;
    public float range;

    protected void Awake()
    {
        Init();
        canProduce = false;
        Target = gameManager.end.transform;
        StartCoroutine(WaitProduceCoroutine(produceTime));
    }

    void Update()
    {
        if (atEnd())
        {
            Destroy();
        }
        if (canProduce)
        {
            if (virusInRange(range))
            {
                StartCoroutine(WaitProduceCoroutine(produceTime));
                Instantiate(antibody, transform.position, Quaternion.identity);
            }
        }
    }
    IEnumerator WaitProduceCoroutine(float waitTime)
    {
        canProduce = false;
        yield return new WaitForSeconds(waitTime);
        canProduce = true;
    }
    protected bool virusInRange(float range)
    {
        foreach (GameObject virus in GameManager.Instance.Viruses)
        {
            if (Vector3.Distance(virus.transform.position, transform.position) <= range)
            {
                return true;
            }
        }
        return false;
    }

    public override void Destroy()
    {
        if (Tower != null)
        {
            Tower.Cells.Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
