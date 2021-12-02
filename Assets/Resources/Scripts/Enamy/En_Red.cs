using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Red : Enemy
{
    public override void Initialize()
    {
        Vector3 pos = GameLinks.gl.spawnRed.position;
        type = EnemyType.Red_prefab;

        base.SetPosition( pos);
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
        //base.Die();
    }
}
