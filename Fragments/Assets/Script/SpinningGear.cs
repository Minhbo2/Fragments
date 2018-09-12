using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningGear : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 direction;

	void Update () {
        transform.Rotate(direction * Time.deltaTime * speed);		
	}
}
