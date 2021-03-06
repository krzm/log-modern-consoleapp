using Log.Data;
using Log.Modern.CliApp.TestApi;
using Xunit;
using XUnit.Helper;

namespace Log.Modern.CliApp.Tests;

[Collection(DbTests)]
[TestCaseOrderer(OrdererTypeName, OrdererAssemblyName)]
public class PlaceInsertTests
    : OrderTest
    , IClassFixture<LogFixture>
{
    private LogFixture fixture;

    public PlaceInsertTests(LogFixture fixture)
    {
        this.fixture = fixture;
    }

    [Theory]
    [MemberData(nameof(PlaceData.Insert01), MemberType= typeof(PlaceData))]
    public void Test01(params string[] cmd)
    {
        fixture.AssertPlaceCount(fixture.Uow, 0);
        fixture.RunCmd(fixture.Booter, cmd);
        fixture.AssertPlaceCount(fixture.Uow, 1);
        var data = fixture.GetPlace(fixture.Uow, elementIndex: 0);
        fixture.AssertPlace(
            new Place 
            { 
                Name = "cracow"
                , Description = "city with 700k cheap labor"
            }
            , data);
    }
}