using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    //Public Vars
    public int minNumTargets;
    public int maxNumTargets;
    public float minTargetSpeed;
    public float maxTargetSpeed;
    public float minTargetSpawnDelay;
    public float maxTargetSpawnDelay;
    public float minChangeDirectionDelay;
    public float maxChangeDirectionDelay;

    public int scoreMultiplier;
    public GameObject target_prefab;

    //Private Vars
    int numTargets;
    int numTargetsSpawned;
    float targetSpeed;
    float targetSpawnDelay;
    float targetSpawnDelayRemaining;
    float changeDirectionDelay;
    float changeDirectionDelayRemaining;

    bool targetDirectionChanged;

    //Spawn Points
    Vector3 leftSpawn;
    Vector3 rightSpawn;

    Vector3 targetSpawnPoint;
    Vector3 targetEndPoint;

    //List of Targets
    ArrayList targetList = new ArrayList();

	void Start()
    {
        leftSpawn = transform.Find("LeftSpawn").position;
        rightSpawn = transform.Find("RightSpawn").position;
	}

	void Update()
    {
        CleanTargetList();

        //Check if platform should be reset
        if (targetList.Count == 0)
        {
            ResetPlatform();
            SpawnTarget();
        }
        //Check if platform is still spawning targets
        else if (numTargetsSpawned < numTargets)
        {
            targetSpawnDelayRemaining -= Time.deltaTime;

            if (targetSpawnDelayRemaining < 0)
            {
                SpawnTarget();
                targetSpawnDelayRemaining = targetSpawnDelay;
            }
        }
        //Control Direction Change
        else
        {
            changeDirectionDelayRemaining -= Time.deltaTime;
            if (changeDirectionDelayRemaining < 0 && !targetDirectionChanged)
            {
                SignalChangeDirection();
                targetDirectionChanged = true;
            }
        }
    }

    //*****************
    // Helper Methods
    //*****************

    void ResetPlatform()
    {
        //Generate Random Values
        numTargets = Random.Range(minNumTargets, maxNumTargets);
        targetSpeed = Random.Range(minTargetSpeed, maxTargetSpeed);
        targetSpawnDelay = Random.Range(minTargetSpawnDelay, maxTargetSpawnDelay);
        changeDirectionDelay = Random.Range(minChangeDirectionDelay, maxChangeDirectionDelay);

        //Randomize Spawn Point
        if (Random.value < 0.5f)
        {
            targetSpawnPoint = leftSpawn;
            targetEndPoint = rightSpawn;
        }
        else
        {
            targetSpawnPoint = rightSpawn;
            targetEndPoint = leftSpawn;
        }

        //Reset Timers
        targetSpawnDelayRemaining = targetSpawnDelay;
        changeDirectionDelayRemaining = changeDirectionDelay;

        //Reset Variables
        targetDirectionChanged = false;
        numTargetsSpawned = 0;
    } 

    //Removes destroyed targets from the targetList
    void CleanTargetList()
    {
        for(int i = 0; i < targetList.Count; i++)
            if (targetList[i].Equals(null))
                targetList.Remove(i);
    }


    //******************
    // Target Controls 
    //******************
    void SpawnTarget()
    {
        GameObject target = (GameObject)Instantiate(target_prefab, targetSpawnPoint, Quaternion.identity);
        TargetController target_controller = target.GetComponent<TargetController>();
        target_controller.InitializeTarget(targetSpawnPoint, targetEndPoint, targetSpeed, scoreMultiplier);

        targetList.Add(target);
        numTargetsSpawned++;
        targetSpawnDelayRemaining = targetSpawnDelay;
    }

    void SignalChangeDirection()
    {
        foreach(GameObject target in targetList)
        {
            TargetController target_controller = target.GetComponent<TargetController>();
            target_controller.ChangeTargetDirection();
        }
    }
}
