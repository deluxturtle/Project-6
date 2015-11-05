using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptMyName : MonoBehaviour {

    public string myName = "Seba";

    public void _SetName(GameObject inputField)
    {
        myName = inputField.GetComponent<Text>().text;
    }
}
