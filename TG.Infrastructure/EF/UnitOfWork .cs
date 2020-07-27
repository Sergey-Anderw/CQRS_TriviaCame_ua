using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TG.Domain.Entities;

namespace TG.Infrastructure.EF
{
	public class UnitOfWork : IDisposable
	{
        private ApplicationDbContext _context;
        private  GenericRepository<CategoryEntity> _categoryRepository;
        private GenericRepository<PlayerEntity> _playerRepository;
        private GenericRepository<GameplayRoomEntity> _gameplayRoomrRepository;

        public UnitOfWork(ApplicationDbContext context)
		{
            _context = context;

        }
        public GenericRepository<CategoryEntity> CategoryRepository
        {
            get
            {

                if (_categoryRepository == null)
                {
                    _categoryRepository = new GenericRepository<CategoryEntity>(_context);
                }
                return _categoryRepository;
            }
        }

        public GenericRepository<PlayerEntity> PlayerRepository
        {
            get
            {

                if (_playerRepository == null)
                {
                    _playerRepository = new GenericRepository<PlayerEntity>(_context);
                }
                return _playerRepository;
            }
        }

        public GenericRepository<GameplayRoomEntity> GameplayRoomRepository
        {
            get
            {

                if (_gameplayRoomrRepository == null)
                {
                    _gameplayRoomrRepository = new GenericRepository<GameplayRoomEntity>(_context);
                }
                return _gameplayRoomrRepository;
            }
        }

        public async Task<int> Save()
        {
           return await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
