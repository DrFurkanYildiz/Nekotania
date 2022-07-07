using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OlaylarSO : ScriptableObject
{
    public enum EventName
    {
        Tutorial = 0,
        Olay1,
        Olay2,
        Olay3,
        Olay4,
        Olay5,
        Olay6,
        Olay7,
        Olay8,
        Olay9,
        Olay10,
        Olay11,
        Olay12,
        Olay13,
        Olay14,
        Olay15,
        Olay16,
        Olay17,
        Olay18,
        Olay19,
        Olay20,
        Olay21,
        Olay22,
        Olay23
    }
    public enum EventAnswers
    {
        Empty,
        Answer1,
        Answer2,
        Answer3
    }
    public enum EventType
    {
        AnaOlay,
        AraOlay
    }
    [SerializeField] private string OlayIsmi;
    public EventName MyEventName;
    public EventType MyEventType;
    public int KacinciGunAnaOlayi;
    public LeanPhrase olayBasligiPhrase;
    public LeanPhrase olayAciklamasiPhrase;
    public List<LeanPhrase> olayCevaplariPhraseListesi = new List<LeanPhrase>();
    public List<LeanPhrase> olaySonucAciklamaListesi = new List<LeanPhrase>();

}