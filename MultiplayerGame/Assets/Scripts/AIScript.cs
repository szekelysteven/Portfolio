using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{

    //variable to temporarly hold aiState until state machine is implemented
    public NavMeshAgent aiModel;
    public Animator aiAnimator;
    //timer variable
    public float timer;

    //time between traveling to different points
    [SerializeField]
    public float randomizeDuration;
    
    public float randomizeTrigger;

    //make an array that holds all waypoints for the aiModel to travel to.
    [SerializeField]
    public GameObject[] waypoints = new GameObject[10];
    public int randomWaypoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //get animator off of character
        aiAnimator = gameObject.GetComponent<Animator>();
        randomizeTrigger = Time.deltaTime + randomizeDuration;
        Randomize();
        

    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        
    }

    public void Randomize()
    {
        //Generate random waypoint
        randomWaypoint = Random.Range(0, waypoints.Length);
        randomizeTrigger = timer + randomizeDuration;
        Patrol();
    }
    
    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= randomizeTrigger)
        {
            Randomize();
        }
    }

    public void Patrol()
    {
        aiModel = GetComponent<NavMeshAgent>();
        //travel to random waypoint and play walking animation
        aiModel.destination = waypoints[randomWaypoint].transform.position;
    }

    //character attacks when a collision is detected. new destination will be the enemy being attacked. 
    public void OnCollisionEnter(Collision other)
    {



       if (other.transform.tag == "Enemy")
        {
            aiAnimator.SetBool("isAttacking", true);
            aiModel.destination = other.transform.position;
        }

    }
}
