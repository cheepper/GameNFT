using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public Transform[] npc;

    void Awake()
    {
        NPCS npcsInJson = JsonUtility.FromJson<NPCS>(jsonFile.text);
        int i = 0;
        foreach (NPCScripts npcScripts in npcsInJson.npcs)
        {
            NPCController npcController = npc[i].gameObject.GetComponent<NPCController>();
            npcController.id = npcScripts.id;
            npcController.npcName = npcScripts.npcName;
            int a = 0;
            foreach (string script in npcScripts.script) 
            {
                npcController.npcScripts.Add(script);
                a++;
            }
            i++;
        }
    }
}