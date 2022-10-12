using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(target && _view.IsMine) transform.position = Vector2.MoveTowards(transform.position, target.position, 0.01f);
    }
}
