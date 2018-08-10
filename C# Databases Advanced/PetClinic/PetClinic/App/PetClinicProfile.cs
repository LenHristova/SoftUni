namespace PetClinic.App
{
    using AutoMapper;
    using DataProcessor.Dtos;
    using Models;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            CreateMap<AnimalAidDto, AnimalAid>();
            CreateMap<VetDto, Vet>();

        }
    }
}
