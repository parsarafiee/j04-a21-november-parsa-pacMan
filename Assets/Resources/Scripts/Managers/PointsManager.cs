using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager
{

    #region Singleton
    public static PointsManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PointsManager();
            return instance;
        }
    }

    private static PointsManager instance;

    private PointsManager() { }
    #endregion
    public List<Point> points;
    public int pointsToRemove;


    GameObject pointsResource;

    Transform pointsParent;
    // Start is called before the first frame update
    public void Initialize()
    {
        points = new List<Point>();
        pointsParent = new GameObject("Zombie Parent").transform;
        pointsResource = Resources.Load<GameObject>("Prefabs/Point");
        SpawnPoints();
    }
    public void SpawnPoints()
    {
        for (int i = -17; i < 18; i++)
        {
            for (int j = -10; j < 10; j++)
            {
                
                Vector3 worldPositon= new Vector3(i,j,0);
                Vector3Int cellPosistion =Helper.GetGridPositon(worldPositon);
                if (!GameLinks.gl.colliderTile.GetTile(cellPosistion))
                {
                    Point newZombie = GameObject.Instantiate(pointsResource, pointsParent).GetComponent<Point>();
                    newZombie.name = "Point";
                    newZombie.transform.position = worldPositon + new Vector3(0.5f,0.5f,0);
                    points.Add(newZombie);

                }


            }

        }

    }


    //public void GameStart()
    //{
    //    Vector2 validSpot = new Vector2();
    //    for (int i = 0; i < numberOfPoints; i++)
    //        if (GetValidSpot(out validSpot))
    //            SpawnPoints(validSpot);

    //}
    //private bool GetValidSpot(out Vector2 validSpot)
    //{
    //    Bounds wbounds = GameLinks.gl.worldBounds.bounds;


    //    for (int i = 0; i < VALIDSPOT_ATTEMPTS_MAX; i++)
    //    {
    //        Vector2 spotInMap = new Vector2(Random.Range(-wbounds.extents.x, wbounds.extents.x), Random.Range(-wbounds.extents.y, wbounds.extents.y)) + (Vector2)wbounds.center;

    //        if (!Physics2D.OverlapCapsule(spotInMap, new Vector2(2, 2), CapsuleDirection2D.Vertical, 0, LayerMask.GetMask("Unit", "Wall")))
    //        {
    //            validSpot = spotInMap;
    //            return true;
    //        }
    //    }
    //    validSpot = new Vector2();
    //    return false;
    //}

}
