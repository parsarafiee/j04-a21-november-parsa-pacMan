using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType { Blue_prefab, Pink_prefab, Red_prefab, Yellow_prefab }

public class EnemyManager
{

    #region Singleton
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EnemyManager();
            return instance;
        }
    }

    private static EnemyManager instance;

    private EnemyManager() { }
    #endregion

    Transform enemyParent;
    public Stack<Enemy> toRemove;
    public List<Enemy> enamyList;
    public Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs

    public void Initialize()
    {
        toRemove = new Stack<Enemy>();
        enamyList = new List<Enemy>();
        enemyParent = new GameObject("EnemyParent").transform;
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
        SpawnInitial();
    }

    public List<Enemy> GetEnemyOfType(EnemyType type)
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Enemy enemy in enamyList)
        {
            if (enemy.type == type )
            {
                enemies.Add(enemy); 
            }
        }

        return enemies;
    }
    public List<Enemy> GetAllEnemys()
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Enemy enemy in enamyList)
        {

                enemies.Add(enemy);
        }

        return enemies;
    }
    //public GameObject GetEnemy()
    //{
    //    toAdd.
    //}

    public void SpawnInitial()
    {
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            SpawnEnemy(etype);

        }




    }
    public void Refresh()
    {
        foreach (Enemy e in enamyList)
            if (e.isAlive)
                e.Refresh();
    }
    public void EnemyDied(Enemy enemyDied)
    {
        toRemove.Push(enemyDied);
        enamyList.Remove(enemyDied);

    }

    public Enemy SpawnEnemy(EnemyType eType)
    {
        GameObject newEnemy = GameObject.Instantiate(enemyPrefabDict[eType]);       //create from prefab
        Enemy e = newEnemy.GetComponent<Enemy>();   //get the enemy component on the newly created obj
        e.Initialize();               //initialize the enemy
        enamyList.Add(e);                       //add to update list
        return e;
    }


}
