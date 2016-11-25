using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
- Credentials
    - Access Level (all access badge, crew, staff, media, photo, artist, artist guest)
    - Full Event
    - Specific Day of Event
    - Cost per credential type (badge vs wristbands)
- Shirt Size
    - Women Cut
        - Each Size
        - Number per Size
        - Cost per shirt
    - Men Cut
        - Each Size
        - Number per Size
        - Cost per shirt
    
- Parking
    - Number of Spots
        - per day
        - label (sponsor, artist, staff, vendor, etc)
        - cost per spot
- Petty Cash
    - Amount
    - Date needed by
- Hotel
    - Check In Date
    - Check Out Date
    - Billed to festival or vendor (boolean)
    - Room type (double, king)
    - Number of people per Room
        - names of each occupant
    - Cost per room     
- Radios
    - Radio type
    - Extra battery (boolean)
    - Head set (boolean)
    - Cost per items
Golf Carts
    - Golf cart type (2 seater utility, 4 seater, 6 seater, gator)
    - Number of each
    - Cost per item
- Catering

    - Location (hotel, on site)
        - List of Dates available per location
        
    - Breakfast
        - Number of each per day
        - Cost per meal per location
    - Lunch 
        - Number of each per day
        - Cost per meal per location
    - Dinner
        - Number of each per day
        - Cost per meal per location
Department
    - Department name
    - Department Lead
    - List of each class within each department
    - boolean advance submitted
Vendor  
    - Vendor name
    - Vendor Lead
    - List of each class within each vendor
    - boolean advance submitted
Contact List    
    - first name
    - last name
    - position
    - department
    - vendor
    - phone
    - email

**Approvals - potentially create boolean RequestApproved for each class? - done by creating Advance class
**Confirm each class has cost component
**Remember functionality regarding approvals, assigning tasks, and setting due dates - done by creating Task class
**Make department required within each class 
**Make vendor optional within each class
**Add each class to Advance class
**Think about creating a public class Vendor exactly like department and including it in each class
**Add DateTime dueDate to each class - included in Department/Contact List/Vendor Classes
---------- Everything above is complete

Roles
    - Administrator/Management
        - Approvals
        - View of all submissions
        - Ability to set and view costs
        - Ability to create and assign reminder notifications
        

    - Department Head
        - Ability to submit requests per category
        - Ability to adjust/resubmit requests
        - Ability to set cost on certain items
        - Ability to view certain sets of data (ie number of golf carts/radios per department)

    - Vendor 
        - Ability to submit requests per category
        - Ability to adjust/resubmit requests

    - Artist? - consider what access they would need that is different from above roles
        - Stage schedules
        - Production access
        - Artist Transportation access
        - Payment tracking
        - Hospitality
        - Artist Credentials
        - Catering
        - Hotel
        - Bands


**Consider creating Artist artist (same as Department and vendor)
**Look into adding warehouse inventory tracker on here
**Look into adding functionality that allows user to submit spreadsheet of proposed costs
**Find out if I need to include both Advance advance AND list of advances to each class 


