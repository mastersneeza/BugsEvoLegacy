using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericSpawner : MonoBehaviour
{
    public GameObject[] items;
    public float defaultTime = 5f;

    private Vector2 itemPos;
    public Bounds bounds;

    private void Start() {
        bounds = new Bounds(transform.position, transform.localScale);
        InvokeRepeating("SpawnItem", defaultTime, defaultTime);
    }

    private void SpawnItem() {
        float xCoord, yCoord;
        xCoord = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        yCoord = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        itemPos = new Vector2(xCoord, yCoord);
        Instantiate(items[UnityEngine.Random.Range(0, items.Length)], itemPos, Quaternion.identity);
    }
}
