using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Player player;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            player.AddScore(-1);
            PhotonNetwork.Destroy(other.gameObject);
        }
    }
}
