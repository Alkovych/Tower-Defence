using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebuger : MonoBehaviour
{
    [SerializeField]
    private TileScript goal , start;

    [SerializeField]
    private Sprite blankSprite;

    [SerializeField]
    private GameObject arrowPrefabs;

    [SerializeField]
    private GameObject debugTilePrefab;

	void Start () {
		
	}
	
	void Update ()
	{
	    ClickTile();

	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        AStar.GetPath(start.GridPosition, goal.GridPosition); 
	    }

	}

    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider !=null) // if we hit something we have to make reference
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();

                if (tmp !=null)
                {
                    if (start == null)
                    {
                        start = tmp;

                        CreateDebugTile(start.WorldPosition, new Color32(255,135,0,255));
                       
                    }
                    else if (goal == null)
                    {

                        goal = tmp;


                        CreateDebugTile(goal.WorldPosition, new Color32(255, 0, 0, 255));
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList, Stack<Node> path)
    {
        foreach (Node node in openList)
        {
            if (node.TileRef != start && node.TileRef != goal)
            {

               CreateDebugTile(node.TileRef.WorldPosition, Color.cyan , node);
            }

            PointToParent(node,node.TileRef.WorldPosition);
        }

        foreach (Node node in closedList)
        {
            if (node.TileRef != start && node.TileRef != goal && !path.Contains(node))
            {

                CreateDebugTile(node.TileRef.WorldPosition, Color.blue , node);
            }

            PointToParent(node, node.TileRef.WorldPosition);
        }

        foreach (Node node in path)
        {
            if (node.TileRef != start && node.TileRef != goal)
            {

                CreateDebugTile(node.TileRef.WorldPosition, Color.green, node);
            }
        }
    }

    private void PointToParent(Node node, Vector2 position)
    {
        if (node.Parent != null)
        {
            GameObject arrow = (GameObject)Instantiate(arrowPrefabs, position, Quaternion.identity);

            arrow.GetComponent<SpriteRenderer>().sortingOrder = 3;

             // Right
            if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y == node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            // Top Right
            else if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 45);
            }

            // UP
            else if ((node.GridPosition.X == node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 90);
            }

            // Top Left
            else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y > node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 135);
            }

            // Left
            else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y == node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 180);
            }

            // Botton Left
            else if ((node.GridPosition.X > node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 225);
            }

            // Botton 
            else if ((node.GridPosition.X == node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 270);
            }
            //Right
            else if ((node.GridPosition.X < node.Parent.GridPosition.X) && (node.GridPosition.Y < node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 315);
            }

        }
    }

    private void CreateDebugTile(Vector3 worldPos, Color32 color , Node node = null)
    {
        GameObject debugTile = (GameObject)Instantiate(debugTilePrefab, worldPos, Quaternion.identity);

        if (node != null)
        {
            DebugTile tmp = debugTile.GetComponent<DebugTile>();

            tmp.G.text += node.G;
            tmp.H.text += node.H;
            tmp.F.text += node.F;
        }

        debugTile.GetComponent<SpriteRenderer>().color = color;
    }
}
