using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image gameOverScreen;
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
    public List<GameObject> Cells;
    private int cellIndex;
    private List<Round> rounds = new List<Round>();
    private GridManager gridManager;
    public GameObject[] panels;
    public int TowerUpgrade;//TowerUpgrade
    public int health;
    public TextMeshProUGUI healthDisplay;
    private float spawnMultiplier = 1f;


    public bool inRound = false; //this ends when the round has finished spawning cells, needs to end on actual round end
    private int currentRound;

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
        gameOverScreen.gameObject.SetActive(false);
        Debug.Log(SceneManager.GetActiveScene().name);
        currentRound = 0;
        HP = 30;
        spawnMultiplier = 1f;
        cell.GetComponent<MovingObject>().speed = 1;
        cell.GetComponent<Cell>().worth = 2;
        foreach (GameObject tower in Towers)
        {
            tower.GetComponent<Tower>().cell.GetComponent<MovingObject>().speed = 1;
            tower.GetComponent<Tower>().prodMultiplier = 1f;
        }
        Cells[2].GetComponent<Macrophage>().health = 2;
        Cells[4].GetComponent<Antibody>().slowAmount = 0.8f;
        Cells[5].GetComponent<Neutrophil>().disperseTime = 2.5f;
        gridManager = FindObjectOfType<GridManager>();
        gridManager.Setup();
        rounds.Add(new Round(15, new int[] { 3, 8, 10 }, 1.75f, new Vector2(0, 3), new Vector2(11, 3), new List<(GameObject, Vector2)> { (Towers[0], new Vector2(3, 5)) }));
        rounds.Add(new Round(25, new int[] { 3, 8, 10, 13, 20, 23 }, 1.75f, new Vector2(0, 7), new Vector2(11, 5), new List<(GameObject, Vector2)> { (Towers[1], new Vector2(2, 4)), (Towers[0], new Vector2(4, 6)) }));
        rounds.Add(new Round(35, new int[] { 4, 5, 6, 7, 13, 18, 23, 24, 25, 30, 33 }, 1.75f, new Vector2(0, 0), new Vector2(11, 6), new List<(GameObject, Vector2)> { (Towers[2], new Vector2(2, 3)), (Towers[1], new Vector2(8, 4)), (Towers[0], new Vector2(5, 6)) }));
        gridManager.fillGrid(rounds[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (health <= 0)
        {
            gameOverScreen.gameObject.SetActive(true);
        }
        if (inRound)
        {
            if (spawnTimer <= 0)
            {
                executeRound(rounds[currentRound]);
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
                //Debug.Log($"Valid path: {GridManager.Instance.isValidPath(start.gameObject, end.gameObject)}");
                startRound(rounds[0]);
            }
            
            if (Input.GetMouseButton(0))
            {
                if (removePath)
                {
                    gridManager.removeOrPlacePath(MousePos());
                }
                else
                {
                    if (ShopManager.Instance.canPurchase(2))
                    {
                        gridManager.removeOrPlacePath(MousePos());
                    }
                }
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
        else if (viruses.Count == 0 && infectableCells.Count == 0 && infectedCells.Count == 0)
        {
            foreach(GameObject tower in GridManager.Instance.towers)
            {
                tower.GetComponent<Tower>().deleteCells();
            }
            Debug.Log("Round end");
            currentRound += 1;
            if(rounds.Count > currentRound)
            {
                gridManager.fillGrid(rounds[currentRound]);
            } else
            {
                gameOverScreen.gameObject.SetActive(true);
            }
            inRound = false;
        }
    }

    public void startRound(Round round)
    {
        AstarPath.active.Scan();
        inRound = true;
        round.spawnTime *= spawnMultiplier;
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
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[panelID].SetActive(true);
    }

    public void ChooseUpgrade(int upgrade)
    {
        GameObject upgradeObject;
        switch (TowerUpgrade)
        {
            case 4:
                upgradeObject = cell;
                break;
            default:
                upgradeObject = Towers[TowerUpgrade - 1];
                break;

        }
        if (upgrade == 1)
        {
            if (upgradeObject.TryGetComponent<Tower>(out Tower tower))
            {
                tower.cell.GetComponent<MovingObject>().speed += 0.2f;
            }
            else
            {
                upgradeObject.GetComponent<MovingObject>().speed += 0.2f;
            }
            ShopManager.Instance.DNA -= 5;
        }
        else if (upgrade == 2)
        {
            if (upgradeObject.TryGetComponent<Tower>(out Tower tower))
            {
                foreach(GameObject inGameTower in GridManager.Instance.towers)
                {
                    if(inGameTower.name.Contains(tower.name))
                    {
                        inGameTower.GetComponent<Tower>().updateProduction(0.25f);
                    }
                }
                tower.updateProduction(0.25f);
            }
            else
            {
                spawnMultiplier -= 0.2f;
            }
            ShopManager.Instance.DNA -= 7;
        }
        else if (upgrade == 3)
        {
            switch (TowerUpgrade)
            {
                case 1:
                    Cells[2].GetComponent<Macrophage>().health += 1;
                    break;
                case 2:
                    Cells[4].GetComponent<Antibody>().slowAmount -= 0.15f;
                    break;
                case 3:
                    Cells[5].GetComponent<Neutrophil>().disperseTime += 0.5f;
                    break;
                case 4:
                    cell.GetComponent<Cell>().worth += 1;
                    break;
            }
            ShopManager.Instance.DNA -= 10;
        }
        ShopManager.Instance.purchasedUpgrades.Add(new Vector2Int(TowerUpgrade, upgrade));
    }

    public int HP
    {
        get { return health; }
        set
        {
            health = value;
            healthDisplay.text = $"Health: {health}";
        }
    }
}
