using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    public PlayerManager owner;

    [SerializeField] private GameObject buttonPrompt;

    public void ShowButtonPrompt(bool show)
    {
        buttonPrompt.SetActive(show);
    }
}
