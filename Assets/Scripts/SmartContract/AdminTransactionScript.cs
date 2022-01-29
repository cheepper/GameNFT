using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class AdminTransactionScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Login();

    //[DllImport("__Internal")]
    //private static extern void CurrentSupply();

    [DllImport("__Internal")]
    private static extern void AmountToMint(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToBurn(int amount);

    [DllImport("__Internal")]
    private static extern void AmountToTransfer(string address, int amount);

    [DllImport("__Internal")]
    private static extern void RefreshBalance();

    //[DllImport("__Internal")]
    //private static extern string SetUserAddress();

    //[DllImport("__Internal")]
    //private static extern string SetContractAddress();

    public Button Mint;
    public Button Burn;
    public Button Transfer;
    public Button Refresh;

    public InputField MintInputField;
    public InputField BurnInputField;
    public InputField TransferInputField;
    public InputField AddressInputField;

    public Text UserAddress;
    public Text ContractAddress;
    public Text Supply;

    // Start is called before the first frame update
    void Start()
    {
        Mint.onClick.AddListener(MintToken);
        Burn.onClick.AddListener(BurnToken);
        Transfer.onClick.AddListener(TransferToken);
        Refresh.onClick.AddListener(RefreshSupply);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUserAddress(string address) {
        UserAddress.text = address;
    }

    public void SetContractAddress(string address) {
        ContractAddress.text = address;
    }

    public void SetCurrentSupply(string amount) {
        Supply.text = amount;
    }

    void MintToken() {
        int amount = int.Parse(MintInputField.text);
        if(amount <= 0){
            amount = 0;
        }
        AmountToMint(amount);
        Debug.Log("Mint Token: " + amount);
    }

    void BurnToken() {
        int amount = int.Parse(BurnInputField.text);
        if(amount <= 0){
            amount = 0;
        }
        AmountToBurn(amount);
        Debug.Log("Burn Token: " + amount);
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

    void RefreshSupply() {
        RefreshBalance();
        Debug.Log("Refresh Balance");
    }
}
