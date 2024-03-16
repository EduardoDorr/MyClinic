using MyClinic.Common.ValueObjects;

namespace MyClinic.Common.Entities;

public abstract class Person : BaseEntity
{
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public DateTime BirthDate { get; protected set; }
    public Cpf Cpf { get; protected set; }
    public Email Email { get; protected set; }
    public Telephone Telephone { get; protected set; }
    public Address Address { get; protected set; }
    public BloodData BloodData { get; protected set; }
    public Gender Gender { get; protected set; }

    protected Person() { }

    protected Person(string firstName,
                     string lastName,
                     DateTime birthDate,
                     Cpf cpf,
                     Email email,
                     Telephone telephone,
                     Address address,
                     BloodData bloodData,
                     Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Cpf = cpf;
        Email = email;
        Telephone = telephone;
        Address = address;
        BloodData = bloodData;
        Gender = gender;
    }
}