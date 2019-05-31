using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using ContactApp.ServiceInterface;
using ContactApp.ServiceModel;
using ContactApp.ServiceModel.Data.Models;

namespace ContactApp.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<ContactServices>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public void Can_call_MyServices()
        {
            //var service = appHost.Container.Resolve<ContactServices>();

            //var response = (GetContactsResponse)service.Any(new Contact { ID = 1, Name = "Anh", Address = "HCM" });

            //Assert.That(response.Results, Is.EqualTo("Hello, World!"));
        }
    }
}
