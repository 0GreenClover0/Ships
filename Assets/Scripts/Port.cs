using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Port : MonoBehaviour
{
    private List<Ship> ships = new List<Ship>();

    private void OnTriggerEnter(Collider other)
    {
        ShipEnter(other);
        PlayerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerExit(other);
    }

    private void Update()
    {
        for (int i = ships.Count - 1; i >= 0; --i)
        {
            if (ships[i] == null)
                ships.RemoveAt(i);
        }
    }

    private void ShipEnter(Collider other)
    {
        Ship ship = other.gameObject.GetComponent<Ship>();

        if (ship == null || ship.type != ShipType.Cargo)
            return;

        ship.Moor();
        ships.Add(ship);
    }

    private void PlayerEnter(Collider other)
    {
        if (other.attachedRigidbody == null)
            return;

        PlayerPawn playerPawn = other.attachedRigidbody.gameObject.GetComponent<PlayerPawn>();

        if (playerPawn == null)
            return;

        playerPawn.owner.isNearbyPort = true;
        playerPawn.owner.nearbyPort = this;
    }

    private void PlayerExit(Collider other)
    {
        if (other.attachedRigidbody == null)
            return;

        PlayerPawn playerPawn = other.attachedRigidbody.gameObject.GetComponent<PlayerPawn>();

        if (playerPawn == null)
            return;

        playerPawn.owner.isNearbyPort = false;
        playerPawn.owner.nearbyPort = null;
    }

    public void RemoveShip()
    {
        Ship ship = ships.FirstOrDefault(x => x != null);
        if (ship == null)
            return;

        ship.Unpack();
    }
}
