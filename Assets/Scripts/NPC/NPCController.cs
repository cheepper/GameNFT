using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Transform player;
    public Canvas canvas;
    public int panelIndex;
    public int id;
    public string npcName;
    public int selectedNpcId;
    public List<string> npcScripts;
    private int pageLenght;
    private Player playerScript;
    private Transform conversationPanel;
    private Text conversationText;
    private Text npcNameText;
    private Animator conversationAnimator;
    private Button proceedButton;
    private int pageCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.gameObject.GetComponent<Player>();
        conversationPanel = canvas.transform.GetChild(panelIndex);
        conversationAnimator = conversationPanel.gameObject.GetComponent<Animator>();

        conversationText = conversationPanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
        
        npcNameText = conversationPanel.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>();

        proceedButton = conversationPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
        proceedButton.onClick.AddListener(continueDialogWindow);
        
        pageLenght = npcScripts.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void showDialogWindow() {
        npcNameText.text = npcName;
        conversationText.text = npcScripts[pageCounter];
        conversationAnimator.SetBool("isConversation", true);
        transform.forward = player.position - transform.position;

        //transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.time * 0.1f);
    }

    private void continueDialogWindow() {
        if (selectedNpcId == id)
        {
            if (pageCounter == pageLenght)
            {
                pageCounter = 0;
                selectedNpcId = 0;
                closeDialogWindow(); 
            }
            else
            {
                pageCounter++;
                conversationText.text = npcScripts[pageCounter];
            }
        }
    }

    private void closeDialogWindow() {
        conversationAnimator.SetBool("isConversation", false);
        playerScript.zoomDialog = false;
    }
}
