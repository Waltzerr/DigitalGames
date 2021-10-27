using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : AttackingObject
{
    public float slowAmount;
    private void Update()
    {
        if (atEnd())
        {
            Destroy();
        }
        if (findTarget(gameManager.Viruses, GameManager.Instance.end, Range))
        {
            Virus virus = Target.GetComponent<Virus>();
            virus.aiPath.maxSpeed = virus.aiPath.maxSpeed*slowAmount;
            Destroy();
        }
    }
    private void Awake()
    {
        Init();
        playSound(0);
    }
    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
