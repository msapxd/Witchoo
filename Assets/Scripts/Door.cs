using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject openDoor;
    public GameObject closedDoor;
    public void Open()
    {
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }

    public void Close()
    {
        closedDoor.SetActive(true);
        openDoor.SetActive(false);
    }

    void Start()
    {
        Close();
    }
}
