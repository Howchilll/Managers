using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeGame : MonoBehaviour
{
  private void Awake()
  {
    GameManager.InitAll();
  }
}
