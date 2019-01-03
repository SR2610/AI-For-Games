public static class AIConstants  {

    public static float ChaseEnemyChance = 0.5F; //Chance out of 100 to chase the enemy and attempt to attack them if they see them (0.5 = 50%)

    public static int HealThreshold = 25; //health percentage that the AI should flee and attempt to heal at (25 = 25%)

    public static float AttackCooldown = 0.1F; //How long the AI Agent Has to wait before attacking next in seconds

    public static float BaseDistanceThreshold = 2.5F; //How close they have to be to a base to say that they are there

    public static float FleeChance = 0.3F; //Chance for the agent to flee at low health (0.3 = 30%)
}
