using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameLinks.gl = GameObject.FindObjectOfType<GameLinks>();
        PointsManager.Instance.Initialize();
        EnemyManager.Instance.Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        EnemyManager.Instance.Refresh();
        UIManager.Instance.Refresh();
    }
}
