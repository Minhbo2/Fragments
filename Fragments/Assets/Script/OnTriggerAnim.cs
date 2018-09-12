using UnityEngine;

public class OnTriggerAnim : MonoBehaviour {

    private bool isTrigger = false;

    [SerializeField]
    private bool b_YPosChange = true;

    [SerializeField]
    private GameObject[] animObjs;

    [SerializeField]
    private float stepInterval = 0.3f;
    private float lastTimeActivate = 0;
    [SerializeField]
    private float destDifferential;

    [SerializeField]
    private float previousYPos;

    private int objIndex = 0;

    [SerializeField]
    private AudioSource audio;

   void Update()
    {
        float currentTimeLapse = Time.time - lastTimeActivate;

        if(isTrigger)
        {

            if (objIndex >= animObjs.Length)
            {
                isTrigger = false;
                Destroy(this);
                return;
            }

            destDifferential = animObjs[objIndex].transform.localScale.y;
            if (currentTimeLapse > stepInterval)
            {
                AnimObj targetObj = animObjs[objIndex].GetComponent<AnimObj>();
                if (targetObj == null)
                {
                    targetObj = AddScriptAim(animObjs[objIndex]);
                }

                targetObj.yPos = previousYPos;
                targetObj.enabled = true;
                lastTimeActivate = Time.time;
                objIndex++;
                if (b_YPosChange)
                    previousYPos += destDifferential;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (audio == null) { return; }

            isTrigger = true;
            audio.Play();
            Destroy(GetComponent<Collider>());
        }
    }



    private AnimObj AddScriptAim(GameObject target)
    {
        target.AddComponent<AnimObj>();
        target.GetComponent<AnimObj>().enabled = false;
        return target.GetComponent<AnimObj>();
    }
}
