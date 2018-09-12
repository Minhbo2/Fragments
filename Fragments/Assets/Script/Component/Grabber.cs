using UnityEngine;

public enum InteractableObj
{
    Movable,
    Switch,
    Throwable
}


public class Grabber : MonoBehaviour {

    [SerializeField]
    private float rayRange; 

    public Camera mainCamera = null;


    private void Update()
    {
        if (Input.GetMouseButton(0))
            Grab();
    }


    private void Grab()
    {
        Ray ray = RayDirection();
        RaycastHit hitObj;
        bool bHit = Physics.Raycast(ray, out hitObj);

        if (!bHit) { return; }

        print(hitObj.transform.name);
    }


    private Vector3 RayOrigin()
    {
        float height = mainCamera.pixelHeight/2;
        float width = mainCamera.pixelWidth / 2;
        return new Vector3(height, width, 0);
    }



    private Ray RayDirection()
    {
        return mainCamera.ScreenPointToRay(RayOrigin());
    }


    public void SetCamera(Camera cameraToSet)
    {
        mainCamera = cameraToSet;
    }
}
