using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{

    #region Singleton
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UIManager();
            return instance;
        }
    }

    private static UIManager instance;

    #endregion

    public int playerPoints=0;

    public void Initialize()
    {
        GameLinks.gl.points.text = playerPoints.ToString();
    }
    public void Refresh()
    {
        GameLinks.gl.points.text= PointsManager.Instance.pointsToRemove.ToString();
    }
}