public class Event : HasId {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EventStart { get; set; }
    public DateTime EventEnd { get; set; }
    public DateTime LoadInStart { get; set; }
    public DateTime LoadOutEnd { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
    public IEnumerable<Tasking> Tasks { get; set; }
    public IEnumerable<Advance> Advances { get; set; }
    public IEnumerable<Contact> Contacts { get; set; }
    public IEnumerable<Department> Departments { get; set; }
    public IEnumerable<Vendor> Vendors { get; set; }
    public IEnumerable<Credential> Credentials { get; set; }
    public IEnumerable<StaffShirt> StaffShirts { get; set; }
    public IEnumerable<Parking> ParkingPasses { get; set; }
    public IEnumerable<Hotel> Hotels { get; set; }
    public IEnumerable<PettyCash> PCs { get; set; }
    public IEnumerable<Radio> Radios { get; set; }
    public IEnumerable<GolfCart> GolfCarts { get; set; }
    public IEnumerable<Catering> Meals { get; set; }
}
public class Notification : HasId {
    public int Id { get; set; }
    public DateTime createdAt { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public Contact contact { get; set; }
    public int ContactId { get; set; } //foreign key
    public Task task { get; set; }
    public int TaskId { get; set; }
    public IEnumerable<Tasking> Tasks { get; set; }
    public IEnumerable<Contact> Contacts { get; set; } // can a class have both? do either need to be instantiated?
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } // because each element of an advance has an advanceId foreign key, all notifications with that same key will be connected correct?
    public IEnumerable<Advance> Advances { get; set; }
    
}
public class Tasking : HasId {
    public int Id { get; set; }
    public DateTime DateAssigned { get; set; }
    public DateTime DueDate { get; set;}
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public string Assignor { get; set; }
    public bool TaskAssigned { get; set; }
    public bool TaskComplete { get; set; }
    public Department department { get; set; }
    public int DepartmentId { get; set; } //foreign key
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } //foreign key
    public Contact contact { get; set; }
    public int ContactId { get; set; } //foreign key
    public Notification notification { get; set; }
    public int NotificationId { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
    public Advance advance { get; set; }
    public int AdvanceId { get; set; }
    public IEnumerable<Advance> Advances { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
}
public class Advance : HasId {
    public int Id { get; set; }
    public Advance advance { get; set; } 
    public DateTime AdvanceSent { get; set; }
    public DateTime AdvanceDue { get; set; }
    public bool AdvanceSubmitted { get; set; } = false;
    public bool AdvanceReceived { get; set; } = false;
    public bool AdvanceApproved { get; set; }
    [Required]
    public Event Event { get; set; }
    public int EventId { get; set; }
    public Notification notification { get; set; }
    public int NotificationId { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
    [Required]
    public Contact contact { get; set; }
    public int ContactId { get; set; } //foreign key
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } //foreign key
    public IEnumerable<Department> Departments { get; set; }
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } //foreign key
    public IEnumerable<Vendor> Vendors { get; set; }
    public IEnumerable<Tasking> Tasks { get; set; }
    public IEnumerable<Credential> Credentials { get; set; }
    public IEnumerable<StaffShirt> StaffShirts { get; set; }
    public IEnumerable<Parking> ParkingPasses { get; set; }
    public IEnumerable<Hotel> Hotels { get; set; }
    public IEnumerable<PettyCash> PCs { get; set; }
    public IEnumerable<Radio> Radios { get; set; }
    public IEnumerable<GolfCart> GolfCarts { get; set; }
    public IEnumerable<Catering> Meals { get; set; }
}
public class Contact : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Position { get; set; }
    [Required]
    public string ContactEmail { get; set; }
    [Required]
    public long ContactPhone { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } //foreign key
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
    public Notification notification { get; set; }
    public int NotificationId { get; set; }
    public IEnumerable<Advance> Advances { get; set; }
    public IEnumerable<Tasking> Tasks { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
    
}
public class Department : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public string DeptName { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public string DeptLead { get; set; }
    public Advance advance { get; set; }
    public int AdvanceId { get; set; }
    public IEnumerable<Credential> Credentials { get; set; }
    public IEnumerable<StaffShirt> Staffshirts { get; set; }
    public IEnumerable<Parking> ParkingPasses { get; set; }
    public IEnumerable<Hotel> Hotels { get; set; }
    public IEnumerable<PettyCash> PCs { get; set; }
    public IEnumerable<Radio> Radios { get; set; }
    public IEnumerable<GolfCart> GolfCarts { get; set; }
    public IEnumerable<Catering> Meals { get; set; }
    public IEnumerable<Contact> Contacts { get; set; }
    public IEnumerable<Vendor> Vendors { get; set; }
    public IEnumerable<Advance> Advances { get; set; }
    public IEnumerable<Tasking> Tasks { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
    
}
public class Vendor : HasId {
    [Required]
    public int Id { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    public string VendorName { get; set; }
    public string VendorLead { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } // foreign key
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
    public IEnumerable<Credential> Credentials { get; set; }
    public IEnumerable<StaffShirt> Staffshirts { get; set; }
    public IEnumerable<Parking> ParkingPasses { get; set; }
    public IEnumerable<Hotel> Hotels { get; set; }
    public IEnumerable<PettyCash> PCs { get; set; }
    public IEnumerable<Radio> Radios { get; set; }
    public IEnumerable<GolfCart> GolfCarts { get; set; }
    public IEnumerable<Catering> Meals { get; set; }
    public IEnumerable<Contact> Contacts { get; set; }
    public IEnumerable<Tasking> Tasks { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
}
public class Credential : HasId {
    [Required]
    public int Id { get; set; }
    public double Cost { get; set; }
    [Required]
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public string AccessType { get; set; } //confirm that this means that the user can set the type options themselves
    // figure out method to give user option to add/remove values to type
    [Required]
    public string LevelType { get; set; }
    public string OneDay { get; set; }
    public string AllDays { get; set; }
    public int NumOfCreds { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } // foreign key
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
    
}
public class StaffShirt : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Event Event { get; set; }
    public int EventId { get; set; }
    public int NumOfShirts { get; set; }
    public string Cut { get; set; }
    [Required]
    public string Size { get; set; }
    public double Cost { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } // foreign key
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
}
public class Parking : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public int NumOfPasses { get; set; }
    public double CostPerSpot { get; set; }
    public DateTime Day { get; set; }
    public string LotLocation { get; set; }
    public string PassType { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } //foreign key
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
}
public class Hotel : HasId {
    [Required]
    public int Id { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public DateTime CheckIn { get; set; }
    [Required]
    public DateTime CheckOut { get; set; }
    [Required]
    public bool BillToEvent { get; set; } = false;
    [Required]
    public string RoomType { get; set; }
    [Required]
    public string OccupantName { get; set; }
    public string VendorName { get; set; }
    public double CostPerRoom { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } //foreign key
    public bool IncludeBreakfast { get; set; } = false;
    public int NumOfRooms { get; set; }
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key

}
public class PettyCash: HasId {
    [Required]
    public int Id { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public DateTime RequestedIssueDate { get; set; }
    [Required]
    public double AmountIssued { get; set; }
    public double RequestedAmount { get; set; }
    public double TotalApproved { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; } // foreign key
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
    
}
public class Radio : HasId {
    [Required]
    public int Id { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public string RadioType { get; set; }
    public int NumOfRadios { get; set; }
    public bool ExtraBattery { get; set; } = false;
    public bool HeadSet { get; set; } = false;
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; }
    public double CostPerRadio { get; set; }
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
    

}
public class GolfCart : HasId {
    [Required]
    public int Id { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    [Required]
    public string GCType { get; set; }
    public double GCCost { get; set; }
    public int NumOfGC { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; }
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
    
}
public class Catering : HasId {
    [Required]
    public int Id { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    public string LocationName { get; set; } //onsite, hotel, stage X (x = stage name) 
    public string MealType { get; set; } //ie Breakfast, lunch, Dinner, BBoxed, LBoxed, DBoxed
    public double CostPerMealType { get; set; }
    public int NumOfMeals { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Required]
    public Department department { get; set; }
    public int DepartmentId { get; set; }
    public Vendor vendor { get; set; }
    public int VendorId { get; set; } // foreign key
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; } //foreign key
   
}
// declare the DbSet<T>'s of our DB context, thus creating the tables
public partial class DB : IdentityDbContext<IdentityUser> {
    public DbSet<Event> Events { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Tasking> Tasks { get; set; }
    public DbSet<Advance> Advances { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Credential> Credentials { get; set; }
    public DbSet<StaffShirt> StaffShirts { get; set; }
    public DbSet<Parking> ParkingPasses { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<PettyCash> PCs { get; set; }
    public DbSet<Radio> Radios { get; set; }
    public DbSet<GolfCart> GolfCarts { get; set; }
    public DbSet<Catering> Meals { get; set; }
}

// create a Repo<T> services
public partial class Handler {
    public void RegisterRepos(IServiceCollection services){
        Repo<Event>.Register(services, "Events",
            d => d.Include(n => n.Name)
                .Include(e => e.EventStart)
                .Include(e => e.EventEnd));
        Repo<Notification>.Register(services, "Notifications",
            d => d.Include(c => c.contact)
                .ThenInclude(n => n.department.DeptName)
                .Include(t => t.Title)
                .Include(m => m.Message));
        Repo<Tasking>.Register(services, "Tasks",
            d => d.Include(c => c.contact)
                .ThenInclude(n => n.department.DeptName)
                .Include(n => n.TaskName)
                .Include(r => r.TaskDescription)
                .Include(t => t.DueDate)
                .Include(c => c.TaskComplete));
        Repo<Advance>.Register(services, "Advances",
            d => d.Include(n => n.department.DeptName)
                .Include(v => v.vendor.VendorName)
                .Include(s => s.AdvanceSent)
                .Include(f => f.AdvanceDue) 
                .Include(a => a.AdvanceReceived)
                .Include(r => r.AdvanceReceived));
        Repo<Contact>.Register(services, "Contact",
            d => d.Include(n => n.department.DeptName)
                .Include(v => v.vendor.VendorName)
                .Include(f => f.FirstName)
                .Include(l => l.LastName)
                .Include(p => p.Position)
                .Include(e => e.ContactEmail)
                .Include(p => p.ContactPhone)
                .Include(t => t.Tasks)
                .Include(n => n.Notifications));
        Repo<Department>.Register(services, "Departments",
            d => d.Include(n => n.DeptName)
                .Include(l => l.DeptLead)
                .Include(t => t.Tasks)
                .Include(n => n.Notifications));
        Repo<Vendor>.Register(services, "Vendors",
            d => d.Include(n => n.department.DeptName)
                .Include(n => n.VendorName)
                .Include(c => c.VendorLead)
                .Include(t => t.Tasks)
                .Include(n => n.Notifications));
        Repo<Credential>.Register(services, "Credentials",
            d => d.Include(n => n.department.DeptName)
                .Include(a => a.AccessType)
                .Include(l => l.LevelType));
        Repo<StaffShirt>.Register(services, "StaffShirts",
            d => d.Include(n => n.department.DeptName)
                .Include(c => c.Cut)
                .Include(s => s.Size));
        Repo<Parking>.Register(services, "ParkingPasses",
            d => d.Include(n => n.department.DeptName)
                .Include(l => l.LotLocation)
                .Include(p => p.PassType)
                .Include(f => f.Day));
        Repo<Hotel>.Register(services, "Hotels",
            d => d.Include(n => n.department.DeptName)
                .Include(o => o.OccupantName)
                .Include(v => v.VendorName));
        Repo<PettyCash>.Register(services, "PCs", 
            d => d.Include(n => n.department.DeptName)
                .Include(r => r.RequestedAmount)
                .Include(r => r.RequestedIssueDate)
                .Include(a => a.TotalApproved));
        Repo<Radio>.Register(services, "Radios",
            d => d.Include(n => n.department.DeptName)
                .Include(r => r.RadioType)
                .Include(b => b.ExtraBattery)
                .Include(b => b.HeadSet));
        Repo<GolfCart>.Register(services, "GolfCarts",
            d => d.Include(n => n.department.DeptName)
                .Include(g => g.GCType));
        Repo<Catering>.Register(services, "Meals",
            d => d.Include(n => n.department.DeptName)
                .Include(l => l.LocationName)
                .Include(m => m.MealType));
    }
}
*/
public class Event : HasId {
    [Required]
    public int Id { get; set; }
    public string name { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string Location { get; set; }
    public IEnumerable<Advance> Advances { get; set; }
}
public class Employee : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public string firstName { get; set; }
    [Required]
    public string lastName { get; set; }
    [Required]
    public string department { get; set; }
    [Required]
    public string phone { get; set; }
    [Required]
    public string email { get; set; }
    // public Employee(string firstName = "", string lastName = "", string department = "", string phone = "", string email = ""){
    //     this.firstName = firstName;
    //     this.lastName = lastName;
    //     this.department = department;
    //     this.phone = phone;
    //     this.email = email;
    // }
} // consider adding eventName to employee properties

public class Location : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public string name { get; set; }
    public double lattitude { get; set; }
    public double longitude { get; set; }
    // public Location(string name = "", double lattitude = 0, double longitude = 0){
    //     this.name = name;
    //     this.lattitude = lattitude;
    //     this.longitude = longitude;
    // }

}
public class Advance : HasId {
    [Required]
    public int Id { get; set; }
    public Event Event { get; set; }
    public int EventId { get; set; }
    public string department { get; set; }
    public Employee employee { get; set; }
    public int EmployeeId { get; set; }
    public DateTime dueDate { get; set; }
    public bool wasSent { get; set; }
    public bool wasSubmitted { get; set; }
    public bool wasApproved { get; set; }
    public IEnumerable<Location> Locations { get; set; }
    public IEnumerable<Credential> Credentials { get; set; } 
    public IEnumerable<Shirt> Shirts { get; set; } 
    public IEnumerable<Parking> Passes { get; set; } 
    public IEnumerable<PettyCash> Amounts { get; set; } 
    public IEnumerable<Hotel> Rooms { get; set; } 
    public IEnumerable<Radio> Radios { get; set; } 
    public IEnumerable<GolfCart> Carts { get; set; } 
    public IEnumerable<Catering> Meals { get; set; } 
    // public Advance(string eventName = "", string department = "", DateTime dueDate = default(DateTime), bool sent = false, bool submitted = false, bool approved = false){
    //     this.eventName = eventName;
    //     this.department = department;
    //     this.dueDate = dueDate;
    //     wasSent = sent;
    //     wasSubmitted = submitted;
    //     wasApproved = approved;
    //     Locations = new List<Location>();
    //     Credentials = new List<Credential>();
    //     Shirts = new List<Shirt>();
    //     Passes = new List<Parking>();
    //     Amounts = new List<PettyCash>();
    //     Rooms = new List<Hotel>();
    //     Radios = new List<Radio>();
    //     Carts = new List<GolfCart>();
    //     Meals = new List<Catering>();
    // }
}
public class Credential : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    [Required]
    public int AdvanceId { get; set; }
    public string credType { get; set; }
    public string accessLevel { get; set; }
    public string color { get; set; }
    public double cost { get; set; }
    // public Credential(string credType = "", string accessLevel = "", string color = "", double cost = 0){
    //     this.credType = credType;
    //     this.accessLevel = accessLevel;
    //     this.color = color;
    //     this.cost = cost;
    // }
}

public enum ShirtSize { XS, S, M, L, XL, XXL }
public class Shirt : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    [Required]
    public int AdvanceId { get; set; }
    public ShirtSize size { get; set; }
    public double cost { get; set; }
    // public Shirt(ShirtSize size = default(ShirtSize), double cost = 0){
    //     this.size = size;
    //     this.cost = cost;
    // }
}

public class Parking : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    [Required]
    public int AdvanceId { get; set; }
    public string passType { get; set; }
    public DateTime weekDay { get; set; }
    public IEnumerable<Location> Locations { get; set; }
    public double cost { get; set; }
    // public Parking(string passType = "", DateTime weekDay = default(DateTime), double cost = 0){
    //     this.passType = passType;
    //     this.weekDay = weekDay;
    //     this.cost = cost;
    //     Locations = new List<Location>();
    // }
}

public class PettyCash : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    [Required]
    public int AdvanceId { get; set; }
    public double amountRequested { get; set; }
    public double amountApproved { get; set; }
    public DateTime pcNeededBy { get; set; }
    public Employee employee { get; set; }
    public int EmployeeId { get; set; }
    // public PettyCash(double amountRequested = 0, double amountApproved = 0, DateTime pcNeededBy = default(DateTime)){
    //     this.amountRequested = amountRequested;
    //     this.amountApproved = amountApproved;
    //     this.pcNeededBy = pcNeededBy;
    // } // remember to include Employee name amount to be issued to
    
}
public enum RoomType { doubleRoom, kingRoom, suite}
public class Hotel : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    [Required]
    public int AdvanceId { get; set; }
    public Employee employee { get; set; }
    public int EmployeeId { get; set; }
    public RoomType type { get; set; }
    public DateTime checkIn { get; set; }
    public DateTime checkOut { get; set; }
    public bool eventPaysExpense { get; set; } 
    public string billTo { get; set; }
    public double cost { get; set; }
    public IEnumerable<Location> Locations { get; set; }
    // public Hotel(RoomType type = default(RoomType), DateTime checkIn = default(DateTime), DateTime checkOut = default(DateTime), bool eventPays = false, string billTo = "", double cost = 0){
    //     this.type = type;
    //     this.checkIn = checkIn;
    //     this.checkOut = checkOut;
    //     eventPaysExpense = eventPays;
    //     this.billTo = billTo;
    //     this.cost = cost;
    //     Locations = new List<Location>();
    // } // remember to figure out how to include employee name
}

public class Radio : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    [Required]
    public int AdvanceId { get; set; }
    public string radioType { get; set; }
    public bool hasExtraBattery { get; set; }
    public bool hasExtraHeadSet { get; set; }
    public double cost { get; set; }
    // public Radio(string radioType = "", bool extraBattery = false, bool extraHeadSet = false, double cost = 0){
    //     this.radioType = radioType;
    //     hasExtraBattery = extraBattery;
    //     hasExtraHeadSet = extraHeadSet;
    //     this.cost = cost;
    // }
}

public class GolfCart : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    [Required]
    public int AdvanceId { get; set; }
    public string cartType { get; set; }
    public double cost { get; set; }
    // public GolfCart(string cartType = "", double cost = 0){
    //     this.cartType = cartType;
    //     this.cost = cost;
    // }
}

