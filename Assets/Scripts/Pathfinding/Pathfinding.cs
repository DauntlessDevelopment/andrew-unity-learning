using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinding
{
    List<PathNode> open = new List<PathNode>();//nodes to be evaluated
    List<PathNode> closed = new List<PathNode>();//nodes that have been visited

    Grid<PathNode> grid = ServiceLocator.Get<Grid<PathNode>>();

    List<PathNode> path = new List<PathNode>();

    public void FindPath(Vector3 start, Vector3 end)
    {
        FindPath(grid.GetValue(start), grid.GetValue(end));
    }

    public void FindPath(PathNode start, PathNode end)
    {
        open.Add(start);

        while(open.Any())
        {
            PathNode current = open[0];
            for(int i = 0; i < open.Count; i++)
            {
                if(open[i].fCost < current.fCost || (open[i].fCost == current.fCost && open[i].hCost < current.hCost))
                {
                    current = open[i];
                }
            }

            open.Remove(current);
            closed.Add(current);

            if(current == end)
            {
                //retrace
                open.Clear();
                closed.Clear();
                return;
            }
            foreach(var n in current.GetConnectedNodes())
            {
                if(closed.Contains(n))
                {
                    continue;
                }

                float move_cost = current.gCost + Vector2.Distance(current.GetGridPos(), n.GetGridPos());
                if(move_cost < n.gCost || !open.Contains(n))
                {
                    n.gCost = move_cost;
                    n.hCost = Vector2.Distance(n.GetGridPos(), end.GetGridPos());
                    n.lastNode = current;

                    if(!open.Contains(n))
                    {
                        open.Add(n);
                    }
                }
            }
        }
        //retrace
        open.Clear();
        closed.Clear();
       
    }


}

public class PathNode
{
    //private Grid<PathNode> grid;
    private int x;
    private int y;

    public Vector2 GetGridPos() { return new Vector2(x, y); }

    private List<PathNode> connectedNodes = new List<PathNode>();
    public void AddConnectedNode(PathNode n) { connectedNodes.Add(n); }
    public List<PathNode> GetConnectedNodes() { return connectedNodes; }


    public float gCost;
    public float hCost;
    public float fCost { get { return gCost + hCost; } }

    public PathNode lastNode;

    public PathNode(int x, int y)
    {
        //this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}