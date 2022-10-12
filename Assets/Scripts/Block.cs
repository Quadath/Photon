using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int HP = 3;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (view.IsMine)
            {
                view.RPC("Damage", RpcTarget.AllViaServer);
            }
        }
    }

    [PunRPC]
    void Damage()
    {
        HP--;
        if (HP <= 0)
        {
            if (view.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
