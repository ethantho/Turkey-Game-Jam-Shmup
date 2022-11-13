using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject[] BossWaves;
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject bossesHealth;

    int currentWave;


    GameObject currentWaveObject;
    Vector3 spawnLocation;
    bool started;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        spawnLocation = Boss.transform.localPosition;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        //start wave
        if (!started && Boss.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("flyin"))
        {
            started = true;
            bossesHealth.SetActive(true);
            ActivateBossAttacks();
        }
        //all enemies in wave died go to next
        else if (started && currentWaveObject != null && currentWaveObject.transform.childCount == 0)
        {
            Destroy(currentWaveObject);
            currentWave++;

            if (currentWave == BossWaves.Length)
            {
                currentWave = 0;
            }

            currentWaveObject = Instantiate(BossWaves[currentWave], transform);
            currentWaveObject.transform.localPosition = spawnLocation;
        }
    }

    public void ActivateBossAttacks()
    {
        currentWaveObject = Instantiate(BossWaves[currentWave], transform);
        currentWaveObject.transform.localPosition = spawnLocation;
    }
}
