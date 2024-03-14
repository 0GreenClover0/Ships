using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedShip : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 0.85f);
    }
}
