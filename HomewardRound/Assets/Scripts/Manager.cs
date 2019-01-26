using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour //Singleton<Manager>
{
    public float m_MoveSpeed = 3.0f;

    public static Manager instance;
}
