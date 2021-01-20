using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.inGameUI = this;
        infoControlObject.SetActive(false);
    }

    [SerializeField] GameObject infoControlObject;
    [SerializeField] Text infoControlText;

    public void ShowControlKey(string info)
    {
        infoControlObject.SetActive(true);
        infoControlText.text = info;
    }
    public void HideControlKey()
    {
        infoControlObject.SetActive(false);
    }

    [SerializeField] GameObject consolePrefab;
    [SerializeField] Transform spawnLoc;
    
    public void ShowConsoleLog(string info)
    {
        GameObject g = Instantiate(consolePrefab, spawnLoc);
        g.GetComponent<UIConsole>().infoText.text = info;
    }

}
