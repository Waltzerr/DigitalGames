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
    public List<GameObject> Towers;
    private int cellIndex;
    private List<Round> rounds = new List<Round>();
    private GridManager gridManager;

    public bool inRound = false; //this ends when the round has finished spawning cells, needs to end on actual round end
    private Round currentRound;

    public List<GameObject> InfectableCells
    {
        get { return infectableCells; }
    }
    public List<GameObject> Viruses
    {
        get { return viruses; }
    }

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        rounds.Add(new Round(5, new int[] { 3 }, 2, new Vector2(0, 7), new Vector2(11, 5), new List<(GameObject, Vector2)> { (Towers[0], new Vector2(4, 6)) }));
        gridManager.fillGrid(rounds[0]);
    }

    private void Update()
    {
        if (inRound)
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
        else 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startRound(rounds[0]);
            }
            if (Input.GetMouseButtonDown(0))
            {
                gridManager.placeOnGrid(mousePos());
            }
        }

    }

    public Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void executeRound(Round round)
    {
        spawnTimer = round.spawnTime;
        if (cellIndex < round.cellAmount)
        {
            spawnTimer = round.spawnTime;
            Vector3 pos = start.transform.position;
            pos.z -= 1; //makes cells appear above the path
            GameObject newCell = Instantiate<GameObject>(cell, pos, Quaternion.identity);
            if (round.infectedCells.Contains(cellIndex)) //if the index is in the infected cells array, infect this cell
            {
                newCell.GetComponent<Cell>().Infect();
            }
            cellIndex++;
        }
        else
        {
            inRound = false;
        }
    }

    public void startRound(Round round)
    {
        AstarPath.active.Scan();
        start = FindObjectOfType<StartTile>().transform; //starting position
        end = FindObjectOfType<EndTile>().transform; //end position
        currentRound = round;
        inRound = true;
        spawnTimer = round.spawnTime;
        cellIndex = 1; //the number of the cell about to be spawned
    }
}
