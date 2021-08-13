using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackingObject : MovingObject
{
    //Find the closest target from a list of potential targets, set target to itself if none can be found
    //Things to add: range, reset spot (itself, a tower or position on map for when no targets found)
    public bool findTarget(List<GameObject> targetList, Transform resetPos)
    {
        if(Target == null)
        {
            Target = resetPos;
        }
        if (targetList.Count > 0)
        {
            GameObject targetCell = targetList[0];
            foreach (GameObject target in targetList)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < Vector3.Distance(transform.position, targetCell.transform.position))
                {
                    targetCell = target;
                }
            }
            Target = targetCell.transform;
        }
        else if(!targetList.Contains(Target.gameObject))
        {
            Target = resetPos;
        }

        if(targetList.Contains(Target.gameObject) && GetComponent<Collider2D>().IsTouching(Target.GetComponent<Collider2D>()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
