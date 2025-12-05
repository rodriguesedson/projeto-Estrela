using api.Entities;
using api.Models.Requests;

namespace api.Mappers;

public static class ProjectClassMapper
{
    public static ProjectClass ToEntity(RegisterClassRequest request)
    {
        return new ProjectClass()
        {
            Name = request.Name,
            Days =  request.Days,
            Time = request.Time,
            Students =  request.Students,
        };
    }
}