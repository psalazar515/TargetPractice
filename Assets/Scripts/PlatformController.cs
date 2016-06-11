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

    public GameObject target_prefab;

    //Private Vars
    int numTargets;
    float targetSpeed;
    float targetSpawnDelay;

    float delayRemaining;

    bool active = false;
    bool targetDirectionChanged = false;

    //Spawn Points
    Vector3 leftSpawn;
    Vector3 rightSpawn;

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
        if (targetList.Count == 0)
            active = false;

        if (!active)
        {
            Randomize();
            targetList.Add(SpawnTarget());
            active = true;
        }
        else
        {
            delayRemaining -= Time.deltaTime;
            if (delayRemaining < 0 && !targetDirectionChanged)
            {
                Debug.Log("SignalChangeDirection");
                SignalChangeDirection();
                targetDirectionChanged = true;
            }
        }
        Debug.Log("Num Targets: " + numTargets);
        Debug.Log("Target Speed: " + targetSpeed);
        Debug.Log("Target Spawn Delay: " + targetSpawnDelay);
    }

    //*****************
    // Helper Methods
    //*****************
    void Randomize()
    {
        numTargets = Random.Range(minNumTargets, maxNumTargets);
        targetSpeed = Random.Range(minTargetSpeed, maxTargetSpeed);
        targetSpawnDelay = Random.Range(minTargetSpawnDelay, maxTargetSpawnDelay);

        delayRemaining = targetSpawnDelay;
    } 

    void CleanTargetList()
    {
        for(int i = 0; i < targetList.Count; i++)
            if (targetList[i].Equals(null))
                targetList.Remove(i);
    }


    //******************
    // Target Controls 
    //******************
    GameObject SpawnTarget()
    {
        Vector3 spawnPoint = leftSpawn;
        Vector3 targetPoint = rightSpawn;

        GameObject target = (GameObject)Instantiate(target_prefab, spawnPoint, Quaternion.identity);
        TargetController target_controller = target.GetComponent<TargetController>();
        target_controller.InitializeTarget(spawnPoint, targetPoint, targetSpeed);
        return target;
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
