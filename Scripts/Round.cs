using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round 
{
    public int cellAmount;
    public int[] infectedCells;
    public float spawnTime;

    public Round(int _cellAmount, int[] _infectedCells, float _spawnTime)
    {
        cellAmount = _cellAmount;
        infectedCells = _infectedCells;
        spawnTime = _spawnTime;
    }
}
