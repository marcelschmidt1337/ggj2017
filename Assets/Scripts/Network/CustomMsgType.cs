using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CustomMsgType
{
    JoinGroup = MsgType.Highest + 1,
    LeaveGroup,
    JoinSide,
    LeaveSide,
    PlayerStatus,
    StartRowing,
    StopRowing,

    Test = 99,
    RegisterView = 100
}
