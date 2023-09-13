using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject whole;
    [SerializeField] private GameObject slice;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juice;

    private void Awake()
    {
        fruitCollider = GetComponent<Collider>();
        fruitRigidbody = GetComponent<Rigidbody>();
        juice = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            SliceFruit(blade.direction,blade.transform.position,blade.sliceForce);
            GameManager.Instance?.ScoreUpdate();
        }
    }

    private void SliceFruit(Vector3 direction, Vector3 position, float force)
    {
        whole.SetActive(false);
        slice.SetActive(true);

        fruitCollider.enabled = false;
        juice.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slice.transform.rotation = Quaternion.Euler(0f,0f,angle);

        Rigidbody[] slices = slice.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody sli in slices)
        {
            sli.velocity = fruitRigidbody.velocity;
            sli.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }

    }
}
