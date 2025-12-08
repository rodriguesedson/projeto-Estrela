using api.Entities;
using api.Models.Requests;

namespace api.Mappers;

public static class ProjectClassMapper
{
    public static ProjectClass ToEntity(RegisterClassRequestDto requestDto)
    {
        return new ProjectClass()
        {
            Name = requestDto.Name,
            Days =  requestDto.Days,
            Time = requestDto.Time,
            Students =  requestDto.Students,
        };
    }
}