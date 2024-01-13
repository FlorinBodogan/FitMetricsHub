using AutoMapper;
using BACKEND.Interfaces;

namespace BACKEND.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);
        public IBmiRepository BmiRepository => new BmiRepository(_context, _mapper);
        public IRmbRepository RmbRepository => new RmbRepository(_context, _mapper);
        public IArterialTensionRepository ArterialTensionRepository => new ArterialTensionRepository(_context, _mapper);
        public ICholesterolRepository CholesterolRepository => new CholesterolRepository(_context, _mapper);
        public ITriglyceridesRepository TriglyceridesRepository => new TriglyceridesRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}