using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round 
{
    public int cellAmount;
    public int[] infectedCells;
    public float spawnTime;
    public Vector2 startPos;
    public Vector2 endPos;
    public List<(GameObject, Vector2)> Towers = new List<(GameObject, Vector2)>();


    public Round(int _cellAmount, int[] _infectedCells, float _spawnTime, Vector2 _startPos, Vector2 _endPos, List<(GameObject, Vector2)> _Towers)
    {
        cellAmount = _cellAmount;
        infectedCells = _infectedCells;
        spawnTime = _spawnTime;
        startPos = _startPos;
        endPos = _endPos;
        Towers = _Towers;
    }
    public Round(int _cellAmount, int[] _infectedCells, float _spawnTime)
    {

    }
}
