using UnityEngine.UI;
using UnityEngine;

public class UIManagerSet : MonoBehaviour {

    public static UIManagerSet Inst { get { return m_Inst; } }
    static UIManagerSet m_Inst;

    [SerializeField]
    private Text objectMessage;


    private float textDuration = 3;
    private float atTimeUse;

    private void Awake()
    {
        if (m_Inst == null)
            m_Inst = this;
    }



    private void Update()
    {
        float currentTime = Time.time;
        if ((currentTime - atTimeUse) > textDuration)
            objectMessage.text = "";
    }


    public void DisplayText(string message)
    {
        objectMessage.text = message;
        atTimeUse = Time.time;
    }
}
