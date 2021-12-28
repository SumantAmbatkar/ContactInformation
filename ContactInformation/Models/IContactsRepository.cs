using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactInformation.Models
{
    public interface IContactsRepository
    {
        Contacts CreateContact(Contacts contactToCreate);
        void DeleteContact(Contacts contactToDelete);
        Contacts EditContact(Contacts contactToUpdate);
        Contacts GetContact(int id);
        IEnumerable<Contacts> ListContacts();
    }
}