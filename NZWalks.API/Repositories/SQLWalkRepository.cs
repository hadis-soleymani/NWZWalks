using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        #region Init
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region CreateAsync
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
        #endregion

        #region GetAllAsync
        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null
            , string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks
                .Include(c => c.Difficulty)
                .Include(c => c.Region)
                .AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(c => c.Name.Contains(filterQuery));
                }
            }

            //sort
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(c => c.Name) : walks.OrderByDescending(c => c.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(c => c.LengthInKm) : walks.OrderByDescending(c => c.LengthInKm);
                }
            }

            //pagination

            var skipResults = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }
        #endregion

        #region GetByIdAsync
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks
                .Include(c => c.Difficulty)
                .Include(c => c.Region)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (walk == null)
            {
                return null;
            }
            return walk;
        }
        #endregion

        #region UpdateAsync
        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(c => c.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
        #endregion

        #region DeleteAsync
        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks
                .Include(c => c.Difficulty)
                .Include(c => c.Region)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }
        #endregion
    }
}
