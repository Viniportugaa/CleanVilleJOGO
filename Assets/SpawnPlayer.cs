using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;
    public GameObject UICanvas;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private bool player;
    private bool player2;

    private void Start()
    {
        player = true;
        player2 = true;
    }

    private void Update()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
            //if (player)
            //{
                //if (Input.GetButton("Fire2"))
                //{
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, minY));
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        player = false;
                //}
            //}
        //}
        /*/else
        {
            if (player2)
            {
                if (Input.GetButton("Fire2"))   
                {
                    Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, minY));
                    PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
                    player2 = false;
                }
            }
        }
        /*/
    }
}
