using UnityEngine;

public class SpawnerNaveChefe : MonoBehaviour
{
    public GameObject prefabNaveChefe;
    public float intervaloMinimo = 3f;
    public float intervaloMaximo = 10f;

    private float timerProximoSpawn;

    void Start() => AgendarProximoSpawn();

    void Update()
    {
        if (Time.time >= timerProximoSpawn)
        {
            Instantiate(prefabNaveChefe);
            AgendarProximoSpawn();
        }
    }

    private void AgendarProximoSpawn()
    {
        timerProximoSpawn = Time.time + Random.Range(intervaloMinimo, intervaloMaximo);
    }
}