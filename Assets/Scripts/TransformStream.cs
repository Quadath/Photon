using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TransformStream : MonoBehaviourPun, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            transform.position = (Vector3) stream.ReceiveNext();
        }
    }
}
