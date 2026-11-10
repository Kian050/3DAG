using UnityEngine;

public class playerColision : MonoBehaviour
{
        //door animator
        [SerializeField] private GameObject door;// an open slot in the inspector to drag the hinge object into
        Animator doorAnimator;

        //door timer
        [Header("Door time")]
        private bool startDoorTimer = false;
        private float doorTimer=0.0f;
        [SerializeField] private float doorOpenTime = 3.0f;
        


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Run a door timer
        if(startDoorTimer)
        {
            doorTimer += Time.deltaTime;
        }
        
        //automatically shut door after timer
        if(doorTimer>=doorOpenTime)
        {
         ShutDoor(); //this calls the custom method below

         doorTimer = 0.0f;   
        }
    }

    //colision detection
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //check if player hits an object with the houseDoor tag
        if(hit.gameObject.CompareTag("houseDoor"))
        {
                //print("Hit the door!");
                OpenDoor();
        }
    }

    //custom open door method
    private void OpenDoor()
        {
            //set Animator parameter
            doorAnimator.SetBool("doorOpen" , true);

            startDoorTimer = true;
        }
    
    //custom shut door method
    private void ShutDoor()
    {
        //set Animator paramitor
        doorAnimator.SetBool("doorOpen" , false);
    }

}
