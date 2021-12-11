using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_star;

public class Helper : MonoBehaviour
{
    public static Vector3 GetWorldPostion(Vector3Int pos)
    {
        Grid grid = GameLinks.gl.grid;
        Vector3 worldPostion = grid.CellToWorld(pos);
        return worldPostion;
    }
    public static Vector3Int GetGridPositon(Vector3 pos)
    {
        Grid grid = GameLinks.gl.grid;
        Vector3Int cellPosition = grid.WorldToCell(pos);
        return cellPosition;
    }
    public static void Move(Transform transform, Vector2 direction)
    {
        if (CanMove(transform, direction))
        {
            transform.position += (Vector3)direction;
        }

    }
    public static bool CanMove(Transform transform, Vector2 direction)
    {
        Vector3Int gridPositon = GameLinks.gl.groundTile.WorldToCell(transform.position + (Vector3)direction);
        if (!GameLinks.gl.groundTile.HasTile(gridPositon) || GameLinks.gl.colliderTile.HasTile(gridPositon))
        {
            return false;
        }
        return true;
    }
    public static Vector3 NextVector3_ToChaseThePlayer(GameObject chaser, GameObject objectToChase)
    {
        Vector3 nextPo = Vector3.zero;
        Vector3Int chaser_PositonToCell = GetGridPositon(chaser.transform.position);
        Vector3Int objectToChase_PostionToCell = GetGridPositon(objectToChase.transform.position);

        Tile_Node currentNode = new Tile_Node(chaser, chaser_PositonToCell);
        Tile_Node destinationNode = new Tile_Node(objectToChase, objectToChase_PostionToCell);
        Tile_Node nextNode = (Tile_Node)new Astar().GiveNextNodeOfPath(currentNode, destinationNode);

        nextPo.x = nextNode.position.x;
        nextPo.y = nextNode.position.y;

        return nextPo;
        // astar.SearchPath()
    }

    public static GameObject GetTheBestPositonToRun(GameObject currentPositon)
    {
        GameObject pos= null;

            float minDist = 0;
            Vector3 currentPos = currentPositon.transform.position;
            foreach (GameObject t in GameLinks.gl.GameObjectToScape)
            {
                float dist = Vector3.Distance(t.gameObject.transform.position, currentPos);
                if (dist > minDist)
                {
                    pos = t;
                    minDist = dist;
                }
            }

        return pos;
    }
    //public static List<Tile_Node> CreatePathFromNode(Tile_Node lastTile)
    //{



    //}
}
