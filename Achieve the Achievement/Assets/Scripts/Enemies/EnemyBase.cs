using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public void KillPlayer()
    {
        RoomManager rm = GetComponentInParent<RoomManager>();
        rm.SpawnPlayer(rm.ActiveRoom.SpawnPoint);
    }
}