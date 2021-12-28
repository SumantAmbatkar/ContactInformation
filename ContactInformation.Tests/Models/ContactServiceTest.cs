using ContactInformation.Models;
using ContactInformation.Models.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace ContactInformation.Tests.Models
{
    [TestClass]
    public class ContactServiceTest
    {
        private Mock<IContactsRepository> _mockRepository;
        private ModelStateDictionary _modelState;
        private IContactService _service;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IContactsRepository>();
            _modelState = new ModelStateDictionary();
            _service = new ContactService(new ModelStateWrapper(_modelState), _mockRepository.Object);
        }

        [TestMethod]
        public void CreateContact()
        {
            // Arrange
            Contacts contact = new Contacts()
            {
                Id = -1,
                FirstName = "Stephen",
                LastName = "Walther",
                Phone = "555-5555",
                Email = "steve@somewhere.com",
                Status = "Active"
            };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateContactRequiredFirstName()
        {
            // Arrange
            Contacts contact = new Contacts()
            {
                Id = -1,
                FirstName = string.Empty,
                LastName = "Walther",
                Phone = "555-5555",
                Email = "steve@somewhere.com",
                Status = "Active"
            };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["FirstName"].Errors[0];
            Assert.AreEqual("First name is required.", error.ErrorMessage);
        }

        [TestMethod]
        public void CreateContactRequiredLastName()
        {
            // Arrange
            Contacts contact = new Contacts()
            {
                Id = -1,
                FirstName = "Stephen",
                LastName = string.Empty,
                Phone = "555-5555",
                Email = "steve@somewhere.com",
                Status = "Active"
            };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["LastName"].Errors[0];
            Assert.AreEqual("Last name is required.", error.ErrorMessage);
        }

        [TestMethod]
        public void CreateContactInvalidPhone()
        {
            // Arrange
            Contacts contact = new Contacts()
            {
                Id = -1,
                FirstName = "Stephen",
                LastName = "Walther",
                Phone = "India",
                Email = "steve@somewhere.com",
                Status = "Active"
            };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Phone"].Errors[0];
            Assert.AreEqual("Invalid phone number.", error.ErrorMessage);
        }

        [TestMethod]
        public void CreateContactInvalidEmail()
        {
            // Arrange
            Contacts contact = new Contacts()
            {
                Id = -1,
                FirstName = "Stephen",
                LastName = "Walther",
                Phone = "apple",
                Email = "India",
                Status = "Active"
            };

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Email"].Errors[0];
            Assert.AreEqual("Invalid email address.", error.ErrorMessage);
        }
    }
}
