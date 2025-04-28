//Steven Szekely
//4/21/2025


using UnityEngine;
using UnityEngine.AI;


public enum AIState
{
    Idle,
    Patrol,
    Attacking,
    Chasing
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
               
                //if statement to get the ai to patrol when destination is farther than a magnitude of 2
                if ((aiModel.transform.position - aiModel.destination).magnitude > 2)
                {
                    currentState = AIState.Patrol;
                }
                break;

            case AIState.Patrol:

                Timer();
                Patrol();
                
                //if statement to get the ai to idle when destination is reached within a magnitude of 2
                if ((aiModel.transform.position - aiModel.destination).magnitude < 2)
                {
                    currentState = AIState.Idle;
                }


                break;

            case AIState.Attacking:

                Attacking();
                
                //if enemy becomes more than 2 magnitude in distance, then switch animation to walking and close gap. once gap is closed change back to fighting animation
                //might be good to add a chase state
                if ((aiModel.transform.position - aiModel.destination).magnitude > 2)
                {
                    currentState = AIState.Chasing;
                }
                else
                {
                    currentState = AIState.Attacking;
                }
                    break;

            case AIState.Chasing:

                Chasing();
                

                if ((aiModel.transform.position - aiModel.destination).magnitude < 2)
                {
                    currentState = AIState.Attacking;
                }
                else
                {
                    currentState = AIState.Chasing;
                }
                break;
        }
    }

    public void Randomize()
    {
        //Generate random waypoint
        randomWaypoint = Random.Range(0, waypoints.Length);
        randomizeTrigger = timer + randomizeDuration;
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
        aiAnimator.SetBool("isWalking", false);
    }

    public void Patrol()
    {
        


        aiModel.destination = waypoints[randomWaypoint].transform.position;
        aiModel.transform.LookAt(aiModel.destination);
        aiAnimator.SetBool("isIdle", false);
        aiAnimator.SetBool("isWalking", false);
        aiAnimator.SetBool("isDancing", true);
    }

    public void Attacking()
    {
        
        aiAnimator.SetBool("isIdle", false);
        aiAnimator.SetBool("isWalking", false);
        aiAnimator.SetBool("isAttacking", true);
        aiModel.destination = enemy.transform.position;
        aiModel.transform.LookAt(enemy.transform);
    }

    public void Chasing()
    {
        aiAnimator.SetBool("isIdle", false);
        aiAnimator.SetBool("isAttacking", false);
        aiAnimator.SetBool("isWalking", true);
        aiModel.destination = enemy.transform.position;
        aiModel.transform.LookAt(enemy.transform);
    }

    //character attacks when a collision is detected. new destination will be the enemy being attacked. 
    public void OnCollisionEnter(Collision other)
    {



       if (other.transform.tag == "Enemy")
        {
            enemy = other.gameObject;
            currentState = AIState.Attacking;
            
        }

    }

}
