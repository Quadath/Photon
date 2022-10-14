using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    private Turret turr;
    private List<Transform> enemies = new List<Transform>();
    private float radius => turr.radius;
    public Transform tower;
    
    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient) Destroy(this);
        turr = transform.parent.GetComponent<Turret>();
        GetComponent<CircleCollider2D>().radius = radius;
    }

    private void Update()
    {
        var smallestDist = 10f;
        Transform closestEnemy = null;
        if (enemies.Count > 0)
        {
            for(int index = 0; index < enemies.Count; index++)
            {
                var enemy = enemies[index];
                if (!enemy)
                {
                    enemies.RemoveAt(index);
                    continue;
                }
                var position = transform.position;
                var dist = Vector2.Distance(position, enemy.position);
                
                RaycastHit2D hit = Physics2D.Raycast(position, enemy.position - position);
                
                if(hit.transform != enemy)
                    continue;

                if (dist > (radius + .25f))
                {
                    enemies.Remove(enemy);
                }
                else if (dist < smallestDist)
                {
                    smallestDist = dist;
                    closestEnemy = enemy;
                }
            }
            if (closestEnemy != null)
                turr.SetTarget(closestEnemy);
        }
        else
        {
            turr.SetTarget(null);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
            if (!enemies.Contains(other.transform))
            {
                enemies.Add(other.transform);
            }
    }
}
