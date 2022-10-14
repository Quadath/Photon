using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player Owner { get; private set; }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PhotonNetwork.Destroy(gameObject);
    }
    public void Init(Player owner, Vector3 dir)
    {
        Owner = owner;
        transform.up = dir;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * 5;
    }
}
