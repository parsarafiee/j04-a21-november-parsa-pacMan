using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_star;

public class BlueMovement : MonoBehaviour
{
    public float speed_1 = 0.3f;
    float speedPerSec = 0;

    PlayerController playerController;

    GameObject target;

    List<Vector2> dir;

    int randomNumberForDirection;
    Vector2 newDirection;
    GameObject objectToReact;
    void Start()
    {
        objectToReact = GameLinks.gl.player;
        speedPerSec = speed_1;
        dir = new List<Vector2>() { Vector2.right, Vector2.down, Vector2.left, Vector2.up };

        playerController = FindObjectOfType<PlayerController>();
        target = playerController.gameObject;
    }

    void Update()
    {
        if (Vector3.Distance(objectToReact.transform.position, transform.position) < 10)
        {
            if (speedPerSec < Time.time)
            {

                Vector3 newDirection = Helper.NextVector3_ToChaseThePlayer(this.gameObject, objectToReact);
                Helper.Move(transform, newDirection-Helper.GetGridPositon( this.transform.position));
                speedPerSec = Time.time + speed_1;
            }
        }
        else
        {
            if (speedPerSec < Time.time)
            {
                newDirection = dir[randomNumberForDirection];
                if (!Helper.CanMove(transform, newDirection))
                {
                    randomNumberForDirection = Random.Range(0, 4);
                }
                Helper.Move(transform, newDirection);
                speedPerSec = Time.time + speed_1;
            }

        }

    }
}
