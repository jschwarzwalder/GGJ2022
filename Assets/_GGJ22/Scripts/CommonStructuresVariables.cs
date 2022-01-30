using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CommonStructuresVariables : MonoBehaviour
{
    public static int maxPoints = 50;
    public static int minPoints = 15;
    public static Dictionary<AnimationCode,string> AnimationCodeName = new Dictionary<AnimationCode, string>()
    {
        {AnimationCode.crash, "Crash"},
        {AnimationCode.death, "Death"},
        {AnimationCode.flyingFront, "FlyingFront"},
        {AnimationCode.flying, "Flying"},
        {AnimationCode.grab, "Grab"},
        {AnimationCode.hold, "Hold"},
        {AnimationCode.idle, "Idle"},
        {AnimationCode.intro, "Intro"},
        {AnimationCode.push, "Push"},
        {AnimationCode.selected, "Selected"},
        {AnimationCode.struggle, "Struggle"},
        {AnimationCode.walk, "Walk"},
        {AnimationCode.win, "Win"}
    };
    /*public static List<AnimationCode> Triggers = new List<AnimationCode>{
        AnimationCode.GrabTrigger, AnimationCode.StartGame, AnimationCode.Intro
    };*/

    public static Dictionary<SceneCode, string> ScenesNames = new Dictionary<SceneCode, string> ()
    {
        {SceneCode.MainMenu, "MainMenu"},
        {SceneCode.Selection, "SelectionMenu"},
        {SceneCode.Game, "GameScene"},
        {SceneCode.Score, "EndGame"},
    };

}

public enum AnimationCode
{
    crash, death, flyingFront, flying,
    grab, hold, idle, intro, push, selected,
    struggle, walk, win, None
}

public enum SceneCode
{
    StartScene ,MainMenu, Selection, Game, Score, PameBD 
}

public enum BehaviorState
{
    Idle, Normal, Hugging, BeingHug, Throwed, Damage, win, Podium, Stopped, StartHug
}

public enum CameraRumbleId
{
    Death, Danger, Damage, BeingDamage, Roar
}

public enum RumbleId
{
    normal, interaction, tap, inDanger, death, hugging, hugged, crash
}

public enum RumbleType
{
    normal, fadeOut, fadeIn, tap, keep,
}

public enum SelectionState
{
    Available, Joined, Ready
}

public enum AudioKeys
{
    MENU_THEME, LOOP_BASE, FINAL_STAGE, SWITCH_ARROW, ENTER, SWITCH_FIGHTER, CHARGE_HUG, BUMP, AWAY, CRASH, POINTS, POINTS_X2, PENALIZATIONS,five_SEC_COUNTDOWN, GAME_OVER, STAR_POWER_UP, PAUSE,One_VS_1, MUSIC_TRANSITION, TIMES_UP, READY, VICTORY
}

//still not sure of this
public enum ActionIds
{

}

public enum ButtonCode
{
    upBtn, downBtn, leftBtn, rightBtn,
    upDir, downDir, leftDir, rightDir
}

[System.Serializable]
public class InputBtnInfo
{
    public Sprite Button;
    public Sprite PressedButton;
    public string deviceName;
    public bool keyboardConfig1;

}

[System.Serializable]
public struct BounceInfo
{
    public Vector3 position;
    public int playerId;

    public BounceInfo(Vector3 _position, int _playerId)
    {
        position = _position;
        playerId = _playerId;
    }
}

[System.Serializable]
public struct AnimStep
{
    public Vector3 position;
    public int playerId;
}

//this should be a serialized item
[System.Serializable]
public class RumbleProfile
{
    public RumbleId rumbleId;
    public float time;
    public bool canBeInterrupt;
    public RumbleType rumbleStyle;
    public float force;

}

[System.Serializable]
public class RumbleCameraProfile
{
    public CameraRumbleId rumbleId;
    public float time;
    public bool canBeInterrupt;
    public RumbleType rumbleStyle;
    public float amplitude;
    public float frequency;
}

[System.Serializable]
public class KaijuScoreInfo
{
    public float points;
    public int lives;
    public Image[] livesUI;
    public TMP_Text pointsText;
    public GameObject frame;
    public Image portrait;
}

[System.Serializable]
public class PodiumObject
{
    public List<GameObject> building;
    public Transform pos;
}

[System.Serializable]
public class AudioInfo
{
    public AudioKeys audioID;
    public AudioClip[] clip;
}

[System.Serializable]
public class KawaiijuCallInfo
{
    //A caller is always a player. For now who knows iff we have bosses or something
    public Player caller;
    public Transform receptor;

    public BehaviorState stateChange;
}

[System.Serializable]
public class PlayerInfo
{
    //A caller is always a player. For now who knows iff we have bosses or something
    public Player caller;
    public Transform receptor;

    public BehaviorState stateChange;
}