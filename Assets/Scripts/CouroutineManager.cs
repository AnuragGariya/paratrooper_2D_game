using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouroutineManager : MonoBehaviour
{
    public static CouroutineManager _Instance;
    private WaitForEndOfFrame waitForFrameEnd = new WaitForEndOfFrame();
    private WaitForSeconds waitFor2Sec = new WaitForSeconds(2);
    private WaitForSeconds waitFor1Sec = new WaitForSeconds(1);

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public WaitForEndOfFrame WaitForFrameEnd()
    {
        return waitForFrameEnd;
    }

    public WaitForSeconds WaitFor2Sec()
    {
        return waitFor2Sec;
    }

    public WaitForSeconds WaitFor1Sec()
    {
        return waitFor1Sec;
    }
}
