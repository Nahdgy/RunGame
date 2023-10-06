using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Playercam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    [SerializeField]
    private Player player;
    private void Start()
    {
        player = GameObject.FindAnyObjectByType<Player>();
        playerCam.Follow = player.transform;
    }
}
