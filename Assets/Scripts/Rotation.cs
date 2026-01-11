using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class handles cube group rotation
 */
public class Rotation : MonoBehaviour
{
    public bool isActive;
    private int rotAngel = 0;
    private Vector3[][] rotation;
    public Transform[] blocks;
    public string type;
    private CubeArray cA;

    void Awake()
    {
        cA = Camera.main.GetComponent<CubeArray>();

        // Assign the 4 blocks of each group
        blocks = new Transform[4];
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks[i] = transform.GetChild(i);
        }

        GetRotByType(type);
    }

    // Perform rotation to the left
    public void RotateLeft()
    {
        rotAngel = GetRotAngle(rotAngel + 90);
        Rotate(rotAngel / 90);

        if (!cA.GetCubePositionFromScene())
        {
            RotateRight();
            GameObject.Find("CantMove").GetComponent<AudioSource>().Play();
        }
    }

    // Perform rotation clockwise
    public void RotateRight()
    {
        rotAngel = GetRotAngle(rotAngel - 90);
        Rotate(rotAngel / 90);

        if (!cA.GetCubePositionFromScene())
        {
            RotateLeft();
            GameObject.Find("CantMove").GetComponent<AudioSource>().Play();
        }
    }

    private int GetRotAngle(int angle)
    {
        if (angle < 0) return 360 + angle;
        if (angle > 270) return 0;
        return angle;
    }

    // Rotate the blocks to the specified position
    private void Rotate(int pos)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].localPosition = rotation[pos][i];
        }
    }

    // Get the rotation pattern by type of the group
    private void GetRotByType(string type)
    {
        if (type == "I")
        {
            Vector3[] rot0 = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(3, 0, 0) };
            Vector3[] rot90 = { new Vector3(2, 1, 0), new Vector3(2, 0, 0), new Vector3(2, -1, 0), new Vector3(2, -2, 0) };
            rotation = new Vector3[][] { rot0, rot90, rot0, rot90 };
        }
        else if (type == "T")
        {
            Vector3[] rot0 = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(1, -1, 0) };
            Vector3[] rot90 = { new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(1, -1, 0) };
            Vector3[] rot180 = { new Vector3(0, -1, 0), new Vector3(1, 0, 0), new Vector3(1, -1, 0), new Vector3(2, -1, 0) };
            Vector3[] rot270 = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0) };
            rotation = new Vector3[][] { rot0, rot90, rot180, rot270 };
        }
        else if (type == "L")
        {
            Vector3[] rot0 = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(0, -1, 0) };
            Vector3[] rot90 = { new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(1, -1, 0), new Vector3(2, -1, 0) };
            Vector3[] rot180 = { new Vector3(0, -1, 0), new Vector3(1, -1, 0), new Vector3(2, -1, 0), new Vector3(2, 0, 0) };
            Vector3[] rot270 = { new Vector3(0, 1, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0) };
            rotation = new Vector3[][] { rot0, rot90, rot180, rot270 };
        }
        else if (type == "L2")
        {
            Vector3[] rot0 = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(2, -1, 0) };
            Vector3[] rot90 = { new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(1, -1, 0), new Vector3(2, 1, 0) };
            Vector3[] rot180 = { new Vector3(0, -1, 0), new Vector3(1, -1, 0), new Vector3(2, -1, 0), new Vector3(0, 0, 0) };
            Vector3[] rot270 = { new Vector3(0, -1, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0) };
            rotation = new Vector3[][] { rot0, rot90, rot180, rot270 };
        }
        else if (type == "Z1")
        {
            Vector3[] rot0 = { new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(0, -1, 0), new Vector3(1, -1, 0) };
            Vector3[] rot90 = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, -1, 0) };
            rotation = new Vector3[][] { rot0, rot90, rot0, rot90 };
        }
        else if (type == "Z2")
        {
            Vector3[] rot0 = { new Vector3(1, 0, 0), new Vector3(0, 0, 0), new Vector3(2, -1, 0), new Vector3(1, -1, 0) };
            Vector3[] rot90 = { new Vector3(2, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 1, 0), new Vector3(1, -1, 0) };
            rotation = new Vector3[][] { rot0, rot90, rot0, rot90 };
        }
        else if (type == "O")
        {
            Vector3[] rot0 = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0) };
            rotation = new Vector3[][] { rot0, rot0, rot0, rot0 };
        }
    }
}
