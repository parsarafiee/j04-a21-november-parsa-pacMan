using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private void OnDestroy()
    {
        PointsManager.Instance.points.Remove(this);
        PointsManager.Instance.pointsToRemove++;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Destroy(this.gameObject);
    }  
}
