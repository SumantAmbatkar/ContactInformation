using System.Collections;
using System.Collections.Generic;

namespace ContactInformation.Models
{
    public interface IContactService
    {
        bool CreateContact(Contacts contactToCreate);
        bool DeleteContact(Contacts contactToDelete);
        bool EditContact(Contacts contactToEdit);
        Contacts GetContact(int id);
        IEnumerable<Contacts> ListContacts();
    }
}