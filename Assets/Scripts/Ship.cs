using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
    Normal,
    Pirate,
    Cargo,
}

public class Ship : MonoBehaviour
{
    public float speed = 2.0f;
    public ShipType type;
    public GameObject activeIndicator;

    [NonSerialized] public bool pirateShipFailed = false;

    private bool destroyed = false;
    private bool stopped = false;

    [SerializeField] private GameObject destroyedPrefab;
    [SerializeField] private Transform steeringPoint;

    [NonSerialized] public PlayerManager playerOwner;

    private void Update()
    {
        if (stopped)
            return;

        transform.Translate(-Vector3.right * Time.deltaTime * speed);
    }

    public void Moor()
    {
        stopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Wall"))
            return;

        Explode();
    }

    private void Explode()
    {
        if (destroyed)
            return;

        playerOwner.RemoveShip(this);
        GameObject destroyedShip = Instantiate(destroyedPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(destroyedShip, 5.0f);
        destroyed = true;

        if (type != ShipType.Pirate)
            GameManager.instance.RemoveLife();
    }

    public void Unpack()
    {
        if (destroyed)
            return;

        GameManager.instance.AddPoint();
        playerOwner.RemoveShip(this);
        Destroy(gameObject);
        destroyed = true; // Should be unpacked instead of destroyed
    }

    public void Delete()
    {
        if (destroyed)
            return;

        playerOwner.RemoveShip(this);
        Destroy(gameObject);
        destroyed = true; // Should be removed instead of destroyed
    }
}
