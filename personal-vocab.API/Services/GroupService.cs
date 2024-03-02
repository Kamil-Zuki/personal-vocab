using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personal_vocab.DAL.DataAccess;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs;
using personal_vocab.Interfeces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;

namespace personal_vocab.Services
{
    public class GroupService : IGroupSevice
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;


        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<GroupService> _logger;

        public IServiceScope scope { get; set; }
        public GroupService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<GroupService> logger, IServiceProvider serviceProvider)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
            _serviceProvider = serviceProvider;
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
                Console.WriteLine("GetUserIds starts");
                var factory = new ConnectionFactory
                {
                    HostName = _configuration.GetSection("RabbitMq:HostName").Get<string>(),
                    Port = 5672,
                    UserName = _configuration.GetSection("RabbitMq:UserName").Get<string>(),
                    Password = _configuration.GetSection("RabbitMq:Password").Get<string>()
                };

                var usersToAdd = new List<long>();  // Accumulate the user IDs

                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "user-information", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += ReceivedHandler;

                channel.BasicConsume(queue: "user-information",
                            autoAck: true,
                            consumer: consumer);

                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw new Exception(ex.Message);
            }
        }
        private async void ReceivedHandler(object? sender, BasicDeliverEventArgs ea)
        {
            try
            {
                if (scope == null)
                    scope = _serviceProvider.CreateScope();

                byte[]? body = ea.Body.ToArray();
                string? message = Encoding.UTF8.GetString(body);
                string? routingKey = ea.RoutingKey;

                DataContext? dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                User? user = dbContext.Users.FirstOrDefault(x => x.Id == Convert.ToInt64(message));
                if (user == null)
                {
                    dbContext.Users.Add(new User { Id = Convert.ToInt64(message) });
                    await dbContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

}

