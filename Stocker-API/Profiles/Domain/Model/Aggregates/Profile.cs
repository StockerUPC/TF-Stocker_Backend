using Stocker_API.Profiles.Domain.Model.Commands;
using Stocker_API.Profiles.Domain.Model.Entities;
using Stocker_API.Profiles.Domain.Model.ValueObjects;

namespace Stocker_API.Profiles.Domain.Model.Aggregates;

/**
 * Profile aggregate root entity.
 *
 * <p>
 * This class represents the Profile aggregate root entity. It contains the properties and methods to manage the profile
 * </p>
 */
public partial class Profile
{
    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Address = new StreetAddress();
        SubscriptionId = int.MinValue;
    }

    public Profile(string firstName, string lastName, string email, string street, string number, string city,
        string postalCode, string country, int subscriptionId)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Address = new StreetAddress(street, number, city, postalCode, country);
        SubscriptionId = subscriptionId;
    }

    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Address = new StreetAddress(command.Street, command.Number, command.City, command.PostalCode, command.Country);
        SubscriptionId = command.SubscriptionId;
    }

    public int Id { get; }
    public PersonName Name { get;  set; }
    public EmailAddress Email { get;  set; }
    public StreetAddress Address { get;  set; }
    public Subscription Subscription { get;  internal set; }
    public int SubscriptionId { get;  set; }

    public string FullName => Name.FullName;

    public string EmailAddress => Email.Address;

    public string StreetAddress => Address.FullAddress;
}