using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager {
    public UserData userData;

    public PlayerManager(GameFacade facade) : base(facade) { }

    public UserData UserData
    {
        set { userData = value; }
        get { return userData;  }
    }
}
