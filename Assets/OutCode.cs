using UnityEngine;

public class OutCode
{
    public bool up, down, left, right;

    public OutCode(Vector2 v)
    {
        up = v.y > 1;
        down = v.y < -1;
        left = v.x < -1;
        right = v.x > 1;
    }

    public OutCode()
    {
        up = down = left = right = false;
    }

    public OutCode(OutCode oc)
    {
        up = oc.up;
        down = oc.down;
        left = oc.left;
        right = oc.right;
    }

    public OutCode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public static OutCode operator +(OutCode a, OutCode b) //OR
    {
        return new OutCode(a.up || b.up, a.down || b.down, a.left || b.left, a.right || b.right);
    }

    public static OutCode operator *(OutCode a, OutCode b) //AND
    {
        return new OutCode(a.up && b.up, a.down && b.down, a.left && b.left, a.right && b.right);
    }

    public static bool operator ==(OutCode a, OutCode b) //EQUAL
    { 
        return (a.up == b.up) && (a.down == b.down) && (a.left == b.left) && (a.right == b.right);
    }

    public static bool operator !=(OutCode a, OutCode b) //NOT EQUAL
    {
        return !(a == b);
    }

    public override string ToString()
    {
        //return $"Up: {up} Down: {down} Left: {left} Right: {right}";
        return (up ? "1" : "0") + (down ? "1" : "0") + (left ? "1" : "0") + (right ? "1" : "0");
    }
}
