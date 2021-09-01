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
        foreach(GridNode node in astar.data.gridGraph.nodes)
        {
            GameObject newWall = Instantiate<GameObject>(wall, (Vector3)node.position, Quaternion.identity);
            walls[i] = newWall;
            i++;
        }
        foreach((GameObject, Vector2) tower in round.Towers)
        {
            GameObject newTower = placeOnGrid(tower.Item1, tower.Item2);
            towers.Add(newTower);
        }
        placeOnGrid(start, round.startPos);
        placeOnGrid(end, round.endPos);
        astar.Scan();
    }

    //returns a list of all connected paths
    public List<GameObject> connectedPath(Vector3 from)
    {
        List<GameObject> connected = new List<GameObject>();
        foreach(GameObject path in paths)
        {
            Debug.Log(Vector3.Distance(path.transform.position, from));
            if(Vector3.Distance(path.transform.position, from) < 1.5f && path.transform.position != from)
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
        if (!hasPath(mousePos))
        {
            GameObject newPath = placeOnGrid(path, posToCoord(mousePos));
            paths.Add(newPath);
        }
    }

    private bool hasPath(Vector3 pos)
    {
        foreach(GameObject path in paths)
        {
            if(Vector3.Distance(path.transform.position, coordToPos(posToCoord(pos))) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 coordToPos(Vector2 coords)
    {
        int index = (int)coords.x + (int)(coords.y * (astar.data.gridGraph.width));
        return (Vector3)astar.data.gridGraph.nodes[index].position;
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
                int y = i/ astar.data.gridGraph.width;
                int x = i - (y * astar.data.gridGraph.width);
                return new Vector2(x, y);
            }
        }
        return Vector2.zero;
    }

}
