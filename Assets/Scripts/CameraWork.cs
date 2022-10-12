using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraWork : MonoBehaviourPunCallbacks
{
    private Transform target;
    private void Start()
    {
        target = GameManager.LocalPlayer.transform;
    }

    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
