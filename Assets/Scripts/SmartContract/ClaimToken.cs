using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaimToken : MonoBehaviour
{
    public int amount = 0;
    public InputField inputField;
    public Text Supply;
    public Button confirmButton;
    public Button cancelButton;
    public Animator ClaimTokenAnimator;
    private SmartContractBridge smartContract;
    void Start()
    {
        smartContract = transform.parent.gameObject.GetComponent<SmartContractBridge>();
        ClaimTokenAnimator = transform.gameObject.GetComponent<Animator>();

        confirmButton.onClick.AddListener(ConfirmTransaction);
        cancelButton.onClick.AddListener(CancelTransaction);
    }

    // Update is called once per frame
    void Update()
    {
        if (ClaimTokenAnimator.GetCurrentAnimatorStateInfo(0).IsName("ClaimToken-Close")) {
            transform.gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void SetCurrentSupply(string amount) {
        Supply.text = amount + " TXT";
    }

    void ConfirmTransaction() {
        int amount = int.Parse(inputField.text);
        smartContract.ClaimToken(amount);
        ClaimTokenAnimator.SetBool("isClosing", true);
    }

    void CancelTransaction() {
        ClaimTokenAnimator.SetBool("isClosing", true);
    }
}
