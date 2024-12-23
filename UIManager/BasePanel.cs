using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasePanel : MonoBehaviour  //UI»ù´¡Àà
{
    public virtual void Hide()    { this.gameObject.SetActive(false);}
    public virtual void Show()    { this.gameObject.SetActive(true); }

}
