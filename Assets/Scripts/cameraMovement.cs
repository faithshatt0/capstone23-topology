using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        transform.Translate(move, Space.World);

        if (Input.GetMouseButtonDown(1))
        {
            return;
        }

    }
}
