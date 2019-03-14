using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float dragSpeed = 2;
    private float angle;

    float camZoom = -10f;
    float camZoomSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            angle = 60 * (Input.mousePosition.x < Screen.width/2 ? 1 : -1) * Time.deltaTime;
            return;
        }
        if (!Input.GetMouseButton(1)) return;

        transform.RotateAround(Vector3.zero, Vector3.up, angle);

        //if (Input.GetAxis("Mouse ScrollWheel") != 0)
        //{
        //    camZoom += Input.GetAxis("Mouse ScrollWheel") * camZoomSpeed;
        //    return;
        //}
        //transform.position = new Vector3(transform.position.x, transform.position.y, camZoom);
    }
}
