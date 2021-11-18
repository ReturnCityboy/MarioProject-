using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text killText;
    public Text coinText;

    public void RenewKillText(int count)
    {
        killText.text = "KILL:" + count.ToString();
    }
}
