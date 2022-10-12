using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player Owner { get; private set; }
    void Start()
    {
        Destroy(gameObject, 3);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    public void Init(Player owner, Vector3 dir)
    {
        Owner = owner;

        transform.up = dir;

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = dir * 5;
    }
}
