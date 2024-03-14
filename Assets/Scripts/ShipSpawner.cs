using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private PlayerManager playerOwner;
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private GameObject pirateShipPrefab;
    [SerializeField] private GameObject cargoShipPrefab;
    [SerializeField] private List<Transform> randomPositions = new List<Transform>();
    [SerializeField] private List<float> spawnTimes = new List<float>();
    [SerializeField] private float pirateShipChance = 0.1f;
    [SerializeField] private float cargoShipChance = 0.2f;

    private int lastSpawned = 0;
    private float currentTime = 0.0f;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (lastSpawned >= spawnTimes.Count)
        {
            if (currentTime > spawnTimes[lastSpawned - 1] + 12.0f)
            {
                lastSpawned = 0;
                currentTime = 0.0f;
            }

            return;
        }

        if (currentTime < spawnTimes[lastSpawned])
            return;

        Spawn();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    private void Spawn()
    {
        int randomPosition = UnityEngine.Random.Range(0, randomPositions.Count);

        float randomRotationOffset = UnityEngine.Random.Range(-8.0f, 8.0f);

        float randomFloat = UnityEngine.Random.Range(0.0f, 1.0f);

        GameObject prefab = shipPrefab;
        if (randomFloat < pirateShipChance)
            prefab = pirateShipPrefab;
        else if (randomFloat >= pirateShipChance && randomFloat < pirateShipChance + cargoShipChance)
            prefab = cargoShipPrefab;

        GameObject go = Instantiate(prefab, randomPositions[randomPosition].position, randomPositions[randomPosition].rotation);
        Vector3 rotation = go.transform.eulerAngles;
        rotation.y += randomRotationOffset;
        go.transform.eulerAngles = rotation;
 
        lastSpawned++;

        Ship ship = go.GetComponent<Ship>();
        playerOwner.AddShip(ship);
    }
}
