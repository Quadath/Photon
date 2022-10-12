using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraWork : MonoBehaviourPunCallbacks
{
    private Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
        StartCoroutine("Follow");
    }

    IEnumerator Follow()
    {
        while (true)
        {
            transform.position = new Vector3(target.position.x, target.position.y, -10);
            yield return new WaitForEndOfFrame();
        }
    }
}
