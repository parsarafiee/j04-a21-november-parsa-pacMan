using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_star;
using UnityEngine.Tilemaps;
public class Tile_Node : Node
{
    public GameObject gameObject;
    Vector3Int[] delta = new Vector3Int[] { Vector3Int.right, Vector3Int.down, Vector3Int.left, Vector3Int.up };
    public Vector3Int position;


    GameObject gameObj;
    public Tile_Node(GameObject _gameObject, Vector3Int _postion)
    {
        position = _postion;
        gameObject = _gameObject;

    }
    public override int GetCost(Node current)
    {
        //gameObj = null;
        //all hate blue
        //if (((Tile_Node)current).gameObject.tag != "Blue")
        //{
        //    List<Enemy> blues = EnemyManager.Instance.GetEnemyOfType(EnemyType.Blue_prefab);
        //    for (int i = 0; i < blues.Count; i++)
        //    {
        //        if (Vector3.Distance(((Tile_Node)current).position, blues[i].transform.position) < 5)
        //        {
        //            return 1000;
        //        }

        //    }

        //}

        List<Enemy> allEnemies = EnemyManager.Instance.GetAllEnemys();



        for (int i = 0; i < allEnemies.Count; i++)
        {
            if (((Tile_Node)current).gameObject.tag != allEnemies[i].gameObject.tag && Vector3.Distance(((Tile_Node)current).position, allEnemies[i].transform.position) < 5)
            {
                return 1000;
            }

        }
        if (((Tile_Node)current).gameObject.tag =="Player")
        {
            for (int i = 0; i < allEnemies.Count; i++)
            {
                if ( Vector3.Distance(((Tile_Node)current).position, allEnemies[i].transform.position) < 5)
                {
                    return 1000;
                }

            }

        }

        return 1;
    }

    public override int GetId()
    {
        return position.x * 1000 + position.y;
    }

    public override List<Node> GetNeighbours(Node node)
    {
        //Tile tile = new Tile();

        List<Node> neighbours = new List<Node>();

        for (int i = 0; i < delta.GetLength(0); i++)
        {
            if (!GameLinks.gl.colliderTile.GetTile(((Tile_Node)node).position + delta[i]))
            {

                neighbours.Add(new Tile_Node(this.gameObject, ((Tile_Node)node).position + delta[i]));

            }
            // Debug.Log("no");

        }

        return neighbours;
    }

    public override int Heuristic(Node start, Node end)
    {
        return Mathf.Abs(((Tile_Node)start).position.x - ((Tile_Node)end).position.x) + Mathf.Abs(((Tile_Node)start).position.y - ((Tile_Node)end).position.y);
    }

    public override void ShowInClosed()
    {
        ///  throw new System.NotImplementedException();
    }

    public override void ShowInOpen()
    {
        /// throw new System.NotImplementedException();
    }
}
