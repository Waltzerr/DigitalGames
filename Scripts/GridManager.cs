using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GridManager : MonoBehaviour
{
    private AstarPath astar;
    public GameObject start;
    public GameObject end;
    public GameObject wall;
    public GameObject path;
    private GameObject[] walls;
    private List<GameObject> towers = new List<GameObject>();

    private void Start()
    {
        astar = AstarPath.active;
        astar.Scan();
    }

    public void fillGrid(Round round)
    {
        walls = new GameObject[astar.data.gridGraph.nodes.Length];
        int i = 0;
        foreach(GridNode node in astar.data.gridGraph.nodes)
        {
            GameObject newWall = Instantiate<GameObject>(wall, (Vector3)node.position, Quaternion.identity);
            //newWall.transform.parent = transform;
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

    private GameObject placeOnGrid(GameObject gameObject, Vector2 coords)
    {
        GameObject newObject = Instantiate(gameObject, coordToPos(coords), Quaternion.identity);
        //newObject.transform.parent = transform;
        GameObject removeWall = walls[coordToIndex(coords)];
        walls[coordToIndex(coords)] = null;
        Destroy(removeWall);
        return newObject;
    }

    public GameObject placeOnGrid(Vector3 mousePos)
    {
        return placeOnGrid(path, posToCoord(mousePos));
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
