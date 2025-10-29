using API.Work.Domain.Events.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Application.Events
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Send welcome email or log something
        }
    }

}
