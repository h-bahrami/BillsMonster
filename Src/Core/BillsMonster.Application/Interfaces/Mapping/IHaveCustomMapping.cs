using AutoMapper;

namespace BillsMonster.Application.Interfaces.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);

        // void CreateReverseMappings(Profile configuration);
    }
}
