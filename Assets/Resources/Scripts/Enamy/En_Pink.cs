using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Pink :Enemy
{
    public override void Initialize()
    {
        Vector3 pos = GameLinks.gl.spawnPink.position;
        type = EnemyType.Pink_prefab;

        base.SetPosition(pos);
        base.Initialize();


    }
    public override void Refresh()
    {
        base.Refresh();
    }
    public override void Die()
    {
        //Dont want to call rootEnemy die, will destroy the rootNode system!
        EnemyManager.Instance.EnemyDied(this);
        isAlive = false;
        GameObject.Destroy(gameObject);
    }
}
