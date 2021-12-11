using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_star;

public class AIMovement : MonoBehaviour
{
    public float speed_1 = 0.3f;
    float speedPerSec = 0;
    GameObject closestPoint;
    SpriteRenderer ai_spriteRenderer;

    public bool pacManTookARedPill;

    float timerForTheRedPill=0;
    void Start()
    {
        speedPerSec = speed_1;
        closestPoint = GetClosestPoint(PointsManager.Instance.points);
        ai_spriteRenderer =GetComponent<SpriteRenderer>();
    }

    public void SetTheTargetTo(GameObject targetObj)
    {
        closestPoint = targetObj;
    }
    void Update()
    {
        timerForTheRedPill += Time.deltaTime;
        ai_spriteRenderer.material.color = pacManTookARedPill ? ai_spriteRenderer.material.color = Color.red: ai_spriteRenderer.material.color = Color.white;
        if (pacManTookARedPill && timerForTheRedPill >GameLinks.gl.timerForRedPill)
        {
            pacManTookARedPill = false;
        }

        if (speedPerSec < Time.time)
        {
            if (closestPoint == null)
            {
                closestPoint = this.gameObject;
            }
            Vector3 newDirection = Helper.NextVector3_ToChaseThePlayer(this.gameObject, closestPoint);
            Helper.Move(transform, newDirection - Helper.GetGridPositon(this.transform.position));
            speedPerSec = Time.time + speed_1;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Point"&&collision.gameObject.GetComponent<SpriteRenderer>().material.color==Color.red)
        {
            pacManTookARedPill = true;
            timerForTheRedPill=0;
        }

        if(collision.gameObject.layer==7&& ai_spriteRenderer.material.color ==Color.red)
        {
            GameObject.Destroy(collision.gameObject);
            
        }
    }
    public GameObject GetClosestPoint(List<Point> enemies)
    {        
        Point tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Point t in enemies)
        {
            float dist = Vector3.Distance(t.gameObject.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.gameObject;
    }
    public GameObject GetClosestEnemy(List<Enemy> enemies)
    {
        Enemy tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Enemy t in enemies)
        {
            float dist = Vector3.Distance(t.gameObject.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.gameObject;
    }



}
