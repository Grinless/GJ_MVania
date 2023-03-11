using UnityEngine;

[System.Serializable]
public class PlayerInputAlloc
{
    public KeyCode keyA, keyB;

    public bool Key()
    {
        if (Input.GetKey(keyA))
        {
            return true;
        }

        if (Input.GetKey(keyB))
        {
            return true;
        }

        return false;
    }

    public bool KeyDown()
    {
        if (Input.GetKeyDown(keyA))
        {
            return true;
        }

        if (Input.GetKeyDown(keyB))
        {
            return true;
        }

        return false;
    }

    public bool KeyRelease()
    {
        if (Input.GetKeyUp(keyA))
        {
            return true;
        }

        if (Input.GetKeyUp(keyB))
        {
            return true;
        }

        return false;
    }
}
