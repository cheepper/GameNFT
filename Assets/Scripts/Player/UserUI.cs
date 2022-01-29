using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    public Text UserAddress;
    public Text UserBalance;
    public Text SupplyBalance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUserAddress(string address)
    {
        UserAddress.text = address;
    }

    public void SetUserBalance(string balance)
    {
        UserBalance.text = balance + " TXT";
    }

    public void SetRemainingFreeSupply(string balance)
    {
        SupplyBalance.text = balance + " TXT";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
