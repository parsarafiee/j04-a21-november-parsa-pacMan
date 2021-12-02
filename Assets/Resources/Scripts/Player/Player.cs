using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CircleCollider2D playerCollider;
    void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.name);

    }

    void OnCollisionEnter(Collision collision)
    {
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
}
