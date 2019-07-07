namespace Awar.Tasks
{
    public interface IInteractable
    {
        bool Targeted { get; set; }

        bool Targetable();
    }
}
