using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX_List")]
public class SO_SFXList : ScriptableObject
{
    public List<SoundEffect> soundEffects;

    private Dictionary<SFX_Type, AudioClip> soundDictionary;

    private void OnEnable()
    {
        if(soundDictionary == null)
        {
            soundDictionary = new Dictionary<SFX_Type, AudioClip>();

            //Rellena el diccionario con la lista de SFX
            foreach (var soundEffect in soundEffects)
            {
                if (!soundDictionary.ContainsKey(soundEffect.type))
                {
                    soundDictionary.Add(soundEffect.type, soundEffect.SFX);
                }
            }
        }

    }

    //Devuelve un sonido asociado a un tipo de SFX desde cualquier sitio. 
    public AudioClip GetClip(SFX_Type SFX_Type)
    {
        if (soundDictionary.ContainsKey(SFX_Type))
            return soundDictionary[SFX_Type];
        else
        {
            Debug.Log("Sonido no encontrado, no se ha rellenado bien la lista del SO");
            return null;
        }
    }
}

public enum SFX_Type
{
    
}

[System.Serializable]
public class SoundEffect
{
    [SerializeField]
    public AudioClip SFX;

    [SerializeField]
    public SFX_Type type;
}
