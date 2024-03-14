using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ship>(out Ship ship))
        {
            if (ship.type == ShipType.Pirate && !ship.pirateShipFailed)
            {
                GameManager.instance.RemoveLife();
                ship.pirateShipFailed = true;
            }
            else if (ship.type != ShipType.Pirate)
            {
                ship.Delete();
            }
        }
    }
}
