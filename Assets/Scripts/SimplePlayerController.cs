using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float upDown = Input.GetAxis("UpDown");

        transform.position += new Vector3(horizontal, upDown, vertical) * Time.deltaTime * 100;
    }
}