using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour

{
    public Vector3 scaleChange;
    public Transform prefab;
    //SpawnX and SpawnY are the boundries of the spawner.
    [Tooltip("Min and Max X Values")]
    public float SpawnX;
    public float SpawnY;
    public float SpawnZ;
    public float spawnRate;
    [Tooltip("Min and Max Y Values")]
    
    private float oldTime;
    private float currentTime;

   
    public List<GameObject> allFreightBoxes = new List<GameObject>();

    public Component workerScript;
    public GameObject otherObject;
    void Start()
    {
        //get refernce for worker script on worker gameobject
        workerScript = otherObject.GetComponent<Component>();
        
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= (spawnRate + oldTime))
        {
            oldTime = currentTime;
            
            {
                spawn();
                consoleDisplay();
            }
        }
    }

    public void spawn()
    {
        //scalechange creates a random scale, a box is instantiated, the random scale is assigned to the box
        scaleChange = new Vector3(Random.Range(0.1f, 4.0f), Random.Range(0.1f, 4.0f), Random.Range(0.1f, 4.0f));
        //Instantiate command creates a new box from prefab
        Transform box = Instantiate(prefab, transform.position + new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
        box.transform.localScale = scaleChange;
    }


    public void findFreightBoxes()
    {
            GameObject g = GameObject.FindGameObjectWithTag("FreightBox");
            allFreightBoxes.Add(g);
            //allFreightBoxes.Clear();

    }
    

    public void consoleDisplay()
    {
        int i = 0;
        for (i = 0; i < allFreightBoxes.Count; i++)
        {
            Debug.Log(allFreightBoxes.Count);
        }
    }
}
