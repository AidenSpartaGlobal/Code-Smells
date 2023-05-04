using NUnit.Framework;
using Moq;
using NorthwindBusiness;
using NorthwindData;
using NorthwindData.Services;
using System.Data;
using System.Collections.Generic;

namespace NorthwindTests
{
    public class CustomerManagerShould
    {
        private CustomerManager _sut;

        //Dummy
        [Test]
        [Category("happy")]
        public void BeAbleToConstructed()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            //Act
            _sut = new CustomerManager(mockCustomerService.Object);
            //Assert
            Assert.That(_sut, Is.InstanceOf<CustomerManager>());
        }

        //stubs


        [Test]
        public void ReturnTrue_WhenUpdateIsCalled_WithValidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer
            {
                CustomerId = "MANDAL"
            };
            mockCustomerService.Setup(cs => cs.GetCustomerById("MANDAL")).Returns(originalCustomer);
            // Act
            _sut = new CustomerManager(mockCustomerService.Object);
            var result = _sut.Update("MANDAL", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            //Assert

            Assert.That(result);
        }



        [Test]
        [Category("happy")]
        public void UpdateSelectedCustomer_WhenUpdateIsCalled_WithValidId()
        {  //Arrange 
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer
            { 
                CustomerId = "MANDAL",
                ContactName = "Nish Mandal",
                CompanyName = "Sparta Global",
                City = "Birmingham"
            };
            mockCustomerService.Setup(cs => cs.GetCustomerById("MANDAL")).Returns(originalCustomer);
            _sut = new CustomerManager(mockCustomerService.Object);
            // Act
            var result = _sut.Update("MANDAL", "Nish Kumar", "UK", "London", null);
            //Assert 
            Assert.That(_sut.SelectedCustomer.ContactName, Is.EqualTo("Nish Kumar"));
            Assert.That(_sut.SelectedCustomer.CompanyName, Is.EqualTo("Sparta Global"));
            Assert.That(_sut.SelectedCustomer.Country, Is.EqualTo("UK"));
            Assert.That(_sut.SelectedCustomer.City, Is.EqualTo("London"));
        }

