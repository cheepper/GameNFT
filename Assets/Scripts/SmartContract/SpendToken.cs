using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpendToken : MonoBehaviour
{
    public int amount = 0;
    public InputField inputField;
    public Button confirmButton;
    public Button cancelButton;
    public Animator SpendTokenAnimator;
    private SmartContractBridge smartContract;
    void Start()
    {
        smartContract = transform.parent.gameObject.GetComponent<SmartContractBridge>();
        SpendTokenAnimator = transform.gameObject.GetComponent<Animator>();

        confirmButton.onClick.AddListener(ConfirmTransaction);
        cancelButton.onClick.AddListener(CancelTransaction);
    }

    // Update is called once per frame
    void Update()
    {
        if (SpendTokenAnimator.GetCurrentAnimatorStateInfo(0).IsName("SpendToken-Close")) {
            transform.gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }
    }

    void ConfirmTransaction() {
        int amount = int.Parse(inputField.text);
        smartContract.SpendToken(amount);
        SpendTokenAnimator.SetBool("isClosing", true);
    }

    void CancelTransaction() {
        SpendTokenAnimator.SetBool("isClosing", true);
    }
}
