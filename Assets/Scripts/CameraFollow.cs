using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerLocation;

    void Update()
    {
        transform.position = new Vector3(playerLocation.position.x, playerLocation.position.y,transform.position.z);
    } 
}
