using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_star;

public class AIMovement : MonoBehaviour
{
    public float speed_1 = 0.3f;
    float speedPerSec = 0;
    GameObject closestPoint;
    void Start()
    {
        speedPerSec = speed_1;
        closestPoint = GetClosestPoint(PointsManager.Instance.points);

    }

    public void SetTheTargetTo(GameObject targetObj)
    {
        closestPoint = targetObj;
    }
    void Update()
    {
        if (speedPerSec < Time.time)
        {
            if (closestPoint == null)
            {
                closestPoint = this.gameObject;
            }
            Vector3 newDirection = Helper.NextVector3_ToChaseThePlayer(this.gameObject, closestPoint);
            Helper.Move(transform, newDirection - Helper.GetGridPositon(this.transform.position));
            speedPerSec = Time.time + speed_1;

        }

    }
    public GameObject GetClosestPoint(List<Point> enemies)
    {
        Point tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Point t in enemies)
        {
            float dist = Vector3.Distance(t.gameObject.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.gameObject;
    }

}
