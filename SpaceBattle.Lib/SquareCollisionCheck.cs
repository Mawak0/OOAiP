using App;

namespace SpaceBattle.Lib
{
    public class SquareCollisionCheckCommand : ICommand
    {
        private readonly IColliding collidingObj;

        public SquareCollisionCheckCommand(IColliding collidingObj)
        {
            this.collidingObj = collidingObj;
        }

        public void Execute()
        {
            var nearbyObjects = Ioc.Resolve<IEnumerable<IColliding>>("Game.GetObjectsInSameSquare", collidingObj);
            nearbyObjects
                .Select(obj => Ioc.Resolve<ICommand>("Game.CollisionCommand", collidingObj, obj, Ioc.Resolve<ICommand>("Game.HandleCollision", collidingObj, obj)))
                .ToList()
                .ForEach(cmd => cmd.Execute());
        }
    }
}
