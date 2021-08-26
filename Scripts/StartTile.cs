using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : MonoBehaviour
{
    public GameObject cell;
    public float spawnTime = 2; //time between spawning a cell

    // Update is called once per frame
    void Update()
    {
        //spawn cell every 2 seconds
        //if(spawnTime <= 0)
        //{
        //    spawnTime = 2;
        //    Instantiate<GameObject>(cell, transform.position, Quaternion.identity);
        //}
        //else
        //{
        //    spawnTime -= Time.deltaTime;
        //}
    }
}
