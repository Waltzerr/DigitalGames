using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public bool removePath = false;
    public static GameManager Instance;
    private List<GameObject> infectableCells = new List<GameObject>();
    private List<GameObject> infectedCells = new List<GameObject>();
    private List<GameObject> viruses = new List<GameObject>();
    public GameObject cell;
    public Transform end;
    public Transform start;
    public float spawnTimer;
    public List<GameObject> Towers;
    private int cellIndex;
    private List<Round> rounds = new List<Round>();
    private GridManager gridManager;
    public GameObject[] panels;
    public int TowerUpgrade;//TowerUpgrade

    public bool inRound = false; //this ends when the round has finished spawning cells, needs to end on actual round end
    private Round currentRound;

    public List<GameObject> InfectableCells
    {
        get { return infectableCells; }
    }
    public List<GameObject> InfectedCells
    {
        get { return infectedCells; }
    }
    public List<GameObject> Viruses
    {
        get { return viruses; }
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        gridManager.Setup();
        rounds.Add(new Round(30, new int[] { 3, 8, 13, 18, 23, 24, 25}, 1.75f, new Vector2(0, 7), new Vector2(11, 5), new List<(GameObject, Vector2)> { (Towers[2], new Vector2(4, 6)), (Towers[0], new Vector2(2, 4)) }));
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
            if (Input.GetMouseButton(0))
            {
                gridManager.removeOrPlacePath(MousePos());
            }
        }

    }

    public Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void executeRound(Round round)
    {
        spawnTimer = round.spawnTime;
        if (cellIndex <= round.cellAmount)
        {
            spawnTimer = round.spawnTime;
            Vector3 pos = start.transform.position;
            GameObject newCell = Instantiate<GameObject>(cell, pos, Quaternion.identity);
            if (round.infectedCells.Contains(cellIndex)) //if the index is in the infected cells array, infect this cell
            {
                newCell.GetComponent<Cell>().Infect();
            }
            cellIndex++;
        }
        else if(viruses.Count == 0 && infectableCells.Count == 0 && infectedCells.Count == 0)
        {
            Debug.Log("Round end");
            inRound = false;
        }
    }

    public void startRound(Round round)
    {
        AstarPath.active.Scan();
        currentRound = round;
        inRound = true;
        spawnTimer = round.spawnTime;
        cellIndex = 1; //the number of the cell about to be spawned
    }

    public void setPlacePath()
    {
        removePath = false;
    }
    public void setRemovePath()
    {
        removePath = true;
    }
    public void Upgrade(int upgrade)
    {
        TowerUpgrade = upgrade;
        ChoosePanel(1);
    }

    public void ChoosePanel(int panelID)
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[panelID].SetActive(true);
    }

    public void ChooseUpgrade(int upgrade)
    {
        if(upgrade == 1)
        {
            //SpeedFunc(TowerUpgrade);
        }
        else if (upgrade == 2)
        {
            //statFunc(TowerUpgrade);
        }
        else if(upgrade == 3)
        {
            //statFunc(TowerUpgrade);
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Sample Round 1");
    }
}
