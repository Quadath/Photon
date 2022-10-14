using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;
    public GameObject bulletPrefab;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    
    public void Update()
    {
        if (!photonView.AmOwner) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            photonView.RPC("Fire", RpcTarget.AllViaServer, transform.position + transform.up, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            var click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject turret =
                PhotonNetwork.Instantiate("Turret", new Vector3(click.x, click.y, 0), Quaternion.identity);
            turret.GetComponent<Turret>().owner = PhotonNetwork.LocalPlayer;
        }
        
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(move * Time.deltaTime, Space.World);
    }

    [PunRPC]
    public void Fire(Vector3 pos, Quaternion rotation, PhotonMessageInfo info)
    {
        GameObject bullet = Instantiate(bulletPrefab, pos, rotation);
        bullet.GetComponent<Bullet>().Init(photonView.Owner, transform.up);
    }
}
