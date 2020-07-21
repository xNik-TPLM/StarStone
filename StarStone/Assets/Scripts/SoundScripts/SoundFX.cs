using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reference: Dark Souls III Soundtrack OST (2015) - Main Menu Theme. Available: https://www.youtube.com/watch?v=stWae6r7Blw [Last Accessed 13th July 2020].
/// Reference: TES V Skyrim Soundtrack (2012) - Ancient Stones. Available: https://www.youtube.com/watch?v=mmZGrvAvPZM [Last Accessed 13th July 2020].
/// Reference: Footstep on Stone (2013) - Walking. Available: https://freesound.org/people/Samulis/sounds/197778/ [Last Accessed 15th July 2020].
/// Reference: Weapons of Choice Unity Asset Store (2017) - PrimaryFire & PrimaryHandling & PrototypeFire. Available: https://assetstore.unity.com/packages/audio/sound-fx/weapons/weapons-of-choice-free-101807 [Last Accessed 13th July 2020].
/// Reference: RPG Playlist - Peaceful/Travel Music (2016) - CutsceneMusic. Available: https://www.youtube.com/watch?v=A8qMyBWZNw0 [Last Accessed 18th July 2020].
/// Reference: Grenade Sound FX Unity Asset Store (2019) - Explosion. Available: https://assetstore.unity.com/packages/audio/sound-fx/grenade-sound-fx-147490 [Last Accessed 14th July 2020].
/// This script handles the music and sound effects used in the game
/// Worked By: Ben Smith
/// </summary>
public class SoundFX : MonoBehaviour
{
    // Public AudioSources
    public AudioSource Explosion; // This plays when the offensive ability is used
    public AudioSource PrimaryFire; // This plays when the primary weapon is being fired
    public AudioSource PrimaryHandling; // This plays when the primary weapon is reloading
    public AudioSource PrototypeFire; // This plays when the prototype weapon is firing
    public AudioSource PlayerWalk; // This plays when the player is walking
    public AudioSource PlayerRun; // This plays when the player is running
    public AudioSource MenuTheme; // This plays when the main menu is active
    public AudioSource CutsceneTheme; // This plays when a cutscene is active
}
