using UnityEngine;


//TODO: make this base anim class more generic
// create other animation states 
public class AnimObj : MonoBehaviour {

    protected Vector3 initPos;
    protected Vector3 newPos;

    public float speed;

    public float yPos;

    [SerializeField]
    protected int layerIndex;

    float acceptableRange = 0.01f;

    public virtual void OnEnable()
    {
        this.gameObject.layer = layerIndex;
        initPos = transform.localPosition;
        newPos = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
    }


    public virtual void Update()
    {
        float tParam = Time.deltaTime / speed;
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, tParam);

        float currentPos = Vector3.Distance(transform.localPosition, newPos);
        if (currentPos <= acceptableRange)
            Destroy(this);
    }
}
