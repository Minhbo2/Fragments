using UnityEngine;
using UnityStandardAssets.ImageEffects;


public enum TimeState
{
    SLOW,
    PAST,
    PRESENT
}

public class TimeControl : MonoBehaviour {


    public TimeState currentState = TimeState.PRESENT;

    [SerializeField]
    private float   eMaxIndex,
                    eCurrentIndex,
                    eUseAmount,
                    eRechargeAmount;

    [SerializeField]
    private float   eConsumptionInterval,
                    eRechargeInterval;

    private float atTimeUse = 0;

    private int pastLayer = 9;
    private int presentLayer = 8;

    [SerializeField]
    private Grayscale imgGreyscale;
    [SerializeField]
    private Vortex imgVortex;

    [SerializeField]
    private Camera fpsCamera;


    private bool    isInPast   = false,
                    isTimeSlow = false,
                    isPresent  = true;

    [SerializeField]
    private AudioSource secondaryAS;


    //Accessors
    public float CurrentEnergyLevel() { return eCurrentIndex; }



    private void Start()
    {
        ResetLevel();
    }



    private void ResetLevel()
    {
        eCurrentIndex = eMaxIndex;
        PresentState();
        UsingTimeControl(false, false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isInPast)
            ChangeState(TimeState.PAST);
        else if (Input.GetKeyDown(KeyCode.F) && !isTimeSlow)
            ChangeState(TimeState.SLOW);
        else if (Input.GetKeyDown(KeyCode.G) && !isPresent)
            ChangeState(TimeState.PRESENT);


        if (isTimeSlow)
        {
            ManageEnergy(-eUseAmount, eConsumptionInterval);
            if(CurrentEnergyLevel() <= 0)
                ChangeState(TimeState.PRESENT);
        }
        else
            ManageEnergy(eRechargeAmount, eRechargeInterval);
    }


    private void ChangeState(TimeState newState)
    {
        currentState = newState;
        secondaryAS.Stop();

        isInPast   = false;
        isTimeSlow = false;
        isPresent  = false;

        //check if energy level is sufficient
        if (!HasEnoughEnergy())
            currentState = TimeState.PRESENT;

        switch (currentState)
        {
            case TimeState.SLOW:
                SlowDownTime();
                isTimeSlow = true;
                break;
            case TimeState.PAST:
                TravelToPast();
                isInPast = true;
                break;
            case TimeState.PRESENT:
                PresentState();
                isPresent = true;
                break;
        }
    }



    private void SlowDownTime()
    {
        //TODO: while this is active, all object touch still slow
        UsingTimeControl(true, false);
        secondaryAS.Play();
    }


    private void TravelToPast()
    {
        UsingTimeControl(true, true);
        this.gameObject.layer = pastLayer;
        Time.timeScale        = 1;
        CameraLayerRender(pastLayer);
        secondaryAS.Play();
    }



    private void PresentState()
    {
        Time.timeScale = 1;
        CameraLayerRender(presentLayer);
        this.gameObject.layer = presentLayer;
        UsingTimeControl(false, false);
    }



    private void CameraLayerRender(int layerIndex)
    {
        if (!fpsCamera) { return; }

        //rendering multiple layers
        fpsCamera.cullingMask = 1<<0 | 1<<layerIndex;
    }


    private bool HasEnoughEnergy()
    {
        return (CurrentEnergyLevel() > 0) ? true : false;
    }


    private void ManageEnergy(float energyIndex, float targetTime)
    {
        float currentTimeUse = Time.time;
        float nextTimeUse = currentTimeUse - atTimeUse;

        if (nextTimeUse > targetTime)
            AddOrSubtractEnergy(energyIndex);
    }



    private void AddOrSubtractEnergy(float energyIndex)
    {
        eCurrentIndex += energyIndex;
        atTimeUse = Time.time;

        //check energy is not exceeding the max and must not less than 0
        eCurrentIndex = Mathf.Clamp(eCurrentIndex, 0, eMaxIndex);
    }



    private void UsingTimeControl(bool bGscale, bool bVortex)
    {
        if (!imgGreyscale && !imgVortex) { return; }

        imgGreyscale.enabled = bGscale;
        imgVortex.enabled = bVortex;
    }
}
