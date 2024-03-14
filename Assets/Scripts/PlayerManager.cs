using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int playerNumber = 1;

    [SerializeField] private Transform lanternSpawn;
    [SerializeField] private GameObject playerPrefab;

    [NonSerialized] public bool canMoveShips = true;
    [NonSerialized] public bool isNearbyPort = false;
    [NonSerialized] public Port nearbyPort = null;
    [NonSerialized] public bool isNearbyGenerator = false;
    [NonSerialized] public Generator nearbyGenerator;
    [NonSerialized] public bool isInsideLantern = true;
    [NonSerialized] public bool canEnterLantern = false;
 
    private List<Ship> ownedShips = new List<Ship>();
    private Ship currentShip;
    private float walkSpeed = 3.0f;
    private float rotationSpeed = 15.0f;
    private GameObject player;

    private void Update()
    {
        if (isInsideLantern)
        {
            InsideLanternMovement();
        }
        else
        {
            player.GetComponent<PlayerPawn>().ShowButtonPrompt(isNearbyPort || isNearbyGenerator);
            OutsideLanternMovement();
        }
    }

    private void InsideLanternMovement()
    {
        bool aButton = Input.GetButtonDown("A" + playerNumber.ToString());

        if (aButton)
        {
            LeaveLantern();
            return;
        }

        if (currentShip == null || !canMoveShips)
            return;

        float vertical = Input.GetAxis("Vertical" + playerNumber.ToString());

        if (playerNumber == 2)
            vertical = -vertical;

        bool bumperRight = Input.GetButtonDown("RightBumper" + playerNumber.ToString());
        bool bumperLeft = Input.GetButtonDown("LeftBumper" + playerNumber.ToString());

        if (bumperLeft)
        {
            currentShip.activeIndicator.SetActive(false);
            int index = ownedShips.IndexOf(currentShip);
            index -= 1;

            if (index < 0)
                index = ownedShips.Count + index;

            SetAsCurrent(ownedShips[index]);
        }

        if (bumperRight)
        {
            currentShip.activeIndicator.SetActive(false);
            int index = ownedShips.IndexOf(currentShip);
            index = (index + 1) % ownedShips.Count;
            SetAsCurrent(ownedShips[index]);
        }

        currentShip.transform.Rotate(Vector3.up, Time.deltaTime * vertical * rotationSpeed);
    }

    private void OutsideLanternMovement()
    {
        float vertical = Input.GetAxis("Vertical" + playerNumber.ToString());
        float horizontal = Input.GetAxis("Horizontal" + playerNumber.ToString());

        player.transform.Translate(new Vector3(horizontal, 0.0f, vertical) * Time.deltaTime * walkSpeed, Space.World);

        bool aButton = Input.GetButtonDown("A" + playerNumber.ToString());

        if (aButton && canEnterLantern)
        {
            EnterLantern();
            return;
        }

        if (aButton && isNearbyPort)
            nearbyPort.RemoveShip();

        if (aButton && isNearbyGenerator)
            nearbyGenerator.Charge();
    }

    private void LeaveLantern()
    {
        isInsideLantern = false;
        player = Instantiate(playerPrefab, lanternSpawn.position, lanternSpawn.rotation);
        player.GetComponent<PlayerPawn>().owner = this;
    }

    private void EnterLantern()
    {
        isInsideLantern = true;
        Destroy(player);
    }

    private void SetAsCurrent(Ship ship)
    {
        currentShip = ship;
        currentShip.activeIndicator.SetActive(true);
    }

    public void AddShip(Ship ship)
    {
        if (currentShip == null)
        {
            SetAsCurrent(ship);
        }

        ownedShips.Add(ship);
        ship.playerOwner = this;
    }

    public void RemoveShip(Ship ship)
    {
        ownedShips.Remove(ship);

        if (currentShip == ship)
        {
            if (ownedShips.Count == 0)
            {
                currentShip = null;
                return;
            }

            int index = ownedShips.IndexOf(currentShip);
            index = (index + 1) % ownedShips.Count;
            SetAsCurrent(ownedShips[index]);
        }
    }
}
