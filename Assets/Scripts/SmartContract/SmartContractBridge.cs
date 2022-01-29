using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class SmartContractBridge : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AmountOfSupply(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToClaim(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToSpend(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToTransfer(string address, int amount);
    [DllImport("__Internal")]
    private static extern void RefreshBalance();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SupplyToken(int amount) {
        if(amount <= 0){
            amount = 0;
        }
        AmountOfSupply(amount);
        Debug.Log("Free Token: " + amount);
    }

    public void ClaimToken(int amount) {
        if(amount <= 0){
            amount = 0;
        }
        AmountToClaim(amount);
        Debug.Log("Claim Token: " + amount);
    }

    public void SpendToken(int amount) {
        if(amount <= 0){
            amount = 0;
        }
        AmountToSpend(amount);
        Debug.Log("Spend Token: " + amount);
    }

    public void TransferToken(string address, int amount) {
        if(amount <= 0){
            amount = 0;
        }
        AmountToTransfer(address, amount);
        Debug.Log("Transfer Token: " + address + " | " + amount);
    }
}
