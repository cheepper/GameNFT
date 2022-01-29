using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeToken : MonoBehaviour
{
    public int amount = 0;
    public Text amountText;
    public UnityEngine.UI.Slider slider;
    public Button confirmButton;
    public Button cancelButton;
    public Animator FreeTokenAnimator;
    private SmartContractBridge smartContract;
    void Start()
    {
        smartContract = transform.parent.gameObject.GetComponent<SmartContractBridge>();
        FreeTokenAnimator = transform.gameObject.GetComponent<Animator>();

        confirmButton.onClick.AddListener(ConfirmTransaction);
        cancelButton.onClick.AddListener(CancelTransaction);

        //StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        amountText.text = slider.value + " TXT";
        amount = (int)slider.value;

        if (FreeTokenAnimator.GetCurrentAnimatorStateInfo(0).IsName("FreeToken-Close")) {
            transform.gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void showTransactionWindow() {
        FreeTokenAnimator.Play("FreeToken",-1, 0f);
        FreeTokenAnimator.SetBool("isClosing", false);
    }

    void ConfirmTransaction() {
        smartContract.SupplyToken(amount);
        FreeTokenAnimator.SetBool("isClosing", true);
    }

    void CancelTransaction() {
        FreeTokenAnimator.SetBool("isClosing", true);
    }
}
