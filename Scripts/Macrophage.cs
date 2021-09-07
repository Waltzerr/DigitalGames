using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macrophage : AttackingObject
{
    
    public int health = 3;
    private void Awake()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            Destroy();
        }
        if (findTarget(gameManager.Viruses, Tower.transform, 3f))
        {
            health -= 1;
            Target.GetComponent<Virus>().Destroy();
        }
    }

    public override void Destroy()
    {
        if(Tower != null)
        {
            Tower.Cells.Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
