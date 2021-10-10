using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Neutrophil : AttackingObject
{
    public Sprite explosionSprite;
    public float disperseTime;
    bool hasExploded = false;
    Vector3 initScale;
    private void Awake()
    {
        Init();
        initScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasExploded)
        {
            if (transform.localScale.x < initScale.x * 1.5)
            {
                transform.localScale = new Vector3(transform.localScale.x + 0.004f, transform.localScale.y + 0.004f, transform.localScale.z);
                sprite.sprite = explosionSprite;
            }
        }
        else
        {
            if (findTarget(gameManager.Viruses, GameManager.Instance.end, Range))
            {
                Explode();
            }
        }
        if (atEnd())
        {
            Destroy();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hasExploded)
        {
            collision.GetComponent<MovingObject>().Destroy();
        }
    }

    IEnumerator WaitDisperse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy();
    }

    public override void Destroy()
    {
        if (Tower != null)
        {
            Tower.Cells.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    public void Explode()
    {
        aiPath.maxSpeed = 0;
        hasExploded = true;
        StartCoroutine(WaitDisperse(disperseTime));
    }
}
