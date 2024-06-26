using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private int _x;
    public int x
    {
        get { return _x; }
        set { _x = value; }
    }
    private int _y;
    public int y
    {
        get { return _y; }
        set { _y = value; }
    }
    private int _playerOnSlot;
    public int playerOnSlot
    {
        get { return _playerOnSlot; }
        set { _playerOnSlot = value; }
    }

}
