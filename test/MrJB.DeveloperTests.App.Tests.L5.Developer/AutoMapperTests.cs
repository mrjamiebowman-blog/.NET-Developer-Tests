using AutoMapper;
using FluentAssertions;
using MrJB.DeveloperTests.App.AutoMapper;
using MrJB.DeveloperTests.App.Models;

namespace MrJB.DeveloperTests.App.Tests.L5.Developer
{
    public class AutoMapperTests
    {
        [Fact]
        public void Mapping_Tests()
        {
            // arrange
            var model = new SomeModel();
            model.FirstName = "Jamie";
            model.LastName = "Bowman";

            // auto mapper
            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<DataServiceMappingProfiles>()
            );

            // mapper 
            var mapper = new Mapper(config);

            var someOtherModel = mapper.Map<SomeOtherModel>(model);

            // assert
            config.AssertConfigurationIsValid();

            someOtherModel.Should().NotBeNull();

            // assert: propertiese
            someOtherModel.FirstName.Should().Be(model.FirstName);
            someOtherModel.LastName.Should().Be(model.LastName);
        }
    }
}
