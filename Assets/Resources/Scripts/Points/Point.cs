using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    GameObject player;
    void Start()
    {
       player = FindObjectOfType<Player>().gameObject; 
    }
    private void OnDestroy()
    {
        PointsManager.Instance.pointsToRemove++;
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
