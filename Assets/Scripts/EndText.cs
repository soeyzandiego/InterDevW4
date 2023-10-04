using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int count = FindObjectOfType<GameManager>().GetBulletCount();
        GetComponent<TMP_Text>().text = "You made it! You shot " + count.ToString() + " bullets.";
    }
}
