using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    [SerializeField]
    public float followSpeed = 0.001f;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed);
    }
}
