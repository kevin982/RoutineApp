using MediatR;
using RoutineMS_Core.Models.Requests;

namespace RoutineMS_Application.Commands
{
    public class SetDoneCommand:IRequest<bool>
    {
        public SetDoneRequest Request { get; init; }

        public SetDoneCommand(SetDoneRequest request)
        {
            Request = request;
        }
    }
}
