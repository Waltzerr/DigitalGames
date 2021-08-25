using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackingObject : MovingObject
{
    private float Range;
    //Find the closest target from a list of potential targets, set target to itself if none can be found
    //Todo: range
    public bool findTarget(List<GameObject> targetList, Transform resetPos, float range = 25f)
    {
        Range = range;
        Target = resetPos;

        if (targetList.Count > 0)
        {
            foreach (GameObject target in targetList)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if(Target != resetPos)
                {
                    if (distance < Vector3.Distance(transform.position, Target.transform.position) && distance < range)
                    {
                        Debug.Log($"{gameObject.name}: Distance: {distance}, Range: {range}");
                        Target = target.transform;
                    }
                }
                else
                {
                    if (distance < range)
                    {
                        Debug.Log($"{gameObject.name}: Distance: {distance}, Range: {range}");
                        Target = target.transform;
                    }
                }
                
            }
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

    public void OnDrawGizmos()
    {
       // UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, Range);
    }
}
