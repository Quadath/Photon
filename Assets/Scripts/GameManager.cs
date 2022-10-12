using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayer;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        LocalPlayer = PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
        LocalPlayer.GetComponent<SpriteRenderer>().color = Color.cyan;
        LocalPlayer.name = "Player";
    }

    public void Start()
    {
       
    }
}
