using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour {
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade _facade;

    protected GameFacade gamefacade
    {
        get
        {
            if(_facade == null)
            {
                _facade = GameFacade.Instance;
            }
            return _facade;
        }
    }

	public virtual void Awake () {
        GameFacade.Instance.AddRequest(actionCode, this);
	}

    protected void SendRequest(string data)
    {
        gamefacade.SendRequest(requestCode, actionCode, data);
    }

	public virtual void SendRequest()
    {

    }

    public virtual void OnResponse(string data)
    {

    }

    public virtual void OnDestroy()
    {
        if(GameFacade.Instance!=null)
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
