using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private AstarPath astar;
    public GameObject start; //start of path
    public GameObject end; //end of path
    public GameObject wall; //prefab of non-passable area
    public GameObject path; //prefab of passable area
    private GameObject[] walls; //array of all non-passable areas
    private List<GameObject> towers = new List<GameObject>(); //list of all towers
    private List<GameObject> paths = new List<GameObject>(); //list of all paths

    private void Awake()
    {
        Instance = this;
    }
    //sets up the class
    public void Setup()
    {
        astar = AstarPath.active;
        astar.Scan();
    }

    //fills the grid with walls
    public void fillGrid(Round round)
    {
        walls = new GameObject[astar.data.gridGraph.nodes.Length];
        int i = 0;
        foreach (GridNode node in astar.data.gridGraph.nodes)
        {
            GameObject newWall = Instantiate<GameObject>(wall, (Vector3)node.position, Quaternion.identity);
            walls[i] = newWall;
            i++;
        }
        foreach ((GameObject, Vector2) tower in round.Towers)
        {
            GameObject newTower = placeOnGrid(tower.Item1, tower.Item2);
            towers.Add(newTower);
        }
        GameManager.Instance.start = placeOnGrid(start, round.startPos).transform;
        GameManager.Instance.end = placeOnGrid(end, round.endPos).transform;
        paths.Add(GameManager.Instance.start.gameObject);
        paths.Add(GameManager.Instance.end.gameObject);
        astar.Scan();
    }

    //returns a list of all connected paths
    public List<GameObject> connectedPath(Vector3 from)
    {
        List<GameObject> connected = new List<GameObject>();
        foreach (GameObject path in paths)
        {
            if (Vector3.Distance(path.transform.position, from) <= 1.05f && path.transform.position != from)
            {
                connected.Add(path);
            }
        }
        return connected;
    }

    //places any object on the path and removes the walls under it
    private GameObject placeOnGrid(GameObject gameObject, Vector2 coords)
    {
        GameObject newObject = Instantiate(gameObject, coordToPos(coords), Quaternion.identity);
        GameObject removeWall = walls[coordToIndex(coords)];
        walls[coordToIndex(coords)] = null;
        Destroy(removeWall);
        return newObject;
    }

    //places path on the mouse position
    public void placeOnGrid(Vector3 mousePos)
    {
        if (canPlace(mousePos))
        {
            Vector2 coords = posToCoord(mousePos);
            if (coords.x != -1)
            {
                GameObject newPath = placeOnGrid(path, coords);
                paths.Add(newPath);
            }
        }
    }

    private bool canPlace(Vector3 pos)
    {
        Vector2 coords = posToCoord(pos);
        if (coords == posToCoord(GameManager.Instance.start.position) || coords == posToCoord(GameManager.Instance.end.position))
        {
            return false;
        }
        foreach (GameObject path in paths)
        {
            if (Vector3.Distance(path.transform.position, coordToPos(coords)) < 0.5f)
            {
                return false;
            }
        }
        foreach (GameObject tower in towers)
        {
            if (Vector3.Distance(tower.transform.position, coordToPos(coords)) < 0.5f)
            {
                return false;
            }
        }
        return true;
    }

    private Vector3 coordToPos(Vector2 coords)
    {
        int index = (int)coords.x + (int)(coords.y * (astar.data.gridGraph.width));
        if (index >= 0 && index < astar.data.gridGraph.nodes.Length)
        {
            return (Vector3)astar.data.gridGraph.nodes[index].position;
        }
        return new Vector2(-1, -1);
    }

    private int coordToIndex(Vector2 coords)
    {
        int index = (int)coords.x + (int)(coords.y * (astar.data.gridGraph.width));
        return index;
    }

    public Vector2 posToCoord(Vector3 pos)
    {
        Vector3 exactPos = astar.GetNearest(pos).position;
        for (int i = 0; i < astar.data.gridGraph.nodes.Length; i++)
        {
            if (Vector3.Distance((Vector3)astar.data.gridGraph.nodes[i].position, exactPos) < 0.5)
            {
                int y = i / astar.data.gridGraph.width;
                int x = i - (y * astar.data.gridGraph.width);
                return new Vector2(x, y);
            }
        }
        return new Vector2(-1, -1);
    }

}
