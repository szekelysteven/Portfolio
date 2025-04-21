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
    public GameObject enemy;

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
        
        
        switch (currentState)
        {
            case AIState.Idle:
                Timer();
                Idle();
                if ((aiModel.transform.position - aiModel.destination).magnitude > 2)
                {
                    currentState = AIState.Patrol;
                }
                break;
            case AIState.Patrol:
                Timer();
                Patrol();
                if ((aiModel.transform.position - aiModel.destination).magnitude < 2)
                {
                    currentState = AIState.Idle;
                }


                break;

            case AIState.Attacking:
                Attacking();
                
                //if enemy becomes more than 2 magnitude in distance, then switch animation to walking and close gap. once gap is closed change back to fighting animation
                //might be good to add running state here
                if ((aiModel.transform.position - aiModel.destination).magnitude > 2)
                {
                    aiAnimator.SetBool("isIdle", false);
                    aiAnimator.SetBool("isAttacking", false);
                    aiAnimator.SetBool("isDancing", true);
                    aiModel.destination = enemy.transform.position;
                }
                else
                {
                    aiAnimator.SetBool("isIdle", false);
                    aiAnimator.SetBool("isAttacking", true);
                    aiAnimator.SetBool("isDancing", false);
                }
                    break;
        }
    }

    public void Randomize()
    {
        //Generate random waypoint
        randomWaypoint = Random.Range(0, waypoints.Length);
        randomizeTrigger = timer + randomizeDuration;
        currentState = AIState.Patrol;
    }
    
    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= randomizeTrigger)
        {
            Randomize();
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
        aiModel.destination = enemy.transform.position;
    }

    //character attacks when a collision is detected. new destination will be the enemy being attacked. 
    public void OnCollisionEnter(Collision other)
    {



       if (other.transform.tag == "Enemy")
        {
            enemy = other.gameObject;
            Attacking();
            currentState = AIState.Attacking;
            aiModel.destination = enemy.transform.position;
        }

    }
}