        [Test]
        [Category("sad")]
        public void NotChangeTheSelectedCustomer_WhenUpdateIsCalled_WithInvalidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer
            {
                CustomerId = "MANDAL",
                ContactName = "Nish Mandal",
                CompanyName = "Sparta Global",
                City = "birmingham"
            };
            mockCustomerService.Setup(cs => cs.GetCustomerById("MANDAL")).Returns((Customer)null);
            _sut = new CustomerManager(mockCustomerService.Object);
            //Act
            _sut.SelectedCustomer = originalCustomer;
            var result = _sut.Update("MANDAL", "Nish Kumar", "UK", "London", null);
            //Assert
            Assert.That(_sut.SelectedCustomer.ContactName, Is.EqualTo("Nish Mandal"));
            Assert.That(_sut.SelectedCustomer.CompanyName, Is.EqualTo("Sparta Global"));
            Assert.That(_sut.SelectedCustomer.Country, Is.EqualTo(null));
            Assert.That(_sut.SelectedCustomer.City, Is.EqualTo("birmingham"));
        }

        //Sad path : Customer ID not found, should return false
        [Test]
        [Category("Sad Path")]
        public void DeleteCustomer_ReturnsFalseWhenCustomerIDIsNull()
        {
            //Arrange
            //Service
            var mockCustomerService = new Mock<ICustomerService>();
            //Customer
            var originalCustomer = new Customer { CustomerId = null };

            //Assigning the mock service to the sut
            _sut = new CustomerManager(mockCustomerService.Object);
            //Act
            //Selecting the fake customer (originalCustomer)
            _sut.SelectedCustomer = originalCustomer;
            //Deleting Fake Customer with invalid ID
            var result = _sut.Delete(_sut.SelectedCustomer.CustomerId);

            //Assert
            Assert.That(result, Is.False);
        }


        //Happy path: returns true, customer is deleted.
        [Test]
        [Category("Happy Path: Customer is Deleted")]

        public void DeleteCustomer_ReturnsTrue()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer
            {
                CustomerId = "MANDAL",
                ContactName = "Nish Mandal",
            };
            mockCustomerService.Setup(cs => cs.GetCustomerById("MANDAL")).Returns(originalCustomer);
            //Assign the fake service that now calls the mock customer
            _sut = new CustomerManager(mockCustomerService.Object);
            //Act
            var result = _sut.Delete("MANDAL");
            //Assert
            Assert.That(result, Is.True);
        }
        //Happy path: Selected Customer is null
        [Test]
        [Category("Happy Path: Selected Customer is properly deleted")]
        public void DeleteCustomer_AssureThatCustomerIsDeleted()
        {
            //Arrange
            //Making a fake / mock service
            var mockCustomerService = new Mock<ICustomerService>();
            //Making a mock customer
            var originalCustomer = new Customer
            {
                CustomerId = "MANDAL",
                ContactName = "Nish Mandal",
            };
            //Assinging the fake customer to the fake service
            mockCustomerService.Setup(cs => cs.GetCustomerById("MANDAL")).Returns(originalCustomer);
            _sut = new CustomerManager(mockCustomerService.Object);
            //Act
            var result = _sut.Delete("MANDAL");
            //Assert
            Assert.That(_sut.SelectedCustomer, Is.Null);
        }

        [Category("sad")]
        [Test]
        public void ReturnFalse_WhenUpdateIsCalled_AndADatabaseExceptionIsThrown()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(cs => cs.GetCustomerById(It.IsAny<string>())).Returns(new Customer());
            mockCustomerService.Setup(cs => cs.SaveCustomerChanges()).Throws<DataException>();

            var originalCustomer = new Customer
            {
                CustomerId = "MANDAL",
                ContactName = "Nish Mandal",
                CompanyName = "Sparta Global",
                City = "birmingham"
            };

            _sut = new CustomerManager(mockCustomerService.Object);
            _sut.SelectedCustomer = originalCustomer;

            var result = _sut.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.That(result, Is.False);

            Assert.That(_sut.SelectedCustomer.ContactName, Is.EqualTo("Nish Mandal"));
            Assert.That(_sut.SelectedCustomer.CompanyName, Is.EqualTo("Sparta Global"));
            Assert.That(_sut.SelectedCustomer.Country, Is.EqualTo(null));
            Assert.That(_sut.SelectedCustomer.City, Is.EqualTo("birmingham"));
        }

        //Spies

        [Test]
        public void CallSaveCustomerChanges_WhenUpdateIsCalled_WithValidId()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(cs => cs.GetCustomerById("MANDAL")).Returns(new Customer());
            _sut = new CustomerManager(mockCustomerService.Object);
            var result = _sut.Update("MANDAL", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            mockCustomerService.Verify(cs => cs.SaveCustomerChanges(), Times.Exactly(1));
        }

        [Test]
        public void LetsSeeWhatHappens_WhenUpdateIsCalled_IfAllMethodCallsAreNotSetUp()
        {
            var mockCustomerService = new Mock<ICustomerService>(MockBehavior.Strict);
            mockCustomerService.Setup(cs => cs.GetCustomerById("MANDAL")).Returns(new Customer());
            mockCustomerService.Setup(cs => cs.SaveCustomerChanges());
           _sut = new CustomerManager(mockCustomerService.Object);
            var result = _sut.Update("MANDAL", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.That(result);
        }

        [Test]
        public void RetrieveAll_ReturnsCustomerList()
        {
            //Arrange

            List<Customer> custList = new List<Customer>();
            var mockCustomerService = new Mock<ICustomerService>();
            var Customer1 = new Customer
             {
                CustomerId = "MANDAL",
                ContactName = "Nish Mandal",
                CompanyName = "Sparta Global",
                City = "birmingham"
             };
            var Customer2 = new Customer
            {
                CustomerId = "A",
                ContactName = "AName",
                CompanyName = "Sparta Global",
                City = "birmingham"
            };
            custList.Add(Customer1);
            custList.Add(Customer2);

            mockCustomerService.Setup(cs => cs.GetCustomerList()).Returns(custList);
            _sut = new CustomerManager(mockCustomerService.Object);
             var result = _sut.RetrieveAll().Count;

             //Assert
             Assert.That(result, Is.EqualTo(2));   
        }

        [Test]
        public void SetSelectedCustomer()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer
            {
                CustomerId = "MANDAL",
            };
            _sut = new CustomerManager(mockCustomerService.Object);
            _sut.SetSelectedCustomer(originalCustomer);

            Assert.That(_sut.SelectedCustomer.CustomerId, Is.EqualTo("MANDAL"));
        }


        //Create
        [Test]
        public void CustomerManager_Create_CallsExpectedMethods()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer
            {
                CustomerId = "1",
                ContactName = "2",
                CompanyName = "3",
                City = "4"
            };

           
            _sut = new CustomerManager(mockCustomerService.Object);
            
            
            mockCustomerService.Verify(cs => cs.CreateCustomer(originalCustomer), Times.Once);
        }

    }
}

    


