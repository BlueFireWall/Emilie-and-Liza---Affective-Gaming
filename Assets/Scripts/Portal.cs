using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform secondPortal;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerModel")) {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player) {
        player.parent.position = secondPortal.position;
    }
}
