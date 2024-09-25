using AutoMapper;
using DomainAPI;
using DomainAPI.Controllers;
using DomainAPI.DTO;
using DomainAPI.Models;
using DomainAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTestProject
{
    public class UnitTestController
    {
        private readonly Mock<IUserRepository> userRepository;
        private readonly IMapper _mapper;
        public UnitTestController()
        {
            userRepository = new Mock<IUserRepository>();
            var mapperProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            _mapper = configuration.CreateMapper();
        }
        [Fact]
        public void GetUserList_User()
        {
            //arrange
            var userList = GetUserData();
            userRepository.Setup(x => x.GetUsers())
                .ReturnsAsync(userList.AsEnumerable<User>);
            var userController = new UserController(userRepository.Object, _mapper);
            //act
            var userResult = userController.Get();

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(userResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(userResult.Result);
            Assert.Equal(userList, model);


        }
        [Fact]
        public void GetUserByID_User()
        {
            //arrange
            var userList = GetUserData();
            userRepository.Setup(x => x.GetUser(2))
                .ReturnsAsync(userList[1]);
            var userController = new UserController(userRepository.Object, _mapper);
            //act
            var userResult = userController.GetById(2);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(userResult.Result);
            var model = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(userList[1], model);
        }


        [Fact]
        public async Task AddUser_User()
        {
            //arrange
            var userList = GetUserData();
            userRepository.Setup(x => x.AddUser(It.IsAny<User>()))
                .ReturnsAsync(new User { Id = 7, Name = "efef", Age = 33, Gender = "Female", Occupation = "eef" });

            var userController = new UserController(userRepository.Object, _mapper);

            //act

            var newUser = _mapper.Map<UserDTO>(userList[2]);

            var userResult = await userController.Post(newUser);
            //assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(userResult);
            var model = Assert.IsAssignableFrom<User>(createdResult.Value);
        }

        private List<User> GetUserData()
        {
            List<User> usersData = new List<User>
        {
            new User
            {
                Name = "User1",
                Age = 32,
                Gender = "Male",
                Occupation = "Doctor"
            },
             new User
            {
                Name = "User2",
                Age = 33,
                Gender = "Female",
                Occupation = "Scientist"
            },
             new User
            {
                Name = "User2",
                Age = 40,
                Gender = "Male",
                Occupation = "Painter"
            },
        };
            return usersData;
        }
    }
}