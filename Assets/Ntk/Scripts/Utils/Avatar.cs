using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public Sprite [] Avatars;

    private void Awake()
    {
        GameManager.Instance.Avatar = Avatars;
    }
}
