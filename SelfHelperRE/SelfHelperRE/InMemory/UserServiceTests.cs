using NUnit.Framework;
using RepositoryForTests;
using SelfHelperRE.Models;
using SelfHelperRE.ViewModels;
using Services;
using System.Threading.Tasks;

namespace InMemory
{
    [TestFixture]
    public class UserServiceTests
    {
        UserService<RegisterModel> userServiceRegistration;
        UserService<LoginModel> userServiceLogin;
        UserService<CheckEmail> userServiceEmail;
        UserService<UserData> userServiceData;

        WorkingWithUserForTest workingWithUserForTest = new WorkingWithUserForTest();

        [SetUp]
        public void Setup()
        {
            userServiceRegistration = new UserService<RegisterModel>(workingWithUserForTest);
            userServiceLogin = new UserService<LoginModel>(workingWithUserForTest);
            userServiceEmail = new UserService<CheckEmail>(workingWithUserForTest);
            userServiceData = new UserService<UserData>(workingWithUserForTest);
        }

        [Test]
        public async Task Add_User_Test()
        {
            RegisterModel registerModel = new RegisterModel { Login = "zzz", Email = "zzasd@mail.ru", Password = "1234" };

            await userServiceRegistration.AddUser(registerModel);

            string param = "registration";

            bool check = await userServiceRegistration.CheckUser(registerModel, param);

            Assert.AreEqual(true, check);
        }

        [Test]
        public async Task Check_User_For_Registration_Test()
        {
            RegisterModel registerModel = new RegisterModel { Login = "wqe", Email = "qwed@mail.ru" };

            string param = "registration";

            bool check = await userServiceRegistration.CheckUser(registerModel, param);

            Assert.AreEqual(false, check);
        }

        [Test]
        public async Task Check_User_Login_Test()
        {
            LoginModel loginModel = new LoginModel { Login = "zoldik", Password = "1111" };
            string param = "login";

            bool check = await userServiceLogin.CheckUser(loginModel, param);

            Assert.AreEqual(true, check);
        }

        [Test]
        public async Task Check_User_Email_For_Get_User_Data_Test()
        {
            CheckEmail loginModel = new CheckEmail { Email = "zoldikds@mail.ru"};
            string param = "email";

            bool check = await userServiceEmail.CheckUser(loginModel, param);

            Assert.AreEqual(true, check);
        }

        [Test]
        public async Task Get_Recovery_Data_Test()
        {
            UserData userData = new UserData { Login = "zoldik", Email = "zoldikds@mail.ru", Password = "1111" };

            UserData user = await userServiceData.GetData(userData);

            Assert.IsNotNull(user);
        }

    }
}