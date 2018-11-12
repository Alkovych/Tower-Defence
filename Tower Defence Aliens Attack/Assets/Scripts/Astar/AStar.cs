using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public static class AStar

{

    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition,new Node(tile));
        }
    }


    public static void GetPath(Point start , Point goal)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();


        Node currentNode = nodes[start];

        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighborsPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    if (LevelManager.Instance.InBounds(neighborsPos) && LevelManager.Instance.Tiles[neighborsPos].WalkAble && neighborsPos != currentNode.GridPosition)
                    {
                        int gCost = 0;

                        if (System.Math.Abs(x - y) == 1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if (!ConnectedDiagonally(currentNode , nodes[neighborsPos]))
                            {
                                continue;
                            }
                            gCost = 14;
                        }

                        Node neigbour = nodes[neighborsPos];
                        // Debbuging
                        //neigbour.TileRef.SpriteRenderer.color = Color.black;



                        if (openList.Contains(neigbour))
                        {
                            if (currentNode.G + gCost < neigbour.G)
                            {
                                neigbour.CalcValues(currentNode, nodes[goal], gCost);
                            }
                        }

                        else if (!closedList.Contains(neigbour))
                        {
                            openList.Add(neigbour);
                            neigbour.CalcValues(currentNode, nodes[goal], gCost);
                        }
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }

            if (currentNode == nodes[goal])
            {
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;

            }
        }

   

        //**** Debbuger ****

        GameObject.Find("Debuger").GetComponent<AStarDebuger>().DebugPath(openList , closedList, finalPath);

    }

    private static bool ConnectedDiagonally(Node currentNode, Node neighbour)
    {
        Point direction = neighbour.GridPosition - currentNode.GridPosition;
        Point first = new Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);
        Point second = new Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);
        if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].WalkAble)
        {
            return false;
        }
        if (LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].WalkAble)
        {
            return false;
        }

        return true;
    }
}
