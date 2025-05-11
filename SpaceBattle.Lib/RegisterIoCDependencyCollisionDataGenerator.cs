// using App;
// using SpaceBattle.Lib;

// public class RegisterIoCDependencyCollisionDataGenerator : App.ICommand
// {
//     public void Execute()
//     {
//         Ioc.Resolve<App.ICommand>(
//             "IoC.Register",
//             "CollisionDataGenerator",
//             (object[] args) => new CollisionDataGenerator((List<Ray>) args[0], (List<Polygon>) args[1])
//         ).Execute();
//     }
// }
