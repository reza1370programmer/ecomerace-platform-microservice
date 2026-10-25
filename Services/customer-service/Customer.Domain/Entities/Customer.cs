

using Domain.ValueObjects;

namespace Customer.Domain.Entities;

public class Customer
{

    public Guid Id { get; private set; }
    public  Guid UserId { get; private set;}
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber?  PhoneNumber { get; private set; }
    public Address? Address { get; private set; }
    
    private Customer() {}

    private  Customer(Guid userId, string firstName, string lastName, Email email, PhoneNumber phoneNumber)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static Customer Create(Guid userId, string firstName, string lastName, Email email, PhoneNumber? phoneNumber)
    {
        return new Customer(userId, firstName, lastName, email, phoneNumber);
    }
    
    public void UpdateAddress(Address address)
    {
        Address = address;
    }
    
    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        phoneNumber = phoneNumber;
    }
}