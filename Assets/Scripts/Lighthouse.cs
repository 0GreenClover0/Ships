using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lighthouse : MonoBehaviour
{
    public PlayerManager playerManager;
    public float power = 100.0f;

    [SerializeField] private Slider slider;
    [SerializeField] private GameObject buttonPrompt;
    [SerializeField] private float powerDrainage = 4.0f;

    private void Update()
    {
        if (power <= 0.0f)
            return;

        power -= Time.deltaTime * powerDrainage;

        slider.value = Mathf.Clamp(power / 100.0f, 0.0f, 1.0f);

        playerManager.canMoveShips = power > 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        buttonPrompt.SetActive(true);
        other.attachedRigidbody.GetComponent<PlayerPawn>().owner.canEnterLantern = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        buttonPrompt.SetActive(false);
        other.attachedRigidbody.GetComponent<PlayerPawn>().owner.canEnterLantern = false;
    }
}
