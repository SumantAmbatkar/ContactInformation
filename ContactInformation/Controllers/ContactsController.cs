using ContactInformation.Models;
using ContactInformation.Models.Validations;
using System.Web.Mvc;

namespace ContactInformation.Controllers
{
    public class ContactsController : Controller
    {
        private IContactService _service;
        
        public ContactsController()
        {
            _service = new ContactService(new ModelStateWrapper(this.ModelState));
        }

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(_service.ListContacts());
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Home/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Contacts contactToCreate)
        {
            if (_service.CreateContact(contactToCreate))
                return RedirectToAction("Index");
            return View();
        }

        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_service.GetContact(id));
        }

        // POST: /Home/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Contacts contactToEdit)
        {
            if (_service.EditContact(contactToEdit))
                return RedirectToAction("Index");
            return View();
        }

        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View(_service.GetContact(id));
        }

        // POST: /Home/Delete/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contacts contactToDelete)
        {
            if (_service.DeleteContact(contactToDelete))
                return RedirectToAction("Index");
            return View();
        }
    }

    
}

