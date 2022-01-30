using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[CreateAssetMenu(fileName = "New UIInfo")]
public class UIInfo : ScriptableObject
{
    public List<InputBtnInfo> supportedInput;
    public ButtonCode buttonId;
    Dictionary<string, InputBtnInfo> supportedInputDict;

    


    public InputBtnInfo GetBtnInfo(string controllerStyle, bool keyConfig1=false)
    {
        
        if (controllerStyle.ToLower().CompareTo("keyboard") != 0 )
        {
            return (from x in supportedInput where controllerStyle.ToLower().Contains(x.deviceName.ToLower()) select x).First();
        }
        else 
        {
            return (keyConfig1) ? (from x in supportedInput where x.keyboardConfig1 select x).First() : (from x in supportedInput where !x.keyboardConfig1 && x.deviceName.ToLower().CompareTo("keyboard") == 0 select x).First();
        }
    }
}

