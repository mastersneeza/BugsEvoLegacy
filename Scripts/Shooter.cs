using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    public Transform firePoint;
    public GameObject projectilePrefab;

    public void Shoot() {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

}
