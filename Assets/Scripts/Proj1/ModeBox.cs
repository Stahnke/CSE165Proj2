using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeBox : MonoBehaviour {

    /*mode list
     * 0 = Normal
     * 1 = Laser
     * 2 = Cannon
     */

    public int mode;

    public int Switch()
    {
        return mode;
    }
}
