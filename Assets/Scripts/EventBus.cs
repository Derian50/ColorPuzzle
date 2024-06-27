using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static Action onSwapTiles;

    public static Action onWin;

    public static Action<int> onClickButtonLevel;

    public static Action<int> onLevelCompleted;

}
