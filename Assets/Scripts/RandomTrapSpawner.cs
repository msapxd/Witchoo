using UnityEngine;

public class RandomTrapSpawner : MonoBehaviour
{
    public GameObject trap1;
    public GameObject trap2;
    public GameObject trap3;

    public GameObject[] spawnPoints;

    //public GameObject[] traps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShuffleSpawnPoints(spawnPoints);

        for (int i = 0; i + 2 < spawnPoints.Length; i += 3)
        {
            Instantiate(trap1, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            Instantiate(trap2, spawnPoints[i+1].transform.position, spawnPoints[i+1].transform.rotation);
            Instantiate(trap3, spawnPoints[i+2].transform.position, spawnPoints[i+2].transform.rotation);
        }
    }

    void ShuffleSpawnPoints(GameObject[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int num = Random.Range(0, i + 1);
            (arr[i], arr[num]) = (arr[num], arr[i]);
        }
    }
    
}
