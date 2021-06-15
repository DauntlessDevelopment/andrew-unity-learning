using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavGrid : MonoBehaviour
{
    Grid<PathNode> grid;
    [SerializeField]private int width;
    [SerializeField]private int height;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(x, y));
        ServiceLocator.Register<Grid<PathNode>>(grid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
