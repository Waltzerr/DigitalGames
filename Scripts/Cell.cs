using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
public class Cell : MovingObject
{
    public Virus virus;
    public bool infected = false;
    public float burstRate = 3;
    private float burstTimer;
    public int worth = 1;
    
    private void Awake()
    {
        Init();
        Target = gameManager.end.transform;
        gameManager.InfectableCells.Add(gameObject);
        burstTimer = burstRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (atEnd())
        {
            if (infected)
            {
                playSound(3);
                GameManager.Instance.HP -= (int)Mathf.Round(virus.reproRate * burstRate);
            }
            else
            {
                playSound(Random.Range(0, 3));
                ShopManager.Instance.DNA += worth;
            }
            Destroy();
        }
        if (infected)
        {
            Burst();
        }
    }

    public void Infect()
    {
        gameManager.InfectableCells.Remove(gameObject);
        GameManager.Instance.InfectedCells.Add(gameObject);
        infected = true;
    }

    public void Burst()
    {
        if(burstTimer <= 0)
        {         
            for (int i = 0; i < Mathf.Round(virus.reproRate*burstRate); i++)
            {
                Vector3 trajectory = Random.insideUnitCircle * 200f;
                GameObject newVirus = Instantiate<GameObject>(virus.gameObject, transform.position, Quaternion.identity);
                newVirus.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-600f, 600f) + trajectory.x, Random.Range(-600f, 600f) + trajectory.y));
            }
            Destroy();
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x+0.0007f, transform.localScale.y + 0.0007f, transform.localScale.z);
            sprite.color = new Color(sprite.color.r - 0.001f, sprite.color.g - 0.001f, sprite.color.b - 0.001f);
            burstTimer -= Time.deltaTime;
        }
    }
    public override void Destroy()
    {
        if (gameManager.InfectableCells.Contains(gameObject))
        {
            gameManager.InfectableCells.Remove(gameObject);
        }
        if (gameManager.InfectedCells.Contains(gameObject))
        {
            gameManager.InfectedCells.Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
