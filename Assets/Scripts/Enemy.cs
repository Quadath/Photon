using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private PhotonView _view;
    private float hp = 3;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Damage(float dmg, Player player)
    {
        hp -= dmg;
        if (hp <= 0)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if(PhotonNetwork.IsMasterClient) _view.RPC("Damage", RpcTarget.AllViaServer, 1f, other.gameObject.GetComponent<Bullet>().Owner);
        }
    }

    private void Update()
    {
        if(target && _view.IsMine) transform.position = Vector2.MoveTowards(transform.position, target.position, 0.01f);
    }
}
