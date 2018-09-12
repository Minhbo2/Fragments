using UnityEngine;

public class OnTriggerMessage : MonoBehaviour {

    [SerializeField]
    private string objectMessage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            UIManagerSet.Inst.DisplayText(objectMessage);
    }
}
