using System.Collections;
using System.Collections.Generic;
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

}
