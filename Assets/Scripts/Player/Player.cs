using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;
    public GameObject FrontBody, BackBody;
    public GameObject PopupMessage, PopupBox;
    //public AudioClip footStep;
    public Canvas canvas;
    public bool isTalkingToNPC = false;
    private UnityEngine.AI.NavMeshAgent MeshAgent;
    private Camera mainCamera;
    private Animator FrontBodyAnimator, BackBodyAnimator, PopBubbleMessageAnimator, PopBoxMessageAnimator;
    private CharacterSpriteController CharacterSpriteScript;
    private Transform TextMessage;
    private RaycastHit raycastHitInfo;
    private bool hasQueueInteraction = false;
    private Vector3 queuesInteractPos;
    private Transform queuesTransform;
    private bool isTeleporting = false, isTransitioning, isInteractingToObjects = false, isInteractingToLootBox = false, isInteractingToBlocks = false, isUsingMachine = false, onUIOptions = false;
    private SpriteRenderer spriteRenderer;
    private float stepRate = 0.3f;
	private float stepCoolDown;

    private float nextActionTime = 10.0f;

    // Start is called before the first frame update
    void Start() {
        mainCamera = Camera.main;
        MeshAgent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        
        FrontBodyAnimator = FrontBody.GetComponent<Animator>();
        BackBodyAnimator = BackBody.GetComponent<Animator>();

        PopBubbleMessageAnimator = PopupMessage.GetComponent<Animator>();
        PopBoxMessageAnimator = PopupBox.GetComponent<Animator>();
        TextMessage = PopupMessage.transform.GetChild(0);

        CharacterSpriteScript = transform.gameObject.GetComponent<CharacterSpriteController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp("space")) {
            //showBlockchainBridgeWindow();
            //popMessage();
            //FrontBodyAnimator.SetTrigger("Pickup");
            //BackBodyAnimator.SetTrigger("Pickup");
            print("Space key was released");
        }

        // Player is on the move
        if (MeshAgent.hasPath) {
            // Play the walking sound
            stepCoolDown -= Time.deltaTime;
            if (stepCoolDown < -0.05f) {
                //GetComponent<AudioSource>().pitch = 0.8f + Random.Range (-0.025f, 0.025f);
                //GetComponent<AudioSource>().PlayOneShot (footStep, 0.5f);
                stepCoolDown = stepRate;
            }
            // Zoom-out when suddenly move while interacting
            zoomOutInteraction();
            //Debug.Log("player is moving");
        }

        // Move the player while holding down left mouse button
        if (Input.GetMouseButton(0) && !onUIOptions) {
            if (EventSystem.current.IsPointerOverGameObject()) {
                //Debug.Log("Clicked on the UI");
            } else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 100, whatCanBeClickedOn)) {
                    //FrontBodyAnimator.SetBool("isWalking", true);
                    //BackBodyAnimator.SetBool("isWalking", true);
                    FrontBodyAnimator.SetBool("isRunning", true);
                    BackBodyAnimator.SetBool("isRunning", true);

                    MeshAgent.SetDestination(hitInfo.point);
                }
            }
        }

        // Move the player by clicking on the field with left mouse button
        if (Input.GetMouseButtonUp(0)) {
            if (EventSystem.current.IsPointerOverGameObject()) {
                //Debug.Log("Clicked on the UI");
            } else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Check if player has UI options window showsup
                if (onUIOptions) {
                    // For 2D sprite raycast hit in 3D field 
                    //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //RaycastHit hit;
                    try {
                        //if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Options")) {
                        if (Physics.Raycast (ray, out hit, 100, LayerMask.GetMask("Options"))) {
                            PopBoxMessageAnimator.SetBool("isInteracting", false);
                            showBlockchainBridgeWindow(int.Parse(hit.transform.gameObject.tag));
                            isUsingMachine = false;
                            onUIOptions = false;
                            Debug.Log("object clicked: "+hit.transform.gameObject.tag);
                        }
                    } catch {
                        isUsingMachine = false;
                        onUIOptions = false;
                        //canvas.gameObject.SetActive(false);
                        Debug.Log("Null, 2D sprite raycast reference");
                    }
                } else {
                    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //RaycastHit hit;
                    // Filter what can be click on the field
                    if (Physics.Raycast(ray, out hit, 100, whatCanBeClickedOn)) {
                        raycastHitInfo = hit;
                        // Check if player click on the NPC
                        if (checkAssignLayer("NPC"))
                        {
                            isTalkingToNPC = true;
                        }
                        // Check if player click on the teleport platform
                        else if (checkAssignLayer("Teleport"))
                        {
                            isTeleporting = true;
                        }
                        // Check if player click on the door
                        else if (checkAssignLayer("Door"))
                        {
                            isTransitioning = true;
                            Debug.Log("go transition " + isTransitioning);
                        }
                        // Check if player on the Interactable objects
                        else if (checkAssignLayer("Object"))
                        {
                            isInteractingToObjects = true;
                        }
                        // Check if player on the Interactable loot box
                        else if (checkAssignLayer("LootBox"))
                        {
                            isInteractingToLootBox = true;
                        }
                        // Check if player on the Interactable blocks/Mine
                        else if (checkAssignLayer("Blocks"))
                        {
                            isInteractingToBlocks = true;
                        }
                        // Check if player on the Interactable Machine
                        else if (checkAssignLayer("Machine")) 
                        {
                            isUsingMachine = true;
                        } 
                        else 
                        {
                            // Cancel any queues for interaction
                            hasQueueInteraction = false;
                            isTransitioning = false;
                            isTeleporting = false;
                            isTalkingToNPC = false;
                            isInteractingToObjects = false;
                            isInteractingToLootBox = false;
                            isInteractingToBlocks = false;
                            //isUsingMachine = false;
                        }
                    }
                }
            }
        }
        spriteRenderingInteraction();
    }

    bool checkAssignLayer(string layerName) {
        if(raycastHitInfo.transform.gameObject.layer == LayerMask.NameToLayer(layerName)) {
            hasQueueInteraction = true;
            return true;
        } else {
            return false;
        }
    }

    void spriteRenderingInteraction() {
        // Check whether interacting with NPC or interactable objects
        //if (isTalkingToNPC || isInteractingToObjects || isUsingMachine) {
        if (isTalkingToNPC || isUsingMachine)
            {
                // Zoom-in when player stop and interacting
                if (!MeshAgent.hasPath) {
                zoomInInteraction();
            }
            // Flip popup bubble message
            spriteRenderer = PopupMessage.GetComponent<SpriteRenderer>();
            if (CharacterSpriteScript.isRight) {
                //Comment-out replaced by dot popup, use only if using popupmessage instance
                PopupMessage.transform.localPosition = new Vector3(-0.20f, 1.25f, 10.0f);
                spriteRenderer.flipX = false;
            } else {
                //Comment-out replaced by dot popup, use only if using popupmessage instance
                PopupMessage.transform.localPosition = new Vector3(-0.40f, 1.25f, 10.0f);
                spriteRenderer.flipX = true;
            }
        } else {
            zoomOutInteraction();
        }
    }

    public void zoomInInteraction() {
        if (isTalkingToNPC) { 
            for (int i = 0; i < 4; i++)
            {
                Transform dot = PopupMessage.transform.GetChild(i);
                dot.GetComponent<Renderer>().enabled = true;
            }
        }
        
        mainCamera.orthographicSize  -= 12.0f * Time.deltaTime;
        if (mainCamera.orthographicSize <= 12.0f) {
            mainCamera.orthographicSize = 12.0f;
        }
        //Debug.Log("Interact Zoomin");
    }

    public void zoomOutInteraction() {
        for (int i = 0; i < 4; i++)
        {
            Transform dot = PopupMessage.transform.GetChild(i);
            dot.GetComponent<Renderer>().enabled = false;
        }
        mainCamera.orthographicSize  += 12.0f * Time.deltaTime;
        if (mainCamera.orthographicSize >= 15.0f) {
            mainCamera.orthographicSize = 15.0f;
        }

        // Close the interaction window
        PopBubbleMessageAnimator.SetBool("isInteracting", false);
        PopBoxMessageAnimator.SetBool("isInteracting", false);
        //Debug.Log("Interact ZoomOut");
    }

    private void LateUpdate()
    {
        // Check if player has click any interactable and place 
        // them in queue so when the player is on the move it automatically interact
        try
        {
            if (hasQueueInteraction)
            {
                float distance = Vector3.Distance(transform.position, raycastHitInfo.transform.position);
                //Debug.Log("Approaching interact " + dist);
                if (distance < 5.0)
                {
                    hasQueueInteraction = false;
                    MeshAgent.ResetPath();
                    //Debug.Log("Interact!!");
                    // Check what did the player interact with
                    if (isTeleporting)
                    {
                        isTeleporting = false;
                        Teleport warpPortal = raycastHitInfo.transform.gameObject.GetComponent<Teleport>();
                        warpPortal.WarpDestination();
                    }
                    else if (isTransitioning)
                    {
                        isTransitioning = false;
                        Teleport warpPortal = raycastHitInfo.transform.gameObject.GetComponent<Teleport>();
                        warpPortal.useTransition = true;
                        //warpPortal.WarpDestination(true);
                    }
                    else if (isTalkingToNPC)
                    {
                        NPCController npcController = raycastHitInfo.transform.gameObject.GetComponent<NPCController>();
                        npcController.selectedNpcId = npcController.id;
                        Debug.Log("npc name = " + npcController.npcName);
                        npcController.showConversationWindow();
                        transform.forward = npcController.transform.position - transform.position;
                        popMessage();
                    }
                    else if (isInteractingToObjects)
                    {
                        FrontBodyAnimator.SetTrigger("Pickup");
                        BackBodyAnimator.SetTrigger("Pickup");
                        transform.forward = raycastHitInfo.transform.position - transform.position;
                        Destroy(raycastHitInfo.transform.gameObject, 0.5f);
                    }
                    else if (isInteractingToLootBox)
                    {

                    }
                    else if (isInteractingToBlocks)
                    {

                    }
                    else if (isUsingMachine)
                    {
                        boxMessage();
                    }
                }
            }

            // If player has stop moving change to idle animation
            if (hasReachedDestination())
            {
                //FrontBodyAnimator.SetBool("isWalking", false);
                //BackBodyAnimator.SetBool("isWalking", false);
                FrontBodyAnimator.SetBool("isRunning", false);
                BackBodyAnimator.SetBool("isRunning", false);
            }
        }
        catch
        {
            Debug.Log("Object disappear");
        }
    }

    bool hasReachedDestination() {
        if (MeshAgent.remainingDistance <= 0.5f)
            return true;
        else
            return false;        
    }

    void popMessage() {
        PopBubbleMessageAnimator.SetBool("isInteracting", true);
    }

    void boxMessage() {
        PopBoxMessageAnimator.SetBool("isInteracting", true);
        onUIOptions = true;
    }

    void showBlockchainBridgeWindow(int option) {
        //canvas.gameObject.SetActive(true);
        Transform container = canvas.transform.GetChild(0);
        container.gameObject.SetActive(true);
        Transform transactionOption = container.transform.GetChild(option);
        transactionOption.gameObject.SetActive(true);
        // switch(option) {
        //     case 1:
                
        //     case 2:

        //     case 3:

        //     default:
        //         Debug.Log("Unknown option");
        //     break;
        // }
        Debug.Log("Open option window");
    }
}
