using Microsoft.EntityFrameworkCore;
using sm_repetition_algorithm.DAL.DataAccess;
using sm_repetition_algorithm.DAL.Entitis;
using sm_repetition_algorithm.DTOs;
using sm_repetition_algorithm.Interfeces;
using System.Security.Claims;

namespace sm_repetition_algorithm.Services
{
    public class GroupService : IGroupSevice
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GroupService(DataContext dataContext, IHttpContextAccessor httpContextAccessor) 
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateAsync(NoIdGroupDTO group)
        {
            try
            {
                long userId = Convert.ToInt64(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (await _dataContext.Users.FindAsync(userId) == null)
                    await _dataContext.Users.AddAsync(new User() { Id = userId });
                await _dataContext.Groups.AddAsync(new Group()
                {
                    Name = group.Name,
                    NewWordAmount = group.NewWordAmount,
                    RepeatedWordAmount = group.RepeatedWordAmount,
                    UserId = userId
                });
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<GroupDTO>> GetAllAsync()
        {
            try
            {
                return (await _dataContext.Groups.ToListAsync()).Select(x => new GroupDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NewWordAmount = x.NewWordAmount,
                    RepeatedWordAmount = x.RepeatedWordAmount
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GroupDTO> GetAsync(int id)
        {
            try
            {
                var group = await _dataContext.Groups.FindAsync(id);

                return new GroupDTO()
                {
                    Id = group.Id,
                    Name = group.Name,
                    NewWordAmount = group.NewWordAmount,
                    RepeatedWordAmount = group.RepeatedWordAmount
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(GroupDTO group)
        {
            try
            {
                long userId = Convert.ToInt64(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (await _dataContext.Users.FindAsync(userId) == null)
                    await _dataContext.Users.AddAsync(new User() { Id = userId });
                _dataContext.Groups.Update(new Group()
                {
                    Id = group.Id,
                    Name = group.Name,
                    NewWordAmount = group.NewWordAmount,
                    RepeatedWordAmount = group.RepeatedWordAmount,
                    UserId = userId
                });
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var group = await _dataContext.Groups.FirstOrDefaultAsync(e => e.Id == id);
                if (group != null)
                {
                    _dataContext.Groups.Remove(group);
                    await _dataContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
