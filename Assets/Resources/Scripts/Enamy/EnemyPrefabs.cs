using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabs :MonoBehaviour
{

    private void Awake()
    {
        
    }
    #region Singleton
    private static EnemyPrefabs instance;

    private EnemyPrefabs() { }
    public static EnemyPrefabs Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new EnemyPrefabs();

            }
            return instance;
        }
    }



    #endregion
    public GameObject red;
    public GameObject blue;
    public GameObject yellow;
    public GameObject pink;



}
