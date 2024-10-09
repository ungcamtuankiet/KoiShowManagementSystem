using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Entites;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class KoiRepository : IKoiRepository
    {
        private readonly KoiShowManagementSystemContext _context;

        public KoiRepository(KoiShowManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<KoiFish>> GetListKoiFish()
        {
            return _context.KoiFishes.ToList();
        }

        public async Task<KoiFish> GetKoiFishById(int id)
        {
            return await _context.KoiFishes.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<KoiFish> RegisterKoi(KoiFish fish)
        {
            _context.KoiFishes.Add(fish);
            await _context.SaveChangesAsync();
            return fish;
        }

        public async Task<KoiFish> UpdateKoi(KoiFish fish)
        {
            _context.KoiFishes.Update(fish);
            await _context.SaveChangesAsync();
            return fish;
        }
        public async Task<KoiFish> DeleteKoi(KoiFish fish)
        {
            _context.KoiFishes.Remove(fish);
            await _context.SaveChangesAsync();
            return fish;
        }
    }
}
