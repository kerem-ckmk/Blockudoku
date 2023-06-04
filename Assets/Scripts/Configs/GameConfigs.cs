using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfigs", menuName = "Raising Game/Game Configs", order = 1)]
public class GameConfigs : ScriptableObject
{
    public static GameConfigs Instance;

    [Header("Level")]
    public int LevelSkipCountAtRepeat = 0;

    [Header("Economy")]
    public int StartingMoney = 0;

    [Header("Tile")]
    public float TileSize = 1f;
    public float TileMouseDragOffset = 50f;
    public float TileDragSpeed = 10f;
    public float TileDragScale = 1.72f;
    public int BlockValue = 50;

    [Header("Haptic")]
    public float HapticIntervalLimit = 0.15f;

    [Header("Cheats")]
    public int AddCurrencyCheatAmount = 5000;

    [Header("Creative Specific")]
    public bool EnableCursorImage = false;

    [Header("Sounds")]
    public float SoundIntervalLimit = 0.1f;
    public float SoundVolume = 0.5f;
    public float SoundVolumeMultiplier = 1f;
    public AudioClip ButtonSound;

    public void Initialize()
    {
        Debug.Assert(Instance == null);

        Instance = this;
    }
}
