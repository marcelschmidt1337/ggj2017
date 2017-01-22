using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomMsgType
{
	JoinGroup = 50,
	LeaveGroup,
	JoinSide,
	LeaveSide,
	PlayerStatus,
	StartRowing,
	StopRowing,
	UpdateFromFinishedRow,
	UpdateGameState,
	Test = 99,
	RegisterView = 100
}
