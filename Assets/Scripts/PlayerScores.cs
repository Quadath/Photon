using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerScores : MonoBehaviourPunCallbacks
{
    public GameObject textPrefab;

    private Dictionary<int, GameObject> playerScores;

    public void Awake()
    {
        playerScores = new Dictionary<int, GameObject>();

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject scoreText = Instantiate(textPrefab, transform, true);
            scoreText.GetComponent<TextMeshProUGUI>().SetText($"{p.NickName} Score: {p.GetScore()}");
            playerScores.Add(p.ActorNumber, scoreText);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable hashtable)
    {
        GameObject scoteText;
        if (playerScores.TryGetValue(targetPlayer.ActorNumber, out scoteText))
        {
            scoteText.GetComponent<TextMeshProUGUI>().SetText($"{targetPlayer.NickName} Score: {targetPlayer.GetScore()}");
        }
    }
}
