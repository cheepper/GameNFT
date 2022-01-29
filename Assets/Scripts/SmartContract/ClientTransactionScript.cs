using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class ClientTransactionScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AmountOfSupply(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToClaim(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToSpend(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToTransfer(string address, int amount);

    public Button FreeToken;
    public Button Claim;
    public Button Spend;
    public Button Transfer;

    public InputField FreeTokenInputField;
    public InputField ClaimInputField;
    public InputField SpendInputField;
    public InputField TransferInputField;
    public InputField AddressInputField;

    // Start is called before the first frame update
    void Start()
    {
        FreeToken.onClick.AddListener(SupplyToken);
        Claim.onClick.AddListener(ClaimToken);
        Spend.onClick.AddListener(SpendToken);
        Transfer.onClick.AddListener(TransferToken);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SupplyToken() {
        int amount = int.Parse(FreeTokenInputField.text);
        if(amount <= 0){
            amount = 0;
        }
        AmountOfSupply(amount);
        Debug.Log("Free Token: " + amount);
    }

    void ClaimToken() {
        int amount = int.Parse(ClaimInputField.text);
        if(amount <= 0){
            amount = 0;
        }
        AmountToClaim(amount);
        Debug.Log("Claim Token: " + amount);
    }

    void SpendToken() {
        int amount = int.Parse(SpendInputField.text);
        if(amount <= 0){
            amount = 0;
        }
        AmountToSpend(amount);
        Debug.Log("Spend Token: " + amount);
    }

    void TransferToken() {
        string address = AddressInputField.text;
        int amount = int.Parse(TransferInputField.text);
        if(amount <= 0){
            amount = 0;
        }
        AmountToTransfer(address, amount);
        Debug.Log("Transfer Token: " + address + " | " + amount);
    }
}
