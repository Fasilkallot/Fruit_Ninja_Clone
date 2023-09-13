
using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static Action gameOver;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOver?.Invoke();
        }
    }
}
