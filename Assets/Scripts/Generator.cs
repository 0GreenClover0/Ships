using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private Lighthouse lighthouse;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        PlayerPawn playerPawn = other.attachedRigidbody.GetComponent<PlayerPawn>();
        if (playerPawn == null)
            return;

        playerPawn.owner.isNearbyGenerator = true;
        playerPawn.owner.nearbyGenerator = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        PlayerPawn playerPawn = other.attachedRigidbody.GetComponent<PlayerPawn>();
        if (playerPawn == null)
            return;

        playerPawn.owner.isNearbyGenerator = false;
        playerPawn.owner.nearbyGenerator = null;
    }

    public void Charge()
    {
        lighthouse.power += 5.5f;
    }
}
