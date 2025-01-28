using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public NavMeshAgent agent;
    
    //Create object list of all pallets in scene
    public List<GameObject> allPallets = new List<GameObject>();
    //Have worker path to nearest box

    //Attach box to player object
    //Run through a loop to find correct pallet in pallet object list
    //Have worker path to corresponding pallet position
    //Box disappears when contact is made with pallet
    //Repeat




    // Start is called before the first frame update
    void Start()
    {
        
        findPallets();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    
    public void findPallets()
    {

    }
    public void pathToFreight()
    {
        //agent.SetDestination(0,0,0);
    }
    public void pathToPallet()
    {
        //agent.SetDestination(vector);
    }

    //show freightboxes list in console
   
}
