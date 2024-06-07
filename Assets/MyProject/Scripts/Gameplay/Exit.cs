using System;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public static event Action OnExit;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            OnExit?.Invoke();
    }
}
