using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personal_vocab.DAL.DataAccess;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs;
using personal_vocab.Interfeces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Security.Claims;
using System.Text;

namespace personal_vocab.Services
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


        public async Task<List<GroupDTO>> GetAsync()
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
                return await _dataContext.Groups.Where(e => e.Id == id).Select(e => new GroupDTO()
                {
                    Id = e.Id,
                    Name = e.Name,
                    NewWordAmount = e.NewWordAmount,
                    RepeatedWordAmount = e.RepeatedWordAmount
                }).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task PatchAsync(int id, [FromBody] JsonPatchDocument<GroupDTO> patchDoc)
        {
            try
            {
                long userId = Convert.ToInt64(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (await _dataContext.Users.FindAsync(userId) == null)
                    await _dataContext.Users.AddAsync(new User() { Id = userId });

                var existingGroup = _dataContext.Groups.FirstOrDefault(e => e.Id == id);
                if (existingGroup == null)
                    throw new Exception("The deck hasn't been found.");

                var groupDTO = new GroupDTO()
                {
                    Id = existingGroup.Id,
                    Name = existingGroup.Name,
                    NewWordAmount = existingGroup.NewWordAmount,
                    RepeatedWordAmount = existingGroup.RepeatedWordAmount,

                };
                patchDoc.ApplyTo(groupDTO);

                existingGroup.Id = groupDTO.Id;
                existingGroup.Name = groupDTO.Name;
                existingGroup.NewWordAmount = groupDTO.NewWordAmount;
                existingGroup.RepeatedWordAmount = groupDTO.RepeatedWordAmount;
                existingGroup.UserId = userId;

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
                if (group == null)
                    throw new Exception("The term hasn't been found.");

                _dataContext.Groups.Remove(group);
                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task GetUserIds()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "1234"
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "user-information",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                var userIds = new List<long>();
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    if (long.TryParse(message, out var userId))
                    {
                        userIds.Add(userId);
                    }
                };

                channel.BasicConsume(queue: "user-information",
                                     autoAck: true,
                                     consumer: consumer);
                userIds = userIds.Distinct().ToList();

                var existentUserIds = await _dataContext.Users.Where(e => userIds.Contains(e.Id)).Select(x => x.Id).ToListAsync();
                var noExistingUserIds = userIds.Except(existentUserIds);
                if (noExistingUserIds.Count() == 0)
                    throw new Exception("All users exist already");

                var users = noExistingUserIds.Select(x => new User()
                {
                    Id = x
                }).ToArray();

                if (userIds.Count == 0)
                    throw new Exception("No user ids were received");

                foreach (var user in users)
                {
                    await _dataContext.Users.AddAsync(user);
                }

                 await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
