using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] bool solved = false;

    public bool Solved { get => solved; set => solved = value; }
}
