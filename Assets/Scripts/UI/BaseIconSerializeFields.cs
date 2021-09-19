using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseIconSerializeFields : MonoBehaviour
{
    public Button Button => button;

    [SerializeField]
    private Button button = null;
}
