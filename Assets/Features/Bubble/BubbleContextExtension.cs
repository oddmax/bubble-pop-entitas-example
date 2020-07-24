using UnityEngine;

public static class BubbleContextExtension
{
    public static GameEntity CreateRandomBoardBubble(this GameContext context, int x, int y)
    {
        var randomNumber = 1 << Rand.game.Int(1, 10);
        return CreateBoardBubble(context, randomNumber, x, y);
    }
    
    public static GameEntity CreateBoardBubble(this GameContext context, int number, int x, int y)
    {
        var entity = context.CreateEntity();
        entity.isBubble = true;
        entity.AddBubbleNumber(number);
        entity.AddPosition(new Vector2Int(x, y));
        entity.AddAsset("Bubble");
        return entity;
    }
    
    public static GameEntity CreateLauncherBubble(this GameContext context)
    {
        var entity = context.CreateEntity(); 
        entity.isBubble = true;
        entity.isLaunchBubble = true;
        var randomNumber = 1 << Rand.game.Int(1, 6);
        entity.AddBubbleNumber(randomNumber);
        entity.AddTransformPosition(GameObject.FindWithTag("Launcher").transform.position);
        entity.AddAsset("Bubble");
        return entity;
    }
    
    public static GameEntity SpawnEffect(this GameContext context, Vector2 position, int Number)
    {
        var entity = context.CreateEntity();
        entity.isParticle = true;
        entity.AddTransformPosition(position);
        entity.AddAsset("ParticleEffect");
        entity.AddBubbleNumber(Number);
        return entity;
    }
    
    public static GameEntity SpawnTextEffect(this GameContext context, Vector2 position, int Number)
    {
        var entity = context.CreateEntity();
        entity.isTextSpawn = true;
        entity.AddTransformPosition(position);
        entity.AddAsset("SpawnText");
        entity.AddBubbleNumber(Number);
        return entity;
    }
}
