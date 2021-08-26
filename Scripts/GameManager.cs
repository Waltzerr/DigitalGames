using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;
public class GameManager : MonoBehaviour
{
    private List<GameObject> infectableCells = new List<GameObject>();
    private List<GameObject> viruses = new List<GameObject>();
    public GameObject cell;
    public Transform end;
    public Transform start;
    public float spawnTimer;
    private int cellIndex;
    public Round round1 = new Round(5, new int[] { 3 }, 2);
    public Round round2 = new Round(50, new int[] { 5, 10, 15, 20, 25, 30, 35, 40, 45 }, 1.5f);

    private bool playRound = false;
    private Round currentRound;

    public List<GameObject> InfectableCells
    {
        get { return infectableCells; }
    }
    public List<GameObject> Viruses
    {
        get { return viruses; }
    }

    private void Awake()
    {
        startRound(round2);
    }

    private void Update()
    {
        if (playRound)
        {
            if (spawnTimer <= 0)
            {
                executeRound(currentRound);
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }

    }

    public void executeRound(Round round)
    {
        spawnTimer = round.spawnTime;
        if (cellIndex < round.cellAmount)
        {
            spawnTimer = round.spawnTime;
            GameObject newCell = Instantiate<GameObject>(cell, start.transform.position, Quaternion.identity);
            if (round.infectedCells.Contains(cellIndex)) //if the index is in the infected cells array, infect this cell
            {
                newCell.GetComponent<Cell>().Infect();
            }
            cellIndex++;
        }
        else
        {
            playRound = false;
        }
    }

    public void startRound(Round round)
    {
        start = FindObjectOfType<StartTile>().transform; //starting position
        end = FindObjectOfType<EndTile>().transform; //end position
        currentRound = round;
        playRound = true;
        spawnTimer = round.spawnTime;
        cellIndex = 1; //the number of the cell about to be spawned
    }
}
