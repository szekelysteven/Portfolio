using UnityEngine;
using UnityEngine.AI;


public enum AIState
{
    Idle,
    Patrol,
    Attacking
}

public class AIScript : MonoBehaviour
{

    //variable to temporarly hold aiState until state machine is implemented
    public GameObject other;
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

    public AIState currentState = AIState.Patrol;
    public NavMeshAgent aiModel;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //get animator off of character
        aiModel = GetComponent<NavMeshAgent>();
        aiAnimator = gameObject.GetComponent<Animator>();
        randomizeTrigger = Time.deltaTime + randomizeDuration;
        aiModel.destination = waypoints[randomWaypoint].transform.position;
        Randomize();
        

    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        switch (currentState)
        {
            case AIState.Idle:
                Idle();
                if ((aiModel.transform.position - aiModel.destination).magnitude > 5)
                {
                    currentState = AIState.Patrol;
                }
                break;
            case AIState.Patrol:
                Patrol();
                if ((aiModel.transform.position - aiModel.destination).magnitude < 5)
                {
                    currentState = AIState.Idle;
                }
                break;

            case AIState.Attacking:
                Attacking();
                break;
        }
    }

    public void Randomize()
    {
        //Generate random waypoint
        randomWaypoint = Random.Range(0, waypoints.Length);
        randomizeTrigger = timer + randomizeDuration;
        if (currentState == AIState.Patrol)
        {
            Patrol();
        }
    }
    
    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= randomizeTrigger)
        {
            Randomize();
            currentState = AIState.Patrol;
        }
    }

    public void Idle()
    {

        aiAnimator.SetBool("isIdle", true);
        aiAnimator.SetBool("isAttacking", false);
        aiAnimator.SetBool("isDancing", false);
    }

    public void Patrol()
    {

        

        aiModel.destination = waypoints[randomWaypoint].transform.position;
        aiAnimator.SetBool("isIdle", false);
        aiAnimator.SetBool("isAttacking", false);
        aiAnimator.SetBool("isDancing", true);
    }

    public void Attacking()
    {
        aiAnimator.SetBool("isIdle", false);
        aiAnimator.SetBool("isDancing", false);
        aiAnimator.SetBool("isAttacking", true);
        aiModel.destination = other.transform.position;
    }

    //character attacks when a collision is detected. new destination will be the enemy being attacked. 
    public void OnCollisionEnter(Collision other)
    {



       if (other.transform.tag == "Enemy")
        {
            Attacking();
            currentState = AIState.Attacking;
            aiModel.destination = other.transform.position;
        }

    }
}
