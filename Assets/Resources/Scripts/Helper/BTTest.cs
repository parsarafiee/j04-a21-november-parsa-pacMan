using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT_lib;
using System;

public class BTTest : MonoBehaviour
{
    public float mindistanceToAI=5;
    AIMovement ai;
    BT bt_ScapeFromEnamy;
    BT bt_Root;

    BT bt_EnemyScapeFromPlayer;

    BT bt_SelectorToRunOrAttack;


    Vector2 directionToRun;
    void Start()
    {
        ai=this.gameObject.GetComponent<AIMovement>();
        bt_EnemyScapeFromPlayer = new BT(NODE_TYPE.SEQUENCE, new BT(this.PlayerIsCloseWithRedPill), new BT(this.PlayerChaseEnemy));
        bt_SelectorToRunOrAttack = new BT(NODE_TYPE.SELECTOR, bt_EnemyScapeFromPlayer, new BT(this.Run));
        bt_ScapeFromEnamy = new BT(NODE_TYPE.SEQUENCE, new BT(this.CloseTpEnemy), bt_SelectorToRunOrAttack);
        bt_Root = new BT(NODE_TYPE.SELECTOR, bt_ScapeFromEnamy, new BT(this.GetAllPoints));
    }

    private BT_VALUE GetAllPoints()
    {
        ai.SetTheTargetTo(ai.GetClosestPoint(PointsManager.Instance.points));
        return BT_VALUE.SUCCESS;

    }

    private void Update()
    {
        bt_Root.Evaluate();

    }

    public BT_VALUE PlayerIsCloseWithRedPill()
    {
        BT_VALUE value = BT_VALUE.FAIL;
        if (GameLinks.gl.player.GetComponent<AIMovement>().pacManTookARedPill == true )
        {
            foreach (var item in EnemyManager.Instance.enamyList)
            {
                if (Vector3.Distance(item.gameObject.transform.position, this.transform.position) < mindistanceToAI)
                {
                    value = BT_VALUE.SUCCESS;
                }
            }
        }
       

        return value;
    }
    public BT_VALUE PlayerChaseEnemy()
    {
        ai.SetTheTargetTo(ai.GetClosestEnemy(EnemyManager.Instance.enamyList));
        return BT_VALUE.SUCCESS;
    }

    public BT_VALUE CloseTpEnemy()
    {
        BT_VALUE value = BT_VALUE.FAIL;
        foreach (var item in EnemyManager.Instance.enamyList)
        {
            if (Vector3.Distance(item.gameObject.transform.position,this.transform.position)<mindistanceToAI)
            {
            value = BT_VALUE.SUCCESS;
            }
        }
        return value;
    }
    public BT_VALUE Run()
    {
        GameObject locationToRun = Helper.PlayerGetTheBestPositonToRun(this.gameObject);
        ai.SetTheTargetTo(locationToRun);
       // Debug.Log("i have to run");
        return BT_VALUE.SUCCESS;
    }
}
