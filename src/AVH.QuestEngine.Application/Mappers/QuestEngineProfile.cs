using AutoMapper;
using AVH.QuestEngine.Application.ViewModels;
using AVH.QuestEngine.Domain.Entities;

namespace AVH.QuestEngine.Application.Mappers
{
    public class QuestEngineProfile : Profile
    {
        public QuestEngineProfile()
        {
            CreateMap<Milestone, MilestoneViewModel>()
                .ForMember(dest => dest.MilestoneIndex, opt => opt.MapFrom(src => src.Index));
        }
    }
}
