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

    //Door Sounds
    [SerializeField] private AudioClip pickupCollectSound;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorShutSound;

    private new AudioSource audio; // Will hold the AudioSource component
    private bool doorIsOpen; // To determine current state of door

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        void OpenDoor()
        {
            // ...code we’ve already written...
            // Play audio only if the doorIsOpen variable is false
            if (!doorIsOpen)
            {
                // Play audio
                audio.PlayOneShot(doorOpenSound);
                doorIsOpen = true;
            }
        }
        void ShutDoor()
        {
            // ...code we’ve already written…
            if (doorIsOpen)
            {
                // Play audio
                audio.PlayOneShot(doorShutSound);
                doorIsOpen = false;
            }
        }

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
        if(hit.gameObject.CompareTag("houseDoor") && PickupCollect.pickupAmount >= 4)
        {
                //print("Hit the door!");
                OpenDoor();
            PickupCollect.hud.enabled = false;
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






    //Pickup Collection
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("pickup")) 
        {
            PickupCollect.pickupAmount++;

            audio.PlayOneShot(pickupCollectSound);

            Destroy(coll.gameObject);
        }
    }
}
