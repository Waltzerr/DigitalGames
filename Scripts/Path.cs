using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[4];
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!GameManager.Instance.inRound)
        {
            chooseSprite();
        }
    }

    private void chooseSprite()
    {
        List<GameObject> connectedPaths = GridManager.Instance.connectedPath(transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        float newRotation = 0f;
        switch (connectedPaths.Count)
        {
            case 4:
            case 0:
                spriteRenderer.sprite = sprites[3];
                break;
            case 1:
                spriteRenderer.sprite = sprites[0];
                if(connectedPaths[0].transform.position - transform.position == Vector3.up || connectedPaths[0].transform.position - transform.position == Vector3.down)
                {
                    newRotation = 90;
                }
                break;
            case 2:
                Vector3 direction = (connectedPaths[0].transform.position - connectedPaths[1].transform.position).normalized;
                if(direction == Vector3.up || direction == Vector3.down)
                {
                    spriteRenderer.sprite = sprites[0];
                    newRotation = 90;
                    break;
                }
                if (direction == Vector3.left || direction == Vector3.right)
                {
                    spriteRenderer.sprite = sprites[0];
                    break;
                }
                else
                {
                    spriteRenderer.sprite = sprites[1];
                    Vector3 vertical = Vector3.zero;
                    Vector3 horizontal = Vector3.zero;
                    foreach (GameObject path in connectedPaths)
                    {
                        if(path.transform.position - transform.position == Vector3.up || path.transform.position - transform.position == Vector3.down)
                        {
                            vertical = path.transform.position - transform.position;
                        }
                        if (path.transform.position - transform.position == Vector3.right || path.transform.position - transform.position == Vector3.left)
                        {
                            horizontal = path.transform.position - transform.position;
                        }
                    }

                    if(vertical == Vector3.up && horizontal == Vector3.left)
                    {
                        newRotation = 270;
                    }
                    if(vertical == Vector3.up && horizontal == Vector3.right)
                    {
                        newRotation = 180;
                    }
                    if(vertical == Vector3.down && horizontal == Vector3.right)
                    {
                        newRotation = 90;
                    }
                }
                break;
            case 3:
                spriteRenderer.sprite = sprites[2];
                List<Vector3> dir = new List<Vector3>();
                dir.Add((connectedPaths[0].transform.position - transform.position).normalized);
                dir.Add((connectedPaths[1].transform.position - transform.position).normalized);
                dir.Add((connectedPaths[2].transform.position - transform.position).normalized);
                if (!dir.Contains(Vector3.up)){
                    newRotation = 90;
                }
                if (!dir.Contains(Vector3.left)){
                    newRotation = 180;
                }
                if (!dir.Contains(Vector3.down)){
                    newRotation = 270;
                }

                break;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, newRotation));

    }
}
