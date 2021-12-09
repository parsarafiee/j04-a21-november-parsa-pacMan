using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT_lib;

public class BTTest : MonoBehaviour
{
    public Transform circle;
    public Transform square;
    public Rigidbody2D rbCircle;
    public Rigidbody2D rbsQuare;


    BT bt_move;
    void Start()
    {
     //   bt_move = new BT(NODE_TYPE.SEQUENCE, new BT(this.CloseTpEnemy), new BT(this.Run));

        rbsQuare.AddForce(new Vector2(1f, 0),ForceMode2D.Impulse);
        Debug.Log(rbsQuare.velocity);
    }

    private void Update()
    {
        rbCircle.AddForce(new Vector2(1f, 0));
        Debug.Log(rbCircle.velocity);


    }

    public BT_VALUE CloseTpEnemy()
    {
        BT_VALUE value = BT_VALUE.FAIL;

        
        //        rbCircle.AddForce(new Vector2(f, 0), ForceMode2D.Impulse);

        if (Vector3.Distance(circle.position, square.position) < 5)
        {
            value = BT_VALUE.SUCCESS;
        }
        return value;
    }

    public BT_VALUE Run()
    {
      //  rbCircle.AddForce(new Vector2(1f, 0));

        //Debug.Log("hey");
        return BT_VALUE.SUCCESS;
    }
}
