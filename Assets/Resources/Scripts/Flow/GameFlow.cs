using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameLinks.gl = GameObject.FindObjectOfType<GameLinks>();
        PointsManager.Instance.Initialize();
        EnemyManager.Instance.Initialize();

        Debug.Log(PointsManager.Instance.points.Count);

    }

    // Update is called once per frame
    void Update()
    {
        EnemyManager.Instance.Refresh();
        UIManager.Instance.Refresh();
    }
}
