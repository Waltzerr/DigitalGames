using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macrophage : AttackingObject
{
    private BoneMarrow boneMarrow;
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
        if (findTarget(gameManager.Viruses, transform))
        {
            health -= 1;
            Target.GetComponent<Virus>().Destroy();
        }
    }

    public override void Destroy()
    {
        if(BoneMarrow != null)
        {
            BoneMarrow.Cells.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    public BoneMarrow BoneMarrow
    {
        get { return boneMarrow; }
        set { boneMarrow = value; }
    }
}
