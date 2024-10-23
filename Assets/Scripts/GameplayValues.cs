namespace DefaultNamespace
{
    public static class GameplayValues
    {
        public const float SpawnGap = 0.05f;
        
        public const float MoveSpeed = 5f; 
        public const float IncreasedMoveSpeed = 30f;
        public const float SpeedUpTime = 5000;

        public const float JumpForce = 7f;      
        public const float GroundCheckRadius = 0.2f;
        public const float PlayerSpawnShift = 0.5f;
        
        public const int GroundLayerMask = 64;
        public const int PlayerRespawnDelay = 300;
        public const int ActionDelay = 1000;

        public const string GroundCheckTag = "GroundCheck";
        public const string EvilCloneTag = "EvilClone";
        public const string PlayerTag = "Character";

        public static bool IsSpeedIncreased { get; private set; }

        public static void SetSpeedIncreaseStatus(bool enabled)
        {
            IsSpeedIncreased = enabled;
        }
    }
}