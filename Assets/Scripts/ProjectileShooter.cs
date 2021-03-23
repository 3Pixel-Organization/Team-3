﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{

    public Camera cam;
    public GameObject projectile;
    public Transform firePoint;
    public GameObject Gun;

    public float projectileSpeed = 30f;

    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        EventBroker.SetGunColor += SetGunColor;
    }

    private void OnDestroy()
    {
        EventBroker.SetGunColor -= SetGunColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(100);
        }

        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        projectileObj.GetComponentInChildren<ParticleSystem>().startColor = Gun.GetComponent<MeshRenderer>().material.color;
        projectileObj.GetComponent<Projectile>().color = Gun.GetComponent<MeshRenderer>().material.color;
    }

    private void SetGunColor(Material material)
    {
        Gun.GetComponent<MeshRenderer>().material = material;
        Debug.Log("Color of the Gun: " + material.color.ToString()); 
    }
}
