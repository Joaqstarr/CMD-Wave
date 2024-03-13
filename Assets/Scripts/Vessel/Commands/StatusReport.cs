using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusReport : CommandBase
{
    
    private Room[] _rooms;

    public override string[] Execute(out CommandContext overrideContext, out AudioClip sfx, string arg = null)
    {
        sfx = _soundWhenExecuted;

        overrideContext = null;
        /*
        string[] result = new string[CreateBounds()];
        Debug.Log(_rooms.Length);

        for (int i = 0; i < result.Length; i += 4)
        {
            result[i] = "";
            result[i + 1] = "";
            result[i + 2] = "";
            result[i + 3] = "";

            for (int k = 0; k < _leftBound * 5; k++)
            {
                    result[i] += " ";
                    result[i +1] += " ";
                    result[i+2] += " ";
                    result[i+3] += " ";

            }
            result[i] += " ___ ";
            result[i + 1] += "/"+ _rooms[0].RoomTag + " \\";
            result[i + 2] += "\\100/";
            result[i + 3] += " ‾‾‾‾ ";


        }
        return result;
        */
        return CommandLineManager.StringToArray( "s");
    }

    /*
    private int CreateBounds()
    {
        _rooms = GetComponentsInChildren<Room>();

        int lowestRoom = 0;
        int leftestRoom = 0;
        int rightestRoom = 0;

        for(int i = 0; i < _rooms.Length; i ++)
        {
            if(_rooms[i].Position[0] < leftestRoom)
            {
                leftestRoom = _rooms[i].Position[0];
            }
            if (_rooms[i].Position[0] > rightestRoom)
            {
                rightestRoom = _rooms[i].Position[0];
            }
            if (_rooms[i].Position[1] > lowestRoom)
            {
                lowestRoom = _rooms[i].Position[1];
            }
        }

        _leftBound = leftestRoom;
        _rightBound = rightestRoom;
        return (lowestRoom * 4) + 4;
    }
    */
}
