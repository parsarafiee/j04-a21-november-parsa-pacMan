using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isAlive=true;

    public  EnemyType type { get; set; }
    public virtual void Initialize()
    {
        isAlive=true;
    }
    public virtual void Movement()
    {


    }

    //public virtual Vector3 GetWorldPostion(Vector3Int pos)
    //{
    //    Grid grid = GameLinks.gl.grid;
    //    Vector3 worldPostion = grid.CellToWorld(pos);
    //    return worldPostion;
    //}
    //public virtual Vector3Int GetGridPositon(Vector3 pos)
    //{
    //    Grid grid = GameLinks.gl.grid;
    //    Vector3Int cellPosition = grid.WorldToCell(pos);
    //    return cellPosition;
    //}
    public virtual void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public virtual void Refresh()
    {
        Movement();

    }

    public virtual void Die()
    {
        EnemyManager.Instance.EnemyDied(this);
        isAlive = false;
    }

    


}
