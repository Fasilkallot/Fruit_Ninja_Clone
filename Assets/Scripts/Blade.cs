
using System;
using System.Reflection;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Collider bladeCollider;
    private Camera mainCam;
    private TrailRenderer bladeTrail;
    public Vector3 direction {  get; private set; }
    public float sliceForce = 5f;
    private bool isSlicing;
    [SerializeField] private float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCam = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
        GameManager.Instance.blade = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartSlicing();
        else if (Input.GetMouseButtonUp(0)) StopSlicing();
        else if(isSlicing) ContinueSlicing();
    }
    private void OnEnable()
    {
        StopSlicing ();
    }
    private void OnDisable()
    {
        StopSlicing ();
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
        
    }

    private void StopSlicing()
    {
        isSlicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;

        isSlicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }
}
