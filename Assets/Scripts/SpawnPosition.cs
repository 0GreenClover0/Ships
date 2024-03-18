using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public bool CanSpawn()
    {
        Collider[] overlappingColliders = Physics.OverlapBox(boxCollider.transform.TransformPoint(boxCollider.center), boxCollider.size / 2.0f);

        foreach (var collider in overlappingColliders)
        {
            if (collider.attachedRigidbody != null && collider.attachedRigidbody.gameObject.GetComponent<Ship>() != null)
                return false;
        }

        return true;
    }
}
