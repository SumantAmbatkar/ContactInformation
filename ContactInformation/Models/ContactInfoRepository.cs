using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactInformation.Models
{
    public class ContactInfoRepository : IContactsRepository
    {
        private ContactDBEntities _entities = new ContactDBEntities();

        public Contacts GetContact(int id)
        {
            return (from c in _entities.Contacts
                    where c.Id == id
                    select c).FirstOrDefault();
        }

        public IEnumerable<Contacts> ListContacts()
        {
            return _entities.Contacts.ToList();
        }

        public Contacts CreateContact(Contacts contactToCreate)
        {
            _entities.Contacts.Add(contactToCreate);
            _entities.SaveChanges();
            return contactToCreate;
        }

        public Contacts EditContact(Contacts contactToEdit)
        {
            var originalContact = GetContact(contactToEdit.Id);
            if (originalContact != null)
            {
                _entities.Entry(originalContact).CurrentValues.SetValues(contactToEdit);
                _entities.SaveChangesAsync();
            }
            return contactToEdit;
        }

        public void DeleteContact(Contacts contactToDelete)
        {
            var originalContact = GetContact(contactToDelete.Id);
            if (originalContact != null)
            {
                _entities.Contacts.Remove(originalContact);
                _entities.SaveChangesAsync();
            }
        }
    }
}