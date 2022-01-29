using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoot : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> loot = new List<GameObject>();
    [SerializeField]
    [Range(1, 99)]
    private int minNumber = 3;
    [Range(2, 100)]
    private int maxNumber = 7;
    [SerializeField]
    private Transform spawnPoint;
    private bool hasBeenCollected = false;

    [SerializeField]
    [Header("Click to Spawn")]
    private bool spawnLoot = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnValidate()
    {
        if (minNumber > maxNumber) {
            maxNumber = minNumber + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnLoot && !hasBeenCollected) {
            spawnLoot = false;
            Loot();
        }

        
        if (Input.GetKeyUp("space"))
        {
            Loot();
            //showBlockchainBridgeWindow();
            //popMessage();
            //FrontBodyAnimator.SetTrigger("Pickup");
            //BackBodyAnimator.SetTrigger("Pickup");
            print("Space key was released");
        }
        
    }

    private void Loot() {
        hasBeenCollected = false;
        int number = Random.Range(minNumber, maxNumber);
        StartCoroutine(CreateLoot(number));
    }

    IEnumerator CreateLoot(int number) {
        //this.GetComponent<Animator>().SetTrigger("openChest");
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < number; i++) {
            GameObject tempLoot = Instantiate(loot[Random.Range(0, loot.Count)]);
            tempLoot.transform.position = spawnPoint.position;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
