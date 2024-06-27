using Stocker_API.IAM.Domain.Model.Commands;
using Stocker_API.IAM.Interfaces.REST.Resources;

namespace Stocker_API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}