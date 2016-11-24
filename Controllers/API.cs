using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

/*
[Route("api/event")]
public class EventController : CRUDController<Event> {
    public EventController(IRepository<Event> r) : base(r){}
}

[Route("api/notification")]
public class NotificationController : CRUDController<Notification> {
    public NotificationController(IRepository<Notification> r) : base(r){}
}
[Route("/api/tasking")]
public class TaskingController : CRUDController<Tasking> {
    public TaskingController(IRepository<Tasking> r) : base(r){}

    [HttpGet("search")]
    public IActionResult Search([FromQuery]string term, int listId = -1){
        return Ok(r.Read(dbset => dbset.Where(task => 
            task.TaskName.ToLower().IndexOf(term.ToLower()) != -1
            || task.TaskDescription.ToLower().IndexOf(term.ToLower()) != -1
        )));
    }
}
[Route("/api/contact")]
public class ContactController : CRUDController<Contact> {
    public ContactController(IRepository<Contact> r) : base(r){}
}

[Route("/api/department")]
public class DepartmentController : CRUDController<Department> {
    public DepartmentController(IRepository<Department> r) : base(r){}
}
[Route("/api/vendor")]
public class VendorController : CRUDController<Vendor> {
    public VendorController(IRepository<Vendor> r) : base(r){}
}
*/

[Route("/api/advance")]
public class AdvanceController : CRUDController<Advance> {
    public AdvanceController(IRepository<Advance> r) : base(r){}
    [HttpGet("search")]
    public IActionResult Search([FromQuery]string term, int listId = -1){
        return Ok(r.Read(dbset => dbset.Where(advance => 
            advance.Department.ToLower().IndexOf(term.ToLower()) != -1
            || advance.DepartmentLead.ToLower().IndexOf(term.ToLower()) != -1
            || advance.EventName.ToLower().IndexOf(term.ToLower()) != -1
        ))); // figure out how to search by duedate
}

[Route("/api/credential")]
public class CredentialController : CRUDController<Credential> {
    public CredentialController(IRepository<Credential> r) : base(r){}
}

[Route("/api/shirt")]
public class ShirtController : CRUDController<Shirt> {
    public ShirtController(IRepository<Shirt> r) : base(r){}
}

[Route("/api/parking")]
public class ParkingController : CRUDController<Parking> {
    public ParkingController(IRepository<Parking> r) : base(r){}
}

[Route("/api/hotel")]
public class HotelController : CRUDController<Hotel> {
    public HotelController(IRepository<Hotel> r) : base(r){}
}

[Route("/api/pettycash")]
public class PettyCashController : CRUDController<PettyCash> {
    public PettyCashController(IRepository<PettyCash> r) : base(r){}
}

[Route("/api/radio")]
public class RadioController : CRUDController<Radio> {
    public RadioController(IRepository<Radio> r) : base(r){}
}

[Route("/api/golfcart")]
public class GolfCartController : CRUDController<GolfCart> {
    public GolfCartController(IRepository<GolfCart> r) : base(r){}
}

[Route("/api/catering")]
public class CateringController : CRUDController<Catering> {
    public CateringController(IRepository<Catering> r) : base(r){}
}