using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpots : MonoBehaviour
{
    public Transform[] moveSpots;

    void Start() {
        moveSpots = new Transform[transform.childCount];

        int count = 0;
		foreach (Transform child in transform) {
			moveSpots[count] = child.transform;
			count++;
		}
    }
}
