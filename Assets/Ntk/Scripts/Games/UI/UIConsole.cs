using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIConsole : MonoBehaviour
{
    public Text infoText;

    private void Awake()
    {
        StartCoroutine(DestroyDelayed());
    }

    [SerializeField] float delayTime = 5;
    IEnumerator DestroyDelayed()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