public class Catering : HasId {
    [Required]
    public int Id { get; set; }
    [Required]
    public Advance advance { get; set; }
    public int AdvanceId { get; set; }
    public IEnumerable<Location> Locations { get; set; } // Hotel, CateringTent, Stage
    public string mealType { get; set; } // Breakfast, Lunch, Dinner, BoxBrek, BoxLunch, BoxDin
    public bool duringEventDays { get; set; } = false;
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public double cost { get; set; }
    // public Catering(string location = "", string mealType = "", bool eventDays = false, DateTime startDate = default(DateTime), DateTime endDate = default(DateTime), double cost = 0){
    //     this.mealType = mealType;
    //     duringEventDays = eventDays;
    //     this.startDate = startDate;
    //     this.endDate = endDate;
    //     this.cost = cost;
    //     Locations = new List<Location>();
    // }
}

public partial class DB : IdentityDbContext<IdentityUser> {
    public DbSet<Event> Events { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Advance> Advances { get; set; }
    public DbSet<Credential> Credentials { get; set; }
    public DbSet<Shirt> Shirts { get; set; }
    public DbSet<Parking> Passes { get; set; }
    public DbSet<PettyCash> Amounts { get; set; }
    public DbSet<Hotel> Rooms { get; set; }
    public DbSet<Radio> Radios { get; set; }
    public DbSet<GolfCart> Carts { get; set; }
    public DbSet<Catering> Meals { get; set; }
}

public partial class Handler {
     public void RegisterRepos(IServiceCollection services){
        Repo<Event>.Register(services, "Events");
        Repo<Advance>.Register(services, "Advances");
        Repo<Employee>.Register(services, "Employees");
        Repo<Location>.Register(services, "Locations");
        Repo<Credential>.Register(services, "Credentials");
        Repo<Shirt>.Register(services, "Shirts");
        Repo<Parking>.Register(services, "Passes");
        Repo<PettyCash>.Register(services, "Amounts");
        Repo<Hotel>.Register(services, "Rooms");
        Repo<Radio>.Register(services, "Radios");
        Repo<GolfCart>.Register(services, "Carts");
        Repo<Catering>.Register(services, "Meals");
     }
}