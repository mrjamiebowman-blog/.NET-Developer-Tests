using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MrJB.DeveloperTests.App.AutoMapper
{
    public class DataServiceMappingProfiles : Profile
    {
        public DataServiceMappingProfiles()
        {
            //// models to models
            //CreateMap<Models.Applicants.DesignerApplicantCreate, Models.Applicants.DesignerApplicantSave>();

            //// entities to dto
            //CreateMap<Entities.DesignerApplicant, DTOs.DesignerApplicant>()
            //    .ForMember(d => d.Status, o => o.MapFrom((src, dest, destMember, context) =>
            //    {
            //        return src.Status switch
            //        {
            //            "P" => StatusEnum.Pending,
            //            "A" => StatusEnum.Approved,
            //            "H" => StatusEnum.Hold,
            //            "D" => StatusEnum.Denied,
            //            _ => throw new Exception($"Error mapping Entity to DTO. StatusEnum is not set. (ID: {src.Id}).")
            //        };
            //    }));
        }
    }
}
