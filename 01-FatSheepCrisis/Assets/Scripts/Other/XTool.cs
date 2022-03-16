using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class XTool
{
    public static Vector3 RangeInsideCirclePosition(Vector3 _pos, float minR, float maxR)
    {
        Vector3 pos = _pos;
        while (Vector3.Distance(pos, _pos) < minR || Vector3.Distance(pos, _pos) > maxR)
        {
            pos = new Vector3(Random.Range(_pos.x + maxR, _pos.x - maxR), Random.Range(_pos.y + maxR, _pos.y - maxR), 0);
        }
        return pos;
    }
}
