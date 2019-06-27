namespace Awar.Tasks
{
    public interface ITask
    {
        string Name { get; set; }
        bool InProgress { get; set; }

        void Execute();

        void Tick();
    } 
}
