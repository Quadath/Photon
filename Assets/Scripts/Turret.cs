using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Turret : MonoBehaviour
{
   public Player owner;
   
   private Transform tower;
   private Transform shoot;
   private Transform target;

   public float radius = 4;
   public float rotSpeed = 1;
   public float shootSpeed = 0.7f;

   private float shootCooldown = 0;

   private void Start()
   {
      tower = transform.GetChild(0);
      shoot = tower.GetChild(0);
   }
   public void SetTarget(Transform tr)
   {
      target = tr;
   }
   private void Update()
   {
      if (target)
      {
         var position = transform.position;
         RaycastHit2D hit = Physics2D.Raycast(position, target.transform.position - position);
         if (hit.transform == target)
         {
            Vector3 vectorToTarget = target.transform.position - position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            tower.rotation = Quaternion.RotateTowards(tower.rotation, q, 70f * rotSpeed * Time.deltaTime);
                
            RaycastHit2D hitForward = Physics2D.Raycast(position, tower.transform.right);
            if (hitForward.transform == target)
            {
               Shoot();
            }
         }
      }

      shootCooldown -= Time.deltaTime;
   }
   void Shoot()
   {
      if (shootCooldown <= 0)
      {
         GameObject bullet = PhotonNetwork.Instantiate("Bullet", shoot.position, tower.rotation);
         bullet.GetComponent<Bullet>().Init(owner, tower.transform.right);
         shootCooldown = 1 / shootSpeed;
      }
   }
}
