using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    GameObject player;
    void Start()
    {
       player = GameLinks.gl.player; 
    }
    private void OnDestroy()
    {
        PointsManager.Instance.pointsToRemove++;
        PointsManager.Instance.points.Remove(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 0.7)
        {
            GameObject.Destroy(this.gameObject);

        }


    }

}
