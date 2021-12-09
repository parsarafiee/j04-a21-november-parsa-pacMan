using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT_lib;
using System;

public class BTTest : MonoBehaviour
{
    AIMovement ai;
    BT bt_ScapeFromAnamy;
    BT bt_Root;
    Vector2 directionToRun;
    void Start()
    {
        ai=this.gameObject.GetComponent<AIMovement>();
        bt_ScapeFromAnamy = new BT(NODE_TYPE.SEQUENCE, new BT(this.CloseTpEnemy), new BT(this.Run));
        bt_Root = new BT(NODE_TYPE.SELECTOR, bt_ScapeFromAnamy, new BT(this.GetAllPoints));
    }

    private BT_VALUE GetAllPoints()
    {
        ai.SetTheTargetTo(ai.GetClosestEnemy(PointsManager.Instance.points));
        return BT_VALUE.SUCCESS;

    }

    private void Update()
    {
        bt_Root.Evaluate();

    }

    public BT_VALUE CloseTpEnemy()
    {
        BT_VALUE value = BT_VALUE.FAIL;


        foreach (var item in EnemyManager.Instance.enamyList)
        {
            if (Vector3.Distance(item.gameObject.transform.position,this.transform.position)<4)
            {

            value = BT_VALUE.SUCCESS;
            }

        }

        return value;
    }

    public BT_VALUE Run()
    {
        ai.SetTheTargetTo(this.gameObject);
        Debug.Log("i have to run");
        return BT_VALUE.SUCCESS;
    }
}
