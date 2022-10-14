using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayer;
    private static PhotonView localView;

    public List<GameObject> players;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        LocalPlayer = PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
        PhotonNetwork.Instantiate("Portal", new Vector3(-7, 6 * (PhotonNetwork.LocalPlayer.ActorNumber - 1)),
            Quaternion.identity).GetComponent<Portal>().player = PhotonNetwork.LocalPlayer;
        localView = LocalPlayer.GetComponent<PhotonView>();
        LocalPlayer.GetComponent<SpriteRenderer>().color = Color.white;
        LocalPlayer.name = "Player";
        LocalPlayer.transform.position = new Vector3(0, 5 * (PhotonNetwork.LocalPlayer.ActorNumber - 1), 0);
        FindObjectOfType<CameraWork>().SetTarget(LocalPlayer.transform);
        StartCoroutine("CheckAllPlayersConnected");

        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 20;
    }
    private IEnumerator CheckAllPlayersConnected()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Player").Length == (int)PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine("SpawnEnemies");
        }
        
        foreach (var p in GameObject.FindObjectsOfType<PlayerController>())
        {
            players.Add(p.gameObject);
            players = players.OrderBy(p => p.GetComponent<PhotonView>().Owner.ActorNumber).ToList();
        }
    }
    private IEnumerator SpawnEnemies()
    {
        int p = 0;
        while (true)
        {
            yield return new WaitForSeconds(4);
            PhotonNetwork.Instantiate("Enemy", new Vector3(4, 5 * p, 0), Quaternion.identity)
                .GetComponent<Enemy>().target = players[p].transform;
            p++;
            if (p >= PhotonNetwork.CurrentRoom.PlayerCount) p = 0;
        }
    }
}
