using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 60f;
    public float panBorderThickness = 0f;
    public Vector2 panLimitX;
    public Vector2 panLimitY;
    public Vector2 panLimitZ;
    public float scrollSpeed = 20f;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
        pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);

        pos.y = Mathf.Clamp(pos.y, panLimitY.x, panLimitY.y);

        transform.position = pos;
    }
}
