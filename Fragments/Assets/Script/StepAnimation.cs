 using UnityEngine;

public class StepAnimation : MonoBehaviour {
    [SerializeField]
    private GameObject beamHolder;
    [SerializeField]
    private GameObject stepHolder;

    [SerializeField]
    private float beamMaxDistance;
    [SerializeField]
    private float distActiveStepRot;

    [SerializeField]
    private Quaternion stepRotator;

    private Vector3 newBeamPos;


    private void Start()
    {
        newBeamPos = new Vector3(beamMaxDistance, 0, 0);
    }


    private void Update()
    {
        TranslateBeam();
    }



    public void TranslateBeam()
    {
        float smoothSpeed = 1.5f;
        float tParam = Time.deltaTime / smoothSpeed;
        beamHolder.transform.localPosition = Vector3.Lerp(beamHolder.transform.localPosition, newBeamPos, tParam);

        float beamCurrentXPos = beamHolder.transform.localPosition.x;
        if (beamCurrentXPos >= distActiveStepRot)
            RotateAtBeamDistance(); 
    }


    private void RotateAtBeamDistance()
    {
        // all quaternion value are ranging from 0 to 1
        Quaternion currentRotation = stepHolder.transform.localRotation;
        float tParam = Time.deltaTime / 2.5f;
        stepHolder.transform.localRotation = Quaternion.Lerp(currentRotation, stepRotator, tParam);
    }
}
