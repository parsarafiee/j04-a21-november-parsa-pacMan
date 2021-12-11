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
                Vector3 worldPositon = new Vector3(i, j, 0);
                Vector3Int cellPosistion = Helper.GetGridPositon(worldPositon);
                if (!GameLinks.gl.colliderTile.GetTile(cellPosistion))
                {
                    Point newPoint = GameObject.Instantiate(pointsResource, pointsParent).GetComponent<Point>();
                    newPoint.name = "Point";
                    newPoint.transform.position = worldPositon + new Vector3(0.5f, 0.5f, 0);

                    points.Add(newPoint);
                }
            }
        }
        MakeRedPills();
    }

    public void MakeRedPills()
    {
        int[] randomRedpills = new int[GameLinks.gl.numberOfRedPills];
        for (int i = 0; i < randomRedpills.Length; i++)
        {
            randomRedpills[i] = Random.Range(0, points.Count);
            points[randomRedpills[i]].GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }



}
