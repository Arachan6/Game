using System.Collections;
using UnityEngine;

public class WaveSpawner2 : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int size;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update ()
    {

        if (state == SpawnState.WAITING)
        {
            WaveCompleted();
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine( SpawnWave ( waves[nextWave] ) );
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            waveCountdown = 5000f;
            Debug.Log ("Waves completed! Looping...");
        } 

        else
        {
            nextWave++;
        }


    }
    
    IEnumerator SpawnWave (Wave _wave)
    {

        Debug.Log("Spawning Wave: "+ _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.size; i++)

        {
            SpawnEnemy (_wave.enemy);
            yield return new WaitForSeconds (_wave.spawnRate/1f);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Instantiate (_enemy, transform.position, transform.rotation);
    }
}
