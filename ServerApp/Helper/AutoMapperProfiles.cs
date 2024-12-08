using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using AutoMapper;

namespace ServerApp.Helper
{
    public class AutoMapperProfiles:Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Doctor, DoctorDTO>().ReverseMap() 
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordKey, opt => opt.Ignore());


            CreateMap<Patient, PatientDTO>().ReverseMap() 
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordKey, opt => opt.Ignore());

            CreateMap<AppointmentDTO, Appointment>()
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
            .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
            .ForMember(dest => dest.Problem, opt => opt.MapFrom(src => src.Problem))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            
           CreateMap<Schedule, ScheduleDTO>().ReverseMap(); 

           CreateMap<Department, DepartmentDTO>().ReverseMap();

        }


    }
}