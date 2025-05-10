using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewsData", menuName="ScriptableObjects/NewsData")]
public class NewsData : ScriptableObject
{
    [TextArea(order = 8)]
    public string headline;
    [Range(1, 3)]
    public int tier;
    public Sprite image;
    public bool isReal;
    public int followersGained;
    public float credibilityGained;
}
