using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Set Player Info Event Channel")]
public class SetPlayerInfoEventChannelSO : ScriptableObject
{
    //maybe think about generalize this 
	public UnityAction<int, Material, int> OnMaterialEventRaised;
	public void RaiseEvent(int kawaiijuOption, Material skin, int index)
	{
		OnMaterialEventRaised.Invoke(kawaiijuOption, skin, index);
	}
}
