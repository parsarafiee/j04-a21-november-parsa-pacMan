using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameLinks : MonoBehaviour
{
    public static GameLinks gl;

    public Collider2D worldBounds;
    public Transform spawnBule;
    public Transform spawnRed;
    public Transform spawnPink;
    public Transform spawnYellow;
    public Transform spawnLocationParent;
    public Grid grid;

    public GameObject player;

    public Tilemap groundTile;
    public Tilemap colliderTile;
    public Grid basicGrid;

    public TileBase wallTile;

    public List<GameObject> GameObjectToScape;
    public Text points;

    public GameObject blue;

    private void Start()
    {
  
    }
}
