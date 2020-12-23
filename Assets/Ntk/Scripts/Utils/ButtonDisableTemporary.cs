using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisableTemporary : MonoBehaviour
{
    public void DisableButton(Button btn)
    {
        btn.interactable = false;

        StartCoroutine(Enable(btn));
    }

    IEnumerator Enable(Button btn)
    {
        yield return new WaitForSeconds(8);
        btn.interactable = true;
    }
}
