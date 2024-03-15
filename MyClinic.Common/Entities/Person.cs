using MyClinic.Common.ValueObjects;

namespace MyClinic.Common.Entities;

public abstract class Person : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Cpf Cpf { get; private set; }
    public Email Email { get; private set; }
    public Telephone Telephone { get; private set; }
    public Address Address { get; private set; }
    public BloodData BloodData { get; private set; }
    public Gender Gender { get; private set; }

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