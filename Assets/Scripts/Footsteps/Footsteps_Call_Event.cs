using UnityEngine;

public class Footsteps_Call_Event: MonoBehaviour
{
    public void footsteps_Call_Event(string s)
    {
        AkUnitySoundEngine.PostEvent(s, gameObject);
        Debug.Log("Print Event" + s +"Calledat: " + s);
    }
}
