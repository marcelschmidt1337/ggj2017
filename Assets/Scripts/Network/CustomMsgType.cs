﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomMsgType
{
    JoinGroup = 41 + 8 + 1,
    LeaveGroup,
    JoinSide,
    LeaveSide,
    PlayerStatus,
    StartRowing,
    StopRowing,
    UpdateFromFinishedRow,

    Test = 99,
    RegisterView = 100
}