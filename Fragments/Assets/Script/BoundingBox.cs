using UnityEngine;

public class BoundingBox : MonoBehaviour {

    [SerializeField]
    private Transform playerStart;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.transform.position = playerStart.position;
    }
}
