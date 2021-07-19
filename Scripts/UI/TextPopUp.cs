using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    public void ChangeText(string text)
    {
        m_Text.text = text;
    }
}
