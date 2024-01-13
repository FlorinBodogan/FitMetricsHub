namespace BACKEND.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IBmiRepository BmiRepository { get; }
        IRmbRepository RmbRepository { get; }
        IArterialTensionRepository ArterialTensionRepository { get; }
        ICholesterolRepository CholesterolRepository { get; }
        ITriglyceridesRepository TriglyceridesRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}