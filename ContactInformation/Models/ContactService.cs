using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ContactInformation.Models;
using ContactInformation.Models.Validations;

namespace ContactInformation.Models
{
    public class ContactService : IContactService
    {
        private IValidationDictionary _validationDictionary;
        private IContactsRepository _repository;

        public ContactService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new ContactInfoRepository())
        { }

        public ContactService(IValidationDictionary validationDictionary, IContactsRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }

        public bool ValidateContact(Contacts contactToValidate)
        {
            if (contactToValidate.FirstName.Trim().Length == 0)
                _validationDictionary.AddError("FirstName", "First name is required.");
            if (contactToValidate.LastName.Trim().Length == 0)
                _validationDictionary.AddError("LastName", "Last name is required.");
            if (contactToValidate.Phone.Length > 0 && !Regex.IsMatch(contactToValidate.Phone, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                _validationDictionary.AddError("Phone", "Invalid phone number.");
            if (contactToValidate.Email.Length > 0 && !Regex.IsMatch(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                _validationDictionary.AddError("Email", "Invalid email address.");
            return _validationDictionary.IsValid;
        }


        public bool CreateContact(Contacts contactToCreate)
        {
            // Validation logic
            if (!ValidateContact(contactToCreate))
                return false;

            // Database logic
            try
            {
                _repository.CreateContact(contactToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContact(Contacts contactToEdit)
        {
            // Validation logic
            if (!ValidateContact(contactToEdit))
                return false;

            // Database logic
            try
            {
                _repository.EditContact(contactToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteContact(Contacts contactToDelete)
        {
            try
            {
                _repository.DeleteContact(contactToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Contacts GetContact(int id)
        {
            return _repository.GetContact(id);
        }

        public IEnumerable<Contacts> ListContacts()
        {
            return _repository.ListContacts();
        }

    }
}