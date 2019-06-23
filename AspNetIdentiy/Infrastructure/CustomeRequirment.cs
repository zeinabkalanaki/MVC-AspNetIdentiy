using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspNetIdentiy.Infrastructure
{
    public class CustomeRequirment : IAuthorizationRequirement
    {
        public int SomeValue { get; set; }
    }

    public class CustomeRequirmentHandler : AuthorizationHandler<CustomeRequirment>
    {

        protected override Task HandleRequirementAsync(
                                    AuthorizationHandlerContext context, 
                                    CustomeRequirment requirement)
        {
            var entity = context.Resource as BaseEntity;
            //Find User By Name
            var userId = 2;
            if (entity.AddBy == userId)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
